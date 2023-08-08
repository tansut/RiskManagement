using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Web.Pages.Shared.User
{
    public partial class list : ListerViewControl<UserBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [CommandHandler(KnownCommand = KnownCommand.ViewReport)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var list = this.GetItems();
            var reportData = new ReportData();
            reportData.AddDataSource("Users", list);
            ctlReportWindow.Execute("KULLANICILISTESIRAPORU", reportData);
        }

        protected override ListingParameters GetListingParameters()
        {
            var prms = base.GetListingParameters();

            var filter = prms.FieldFilters.Where(p => p.Name == "AdSoyad").SingleOrDefault();

            if (filter != null)
            {
                prms.FieldFilters.Remove(filter);
                prms.UserParams.Add("AdSoyad", filter.Value);
            }



            


            if (!string.IsNullOrWhiteSpace(Request["Birims"]))
            {
                UnitFilterManager.SetActiveUnits(GetUnitsFromString(Request["Birims"]));
            }

            prms.Units = UnitFilterManager.GetActiveUnits();


            return prms;
        }


    

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            createUnitWindow();
            this.Controls.Add(unitSelect as Control);
            ctlUnitFilterBtn.Listeners.Click.Handler = string.Format("Ext.getCmp('{0}').show();", (unitSelect as IWindowControl).WindowClientID);
            
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


        public RiskFilter Filter
        {
            get
            {
                if (ViewState["Filter"] == null) ViewState["Filter"] = Activator.CreateInstance<RiskFilter>();
                return (RiskFilter)ViewState["Filter"];
            }
            set
            {
                ViewState["Filter"] = value;
            }
        }

        public void BindFilter(RiskFilter rf)
        {
            Filter = rf;
            LoadItems();
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
    }
}