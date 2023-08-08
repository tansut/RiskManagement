using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.Dashboard.Framework;


namespace Kalitte.RiskManagement.Web.Pages
{
    public partial class _default : BaseDashboardPage
    {
        protected override bool UseQueryStringForDashboardKey
        {
            get
            {
                return false;
            }
        }

        protected override void BindDashboard()
        {
            
            string key;
            var dashboards = DashboardFramework.GetDashboards();
            //if (UserBusiness.UserHasGlobalRights())
            //    key = dashboards.Single(p => p.UserTag == "MERKEZANASAYFA").InstanceKey.ToString();
            //else key = dashboards.Single(p => p.UserTag == "BIRIMANASAYFA").InstanceKey.ToString();

            key = dashboards.Single(p => p.UserTag == "MERKEZANASAYFA").InstanceKey.ToString();
            Dashboard.DashboardKey = key;
            base.BindDashboard();
        }



 

        protected override Dashboard.Framework.DashboardSurface Dashboard
        {
            get { return surface; }
        }

         
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}