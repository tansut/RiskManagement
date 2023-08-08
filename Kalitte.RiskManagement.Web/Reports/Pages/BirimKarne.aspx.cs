using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.UI;


namespace Kalitte.RiskManagement.Web.Reports.Pages
{
    public partial class BirimKarneDashboard : BaseDashboardPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ilid"]))
            {
                int ilid;
                if (int.TryParse(Request["ilid"].Trim(), out ilid))
                {
                    UnitBusiness ub = new UnitBusiness();
                    UnitFilterManager.SetActiveUnits(new HashSet<int>(ub.GetUnitsByIlId(ilid).Select(p => p.ID)));                                        
                }
            }
            base.OnInit(e);
        }

        protected override bool UseQueryStringForDashboardKey
        {
            get
            {
                return false;
            }
        }

        protected override Kalitte.Dashboard.Framework.DashboardSurface Dashboard
        {
            get { return surface; }
        }

        protected void surface_WidgetAdding(object sender, Dashboard.Framework.WidgetAddingEventArgs e)
        {
            
        }
    }
}
