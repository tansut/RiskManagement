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
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class RiskOlasilik : BaseChart
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

        protected void ThisChart_Click(object sender, ImageMapEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.PostBackValue))
            {
                RiskScoreBusiness rmb = new RiskScoreBusiness();
                RiskFilter f = new RiskFilter();
                string[] value = e.PostBackValue.Split(":".ToCharArray(), StringSplitOptions.None);
                if (value[0] == "Artık Risk")
                    f.ArtikOlasilikSkors.Add(rmb.GetOlasilikID(value[1]));
                string script = string.Format("openPageAsTab('{0}?filter={1}&Birims={2}','{3}');", Page.ResolveUrl("~/Pages/Risk/default.aspx"), HttpUtility.UrlEncode(f.ToString()), UnitFilterManager.GetActiveUnitsAsString(), "Risk Değerlendirme");
                Kalitte.Dashboard.Framework.ScriptManager.GetInstance(this.Page).AddScript(script);
            }
        }

        private Series databindchart(string serieName, List<RiskSkorEntity> data)
        {
            var chartData = from p in data
                            group p by p.Skor into grp
                            orderby grp.Count() descending
                            select new { Skor = grp.Key, Count = grp.Sum(p => p.Count), Average = grp.Average(p => p.SkorValue) };
            Series serie = ThisChart.Series.Add(serieName);
            serie.PostBackValue = string.Format("{0}:{1}", serie.Name, "#AXISLABEL");
            serie.Points.DataBind(chartData, "Skor", "Count", "Label=Count,LegendText=Skor");
            return serie;
        }

        protected override void DataBindChart(WidgetInstance instance)
        {
            ChartSettings settings = GetSettings(instance);
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();

            var data = new RiskSkorReportBusiness().RetrieveRiskParameters("ArtikRiskOlasilik", listingParams);
            ThisChart.Series.Clear();
            databindchart("Artık Risk", data);
            //data = new RiskSkorReportBusiness().RetrieveRiskParameters("RiskOlasilik", listingParams);
            //var serie = databindchart("Risk", data);

            //serie.ChartArea = "ChartArea2";
            //serie.Legend = "ArtikLegend";
        }

        protected override void SetCommonChartProperties(Chart chart, ChartSettings chartSettings)
        {
            base.SetCommonChartProperties(chart, chartSettings);
            ThisChart.Legends["ArtikLegend"].Enabled = false;
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

    }
}
