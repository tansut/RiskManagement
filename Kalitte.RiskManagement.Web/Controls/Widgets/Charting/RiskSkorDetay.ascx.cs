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

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class RiskSkorDetay : BaseChart
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

        protected override void DataBindChart(WidgetInstance instance)
        {
            ChartSettings settings = GetSettings(instance);
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();

            var data = new RiskSkorReportBusiness().RetreiveEntityItems(listingParams);

            ThisChart.Series.Clear();
            ThisChart.Series.Add("Artık Risk");
            ThisChart.Series[0].Points.DataBind(data, "SkorName", "Count", "");

            ThisChart.Series.Add("Risk");
            ThisChart.Series[1].Points.DataBind(new RiskSkorReportBusiness().RetreiveEntityItems(), "SkorName", "Count", "");
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
