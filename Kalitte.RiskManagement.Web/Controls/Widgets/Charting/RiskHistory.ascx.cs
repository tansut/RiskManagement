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

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class RiskHistory : BaseChart
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

        private Series databindchart(string serieName, List<RiskHistoryEntity> data, string area)
        {
            //foreach (var item in data)
            //{
            //    var serie = ThisChart.Series.Add(item.Name);
            //    serie.Points.DataBind(data, "ApproveDate", "Value", "Label=Value,LegendText=Name");
            //}
            ThisChart.DataBindCrossTable(data, "Name", "ApproveDate", "Value", "Label=Value,LegendText=Name");
            foreach (var item in ThisChart.Series)
            {
                
                item.ChartArea = area;

            }
            return null;
        }

        protected override void DataBindChart(WidgetInstance instance)
        {
            ChartSettings settings = GetSettings(instance);
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();

            var data = new RiskHistoryReportBusiness().RetreiveHistory(listingParams);
            ThisChart.Series.Clear();
            databindchart("Risk Durum", data, "ChartArea1");


            SetPointColorsByLegendText();
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
