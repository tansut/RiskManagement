/*  ----------------------------------------------------------------------------
 *  Kalitte Professional Information Technologies
 *  ----------------------------------------------------------------------------
 *  Dynamic Dashboards
 *  ----------------------------------------------------------------------------
 *  File:       View.ascx.cs
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using Kalitte.WidgetLibrary.Charting;
using System.Data;
using Kalitte.RiskManagement.Framework.Business.Reporting;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.UI;
using System.Collections;
using Kalitte.RiskManagement.Framework.Model.Reporting;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class RiskDurum : BaseChart
    {
        protected override Chart ThisChart
        {
            get
            {
                return chr;
            }
        }

        protected override UpdatePanel ThisUpdatePanel
        {
            get { return UpdatePanel1; }
        }

        private Series databindchart(string serieName, List<StatEntity> data)
        {
            Series serie = ThisChart.Series.Add(serieName);
            serie.Points.DataBind(data, "Name", "Value", "Label=Value,LegendText=Name");
            return serie;
        }

        protected override void DataBindChart(WidgetInstance instance)
        {
            ChartSettings settings = GetSettings(instance);
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();

            var data = new RiskReportBusiness().RetreiveRiskStatus(listingParams);
            ThisChart.Series.Clear();
            var serie = databindchart("Risk Durum", data);
            serie.PostBackValue = string.Format("{0}:{1}", serie.Name, "#AXISLABEL");
        }



        protected override void SetSettings(ChartSettings settings)
        {
            if (settings == null)
            {
                ctlDesc.Visible = true;
                ctlDesc.Text = "Lütfen grafik tanımlarını yapınız.";
            }
            else if (!string.IsNullOrEmpty(settings.Description))
            {
                ctlDesc.Visible = true;
                ctlDesc.Text = settings.Description;
            }
            else ctlDesc.Visible = false;
        }

        protected void chr_Click(object sender, ImageMapEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.PostBackValue))
            {                
                RiskFilter f = new RiskFilter();
                string[] value = e.PostBackValue.Split(":".ToCharArray(), StringSplitOptions.None);
                Kalitte.RiskManagement.Framework.Model.Common.RiskDurum durum;
                if (Enum.TryParse<Kalitte.RiskManagement.Framework.Model.Common.RiskDurum>(value[1], out durum))
                {
                    f.RiskDurum = durum;
                    string script = string.Format("openPageAsTab('{0}?filter={1}&Birims={2}','{3}');", Page.ResolveUrl("~/Pages/Risk/default.aspx"), HttpUtility.UrlEncode(f.ToString()), UnitFilterManager.GetActiveUnitsAsString(), "Risk Değerlendirme");
                    Kalitte.Dashboard.Framework.ScriptManager.GetInstance(this.Page).AddScript(script);
                }
            }
        }
    }
}
