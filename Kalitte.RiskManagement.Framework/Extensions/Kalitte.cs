using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Kalitte.RiskManagement.Framework.Extensions
{
    public static class Kalitte
    {


        public static string AsTurkishDate(this DateTime d)
        {
            return d.ToString("dd.MM.yyyy hh:mm:ss");
        }

        public static string AsTurkishOnlyDate(this DateTime d)
        {
            return d.ToString("dd.MM.yyyy");
        }

        public static string AsExcelDate(this DateTime d)
        {
            return d.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff");
        }

        public static string ZeroAsEmpty(this int d)
        {
            return d == 0 ? string.Empty : d.ToString();
        }

        public static string ZeroAsEmpty(this long d)
        {
            return d == 0 ? string.Empty : d.ToString();
        }

        public static string ZeroAsEmpty(this int? d)
        {
            return !d.HasValue ? string.Empty : d.ToString();
        }

        public static string Capitialize(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                char first = s[0];
                string firstReplace = new string(new char[] { first }).ToUpperInvariant();
                string temp = s.TrimStart(s[0]);
                return firstReplace + temp;
            }
            else return s;
        }

        public static string EscapeXml(this string s)
        {
            string xml = s;
            if (!string.IsNullOrEmpty(xml))
            {
                xml = xml.Replace("&", "&amp;");
                xml = xml.Replace("<", "&lt;");
                xml = xml.Replace(">", "&gt;");
                xml = xml.Replace("\"", "&quot;");
                xml = xml.Replace("'", "&apos;");
                xml = xml.Replace("/", "&#47;");
            }
            return xml;
        }

        public static string UnescapeXml(this string s)
        {
            string unxml = s;
            if (!string.IsNullOrEmpty(unxml))
            {
                unxml = unxml.Replace("&apos;", "'");
                unxml = unxml.Replace("&quot;", "\"");
                unxml = unxml.Replace("&gt;", ">");
                unxml = unxml.Replace("&lt;", "<");
                unxml = unxml.Replace("&amp;", "&");
                unxml = unxml.Replace("&#47;", "/");
            }
            return unxml;
        }

        public static string IsEqualsReturn(this string str, string def)
        {
            string result = str ?? string.Empty;
            result = result.Trim();
            return def == result ? string.Empty : result;
        }


        public static string IsEqualsReturn(this string str, string def, string retval)
        {
            string result = str ?? retval;
            result = result.Trim();
            return def == result ? retval : result;
        }

        public static int ToIntDef(this string str, int def)
        {
            int result;
            int.TryParse(str ?? string.Empty, out result);
            return result == 0 ? def : result;
        }

        public static DateTime GetMaxDate(this DateTime dt)
        {
            DateTime maxdate = dt.Date;
            maxdate = maxdate.AddHours(23);
            maxdate = maxdate.AddMinutes(59);
            maxdate = maxdate.AddSeconds(59);
            maxdate = maxdate.AddMilliseconds(999);
            return maxdate;
        }

        public static Nullable<T> IsZeroReturnNullable<T>(this T parameter) where T : struct
        {
            if (parameter.Equals(Convert.ChangeType(0, typeof(T)))) return null; else return parameter;
        }

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static string GetEnumDescription(this Enum item)
        {
            string result = string.Empty;
            Type type = item.GetType();
            MemberInfo[] member = type.GetMember(item.ToString());
            if (member != null && member.Length > 0)
            {
                object[] att = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (att != null)
                {
                    DescriptionAttribute desc = att.Where(p => p.GetType() == (typeof(DescriptionAttribute))).Select(u => (DescriptionAttribute)u).SingleOrDefault();
                    if (desc != null) result = desc.Description;
                }
            }
            return result;
        }
    }
}
