using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Kalitte.RiskManagement.Framework.Core
{
    public static class UnitFilterManager
    {
        public static string Key
        {
            get
            {
                string key = HttpContext.Current.Request.Url.PathAndQuery;
                Regex reg = new Regex(@"_dc=\d+");
                Match match = reg.Match(key);
                if (match.Success)
                {
                    key = key.Replace(match.Value, "");
                    key = key.TrimEnd("?".ToCharArray());
                }
                return  string.Format("unitsOf{0}", key.GetHashCode());
            }
        }


        public static HashSet<int> GetActiveUnits(string key)
        {
            if (HttpContext.Current.Session[key] == null)
                return new HashSet<int>();
            else return (HashSet<int>)HttpContext.Current.Session[key];
        }

        public static HashSet<int> GetActiveUnits()
        {
            return GetActiveUnits(Key);
        }

        public static void SetActiveUnits(HashSet<int> unitsToSet)
        {
            var set = new HashSet<int>(unitsToSet);
            HttpContext.Current.Session[Key] = set;
        }


        public static void ClearActiveUnits()
        {
            HttpContext.Current.Session[Key] = null;
        }

        public static bool IsUnitSelected(int unitId)
        {
            return GetActiveUnits().Contains(unitId);
        }

        public static string GetActiveUnitsAsString()
        {
            StringBuilder sb = new StringBuilder();
            var list = GetActiveUnits();
            foreach (var item in list)
            {
                sb.AppendFormat("{0},", item);
            }
            return sb.ToString().TrimEnd(",".ToCharArray());
        }
    }
}
