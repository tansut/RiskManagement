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
    public partial class SoruCevap : BaseChart
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

        private Series databindchart(string serieName, List<SoruCevapEntity> data)
        {
            Series serie = ThisChart.Series.Add(serieName);
            serie.Points.DataBind(data, "Name", "Value", "Label=Value,LegendText=Name");
            return serie;
        }

        protected override void DataBindChart(WidgetInstance instance)
        {
            if (instance.WidgetSettings.ContainsKey("SoruID"))
            {
                var listingParams = new ListingParameters();
                listingParams.Units = UnitFilterManager.GetActiveUnits();
                QuestionAnswerReportBusiness bll = new QuestionAnswerReportBusiness();
                int soruId = Convert.ToInt32(instance.WidgetSettings["SoruID"]);
                var data = bll.RetreiveSoruCevap(soruId, listingParams);
                ThisChart.Series.Clear();
                databindchart("Soru Cevap", data);
            }
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