using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Utility
{
    public class Sort
    {
        public string field { get; set; }
        public string direction { get; set; }

        public SortDirection GetDirection()
        {
            SortDirection direct = SortDirection.Default;

            if (!string.IsNullOrEmpty(direction))
            {
                if (direction.ToString() == "ASC")
                    direct = SortDirection.ASC;
                else if (direction.ToString() == "DESC")
                    direct = SortDirection.DESC;
            }
            return direct;
        }

        public static Sort FromJson(string jSon)
        {
            if (string.IsNullOrEmpty(jSon))
                return null;
            else return (Sort)JSON.Deserialize<Sort>(jSon);
        }
    }
}
