using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Web.Pages.Shared.Report
{
    public partial class ExecuteReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var reportKey = Request["reportKey"];
                var dataKey = Request["dataKey"];
                var reportBll = new ReportBusiness(reportKey);

                ReportData data = ReportStateManager.GetData(dataKey) as ReportData;
                reportBll.ResponseReport(ctlViewer.LocalReport, ReportResponse.ReportViewer, data.Datasources, data.ReportParameters, null);
            }
        }
    }
}