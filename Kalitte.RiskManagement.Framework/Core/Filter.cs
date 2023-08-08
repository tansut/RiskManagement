using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Core
{
    public class Filter
    {
        public string field { get; set; }
        public FilterDataType data { get; set; }

        public static List<Filter> FromJson(string jSon)
        {
            if (string.IsNullOrEmpty(jSon))
                return null;
            else return (List<Filter>)JSON.Deserialize<List<Filter>>(jSon);
        }
    }
}
