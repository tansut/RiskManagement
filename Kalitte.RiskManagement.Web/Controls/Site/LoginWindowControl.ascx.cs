using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Web.Security;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Business;
using System.Net;

namespace Kalitte.RiskManagement.Web.Controls.Site
{
    public partial class LoginWindowControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        public static LoginWindowControl LoadInto(Page page)
        {
            LoginWindowControl window = (LoginWindowControl)page.LoadControl("~/Controls/Site/LoginWindowControl.ascx");
            window.ID = "loginControl";
            return window;
        }


        protected void btnLogin_Click(object sender, DirectEventArgs e)
        {
            Kalitte.RiskManagement.Framework.Security.AuthenticationManager.Login(txtUsername.Text, txtPassword.Text, ctlRememberMe.Checked);

        }
    }
}