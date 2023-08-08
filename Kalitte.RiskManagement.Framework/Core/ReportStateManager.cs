using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Kalitte.RiskManagement.Framework.Core
{
    public static class ReportStateManager
    {
        public static string SetData(object data)
        {
            var newKey = Guid.NewGuid();
            string key = string.Format("_ReportData{0}", newKey);
            HttpContext.Current.Session[key] = data;
            return newKey.ToString();
        }

        public static object GetData(string key)
        {
            string rkey = string.Format("_ReportData{0}", key);
            object data = HttpContext.Current.Session[rkey];
            HttpContext.Current.Session[rkey] = null;
            return data;
        }
    }
}
