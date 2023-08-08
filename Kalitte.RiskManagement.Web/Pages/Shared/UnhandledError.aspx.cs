using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Web.Pages.Shared
{
    public partial class UnhandledError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items.Contains("lastException"))
            {
                Exception exc = HttpContext.Current.Items["lastException"] as Exception;
                errorLabel.Text = exc.Message + ExceptionManager.ExceptionDebugDetails(exc);
                Server.ClearError();
            }
            else errorLabel.Text = "No Error";
        }
    }
}