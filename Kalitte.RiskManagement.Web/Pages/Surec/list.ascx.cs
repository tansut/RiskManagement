using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.BI.Shared;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Business.Reporting;

namespace Kalitte.RiskManagement.Web.Pages.Surec
{
    public partial class list : ListerViewControl<WorkflowBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private IUnitSelector unitSelect;

        private void createUnitWindow()
        {
            Control unitSelector = LoadControl("~/Controls/Widgets/UnitSelect/UnitSelectWindow.ascx");
            unitSelect = unitSelector as IUnitSelector;
            unitSelector.ID = this.ID + "selector";
            (unitSelector as ISupportsMultiplePostbackTypes).PostbackType = PostbackType.ExtNet;
            unitSelect.OnFilter += new EventHandler(unitSelect_OnFilter);
        }

        void unitSelect_OnFilter(object sender, EventArgs e)
        {
            LoadItems();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            createUnitWindow();
            this.Controls.Add(unitSelect as Control);
            ctlUnitFilterBtn.Listeners.Click.Handler = string.Format("Ext.getCmp('{0}').show();", (unitSelect as IWindowControl).WindowClientID);

        }

        public void BindFromBI(List<Kalitte.RiskManagement.Framework.Model.Surec> sureclist)
        {
            dsMain.DataSource = sureclist;
            dsMain.DataBind();
        }

        public override void DataBindStore(System.Collections.IList items)
        {
            if (dsMain.DataSource == null)
                base.DataBindStore(items);

        }

        protected override Framework.Core.ListingParameters GetListingParameters()
        {
            var parameters = base.GetListingParameters();
            if (!string.IsNullOrWhiteSpace(Request["selectDataKey"]))
            {
                var filter = Session["SelectData-" + Request["selectDataKey"]] as FilterSelectData;
                Session["SelectData-" + Request["selectDataKey"]] = null;
                parameters.UserParams.Clear();
                if (filter.IDList != null)
                {
                    parameters.UserParams.Add("idFilter", filter.IDList);
                }
                else
                    parameters.UserParams.Add("idFilter", new int[1] { filter.ID });
            }
            return parameters;
        }


        public HashSet<int> GetUnitsFromString(string units)
        {
            HashSet<int> result = new HashSet<int>();
            var array = units.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array)
            {
                result.Add(Convert.ToInt32(item));
            }
            return result;
        }

        protected override System.Collections.IList GetItems()
        {
            var lp = GetListingParameters();
            if (!Page.IsPostBack && !string.IsNullOrWhiteSpace(Request["Birims"]))
            {
                UnitFilterManager.SetActiveUnits(GetUnitsFromString(Request["Birims"]));
            }

            lp.Units = UnitFilterManager.GetActiveUnits();
            return BusinessObject.RetreiveItems(lp);

        }

    }
}