using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.Shared.ServiceManagement;
using System.Web.Script.Serialization;
using System.Web;
using System.Diagnostics;
using Kalitte.BI.Shared.AnalyticsService;

namespace Kalitte.BI.Analytics.Utility
{
    public static class Helpers
    {
        public static void OpenSpecifigFilterPage(FilterSelectData data)
        {
            string url = ServerServices.Params.PortalUrl + "/default.aspx?action=filterData";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string filter = Convert.ToBase64String(Encoding.Default.GetBytes(serializer.Serialize(data)));
            string poststring = "filterData=" + HttpUtility.UrlEncode(filter);
            Process.Start(string.Format("{0}&{1}", url, poststring));
        }
    }
}
