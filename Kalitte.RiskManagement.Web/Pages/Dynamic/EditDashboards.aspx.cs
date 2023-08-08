/*  ----------------------------------------------------------------------------
 *  Kalitte Professional Information Technologies
 *  ----------------------------------------------------------------------------
 *  Dynamic Dashboards
 *  ----------------------------------------------------------------------------
 *  File:       Dashboards.aspx.cs
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;

namespace Kalitte.RiskManagement.Web.Pages.Dynamic
{
    public partial class EditDashboards : BasePage
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!Page.IsPostBack)
            {
                ctlEditor.Edit(Request["d"]);
            }
        }



    }
}
