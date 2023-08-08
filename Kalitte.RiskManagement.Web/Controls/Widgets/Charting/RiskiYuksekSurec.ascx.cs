using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Reporting;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class RiskiYuksekSurec : BaseDataList
    {
        public string sureclist
        {
            get
            {
                if (ViewState["sl"] == null)
                    return string.Empty;
                else return (string)ViewState["sl"];
            }
            private set
            {
                ViewState["sl"] = value;
            }
        }

        protected override void BindData(Dashboard.Framework.Types.WidgetInstance instance)
        {
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();
            UnitReportingBusiness urb = new UnitReportingBusiness();
            var data = new RiskSkorReportBusiness().GetTopSurecWithRisk(listingParams);
            foreach (var item in data)
            {
                sureclist += item.SurecID;
                if (data.IndexOf(item) != data.Count() - 1)
                    sureclist += ",";

            }

            var matrix = urb.GetAverageRiskSkorColorList(data.Select(p => p.SkorValue).ToList());
            List<object> result = new List<object>();
            for (int i = 0; i < data.Count; i++)
            {
                result.Add(new { Surec = data[i].Skor, Skor = string.Format("{0} \\ {1}", data[i].SkorValue.ToString("0.00"), matrix[i].Display), Color = string.Format("background-color: {0}", matrix[i].Color) });
            }
            repeater.DataSource = result;
            repeater.DataBind();
        }

        protected override UpdatePanel ThisUpdatePanel
        {
            get { return this.up; }
        }

        protected void btnGoruntule_Click(object sender, EventArgs e)
        {
            //string script = string.Format("openPageAsTab('{0}?Birims={1}','{2}');", Page.ResolveUrl("~/Pages/Surec/default.aspx"), UnitFilterManager.GetActiveUnitsAsString(), "Süreç ve Risk Tanımları");
            //Kalitte.Dashboard.Framework.ScriptManager.GetInstance(this.Page).AddScript(script);
            //{0}?Birims={1}','{2}');", Page.ResolveUrl("~/Reports/Pages/BirimKarne.aspx"), id, ad));
            string script = string.Format("openPageAsTab('{0}?Surec={1}','{2}');", Page.ResolveUrl("~/Pages/Surec/default.aspx"), sureclist, "Süreç ve Risk Tanımları");
            Kalitte.Dashboard.Framework.ScriptManager.GetInstance(this.Page).AddScript(script);
        }
    }
}