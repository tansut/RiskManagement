using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace Kalitte.RiskManagement.Framework.Controls
{
    [ToolboxData("<{0}:TTReportViewer runat=server></{0}:TTReportViewer>")]
    public class TTReportViewer : ReportViewer
    {
        public TTReportViewer()
            : base()
        {
            Width = new Unit("100%");
            ShowFindControls = false;
            ShowRefreshButton = false;
            AsyncRendering = false;
        }


    }
}
