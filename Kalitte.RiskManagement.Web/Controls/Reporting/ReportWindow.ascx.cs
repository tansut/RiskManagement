using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Web.Controls.Reporting
{
    public partial class ReportWindow : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        internal void Execute(string reportKey, ReportData data)
        {
            var dataKey = ReportStateManager.SetData(data);
            ctlRepWindow.AutoLoad.Url = string.Format("{0}?reportKey={1}&dataKey={2}", 
                Page.ResolveClientUrl("../../pages/shared/report/executereport.aspx"), 
                HttpUtility.UrlEncode(reportKey), 
                HttpUtility.UrlEncode(dataKey));
            ctlRepWindow.LoadContent();
            ctlRepWindow.Show();
        }
    }
}