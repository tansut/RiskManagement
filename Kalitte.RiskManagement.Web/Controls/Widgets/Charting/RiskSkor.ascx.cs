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
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Model;
using Ext.Net;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class RiskSkor : BaseChart
    {



        protected override Chart ThisChart
        {
            get
            {
                return chr;
            }
        }




        protected void ThisChart_Click(object sender, ImageMapEventArgs e)
        {            
            if (!string.IsNullOrWhiteSpace(e.PostBackValue))
            {
                RiskMatrisBusiness rmb = new RiskMatrisBusiness();
                RiskFilter f = new RiskFilter();
                string[] value = e.PostBackValue.Split(":".ToCharArray(), StringSplitOptions.None);
                if (value[0] == "Artık Risk")
                    f.ArtikSkors.Add(rmb.GetGrupDegerID(value[1]));
                string script = string.Format("openPageAsTab('{0}?filter={1}&Birims={2}','{3}');", Page.ResolveUrl("~/Pages/Risk/default.aspx"), HttpUtility.UrlEncode(f.ToString()), UnitFilterManager.GetActiveUnitsAsString(), "Risk Değerlendirme");
                Kalitte.Dashboard.Framework.ScriptManager.GetInstance(this.Page).AddScript(script);                
            }
        }

        protected override UpdatePanel ThisUpdatePanel
        {
            get { return UpdatePanel1; }
        }

        protected override void DataBindChart(WidgetInstance instance)
        {
            ChartSettings settings = GetSettings(instance);
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();

            //var data = new RiskSkorReportBusiness().RetrieveRiskParameters("ArtikRiskSkor", listingParams);
            //var chartData = from p in data
            //                group p by p.Skor into grp
            //                orderby grp.Count() descending
            //                select new { Skor = grp.Key, Count = grp.Sum(p => p.Count), Average = grp.Average(p => p.SkorValue) };


            //ThisChart.Series.Clear();
            //Series serie = ThisChart.Series.Add("Artık Risk");
            //serie.PostBackValue = string.Format("{0}:{1}", serie.Name, "#AXISLABEL");
            //serie.Points.DataBind(chartData, "Skor", "Count", "Label=Count,LegendText=Skor");

            var data = new RiskSkorReportBusiness().RetrieveRiskParameters("ArtikRiskSkor",listingParams);
            var chartData = from p in data
                        group p by p.Skor into grp
                        orderby grp.Count() descending
                        select new { Skor = grp.Key, Count = grp.Sum(p => p.Count), Average = grp.Average(p => p.SkorValue) };

            var serie = ThisChart.Series.Add("Artık Risk");
            serie.ChartArea = "ChartArea1";
            serie.Legend = "ArtikLegend";
            serie.Points.DataBind(chartData, "Skor", "Count", "Label=Count,LegendText=Skor");
            serie.PostBackValue = string.Format("{0}:{1}", serie.Name, "#AXISLABEL");
            SetPointColors();

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
