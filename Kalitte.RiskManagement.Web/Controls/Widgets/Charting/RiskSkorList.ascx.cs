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
    public partial class RiskSkorList : BaseDataList
    {

        protected override void BindData(Dashboard.Framework.Types.WidgetInstance instance)
        {
            var listingParams = new ListingParameters();
            listingParams.Units = UnitFilterManager.GetActiveUnits();

            var data = new RiskSkorReportBusiness().RetrieveRiskParameters("ArtikRiskSkor", listingParams);
            var chartData = from p in data
                            group p by p.Skor into grp
                            orderby grp.Count() descending
                            select new { Skor = grp.Key, Count = grp.Sum(p => p.Count), Average = grp.Average(p => p.SkorValue) };
            repeater.DataSource = chartData;
            repeater.DataBind();
        }

        protected override UpdatePanel ThisUpdatePanel
        {
            get { return this.up; }
        }
    }
}