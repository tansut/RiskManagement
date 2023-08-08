using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Ext.Net;
using Newtonsoft.Json.Linq;

namespace Kalitte.RiskManagement.Framework.Utility
{
    public class GridState
    {
        public List<Filter> Filters { get; set; }
        public Sort Sort { get; set; }

        public static GridState FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            else
            {
                object o = JSON.Deserialize(json);
                if (o is Newtonsoft.Json.Linq.JContainer)
                {
                    Newtonsoft.Json.Linq.JContainer c = (Newtonsoft.Json.Linq.JContainer)o;
                    if (c.HasValues)
                    {
                        JToken filter = c.SelectToken("filterValues");
                        JToken sort = c.SelectToken("sort");
                        GridState state = new GridState();
                        if (sort != null)
                        {
                            state.Sort = Sort.FromJson(sort.ToString());
                        }

                        if (filter != null)
                        {
                            state.Filters = Filter.FromJson(filter.ToString());
                        }
                        return state;
                    }
                }
            }
            return null;
        }
    }
}
