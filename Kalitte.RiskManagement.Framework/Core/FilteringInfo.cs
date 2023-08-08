using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Kalitte.RiskManagement.Framework.Extensions;

namespace Kalitte.RiskManagement.Framework.Core
{
    public enum StringSearchType
    {
        Exact,
        Like,
        Contains
    }

    public class FilteringInfo
    {
        public Comparison Comparison { get; set; }
        public FilterType FilterType { get; set; }
        public string Name { get; private set; }
        public string Value { get; set; }
        public List<string> ValuesList { get; private set; }




        public FilteringInfo(string name)
            : this(name, string.Empty)
        {
        }

        public FilteringInfo(string name, string value)
            : this(name, value, FilterType.String)
        {

        }

        public FilteringInfo(string name, string value, StringSearchType searchType)
            : this(name, value, FilterType.String)
        {
            switch (searchType)
            {
                case StringSearchType.Exact:
                    break;
                case StringSearchType.Like:
                    if (Value == null)
                        Value = "%";
                    else if (!Value.EndsWith("%"))
                        Value += "%";
                    break;
                case StringSearchType.Contains:
                    if (Value == null)
                        Value = "*";
                    else if (!Value.EndsWith("*"))
                        Value += "*";
                    break;
                default:
                    break;
            }
        }

        public FilteringInfo(string name, string value, FilterType type)
        {
            this.Name = name;
            ValuesList = new List<string>();
            this.Value = value;
            this.FilterType = type;
        }

        public DateTime ValueAsDate
        {
            get
            {
                return DateTime.ParseExact(this.Value, "d", System.Threading.Thread.CurrentThread.CurrentCulture);
            }
        }

        public DateTime? ValueAsNullableDate
        {
            get
            {
                return new DateTime?(ValueAsDate);
            }
        }



        public DateTime ValueAsDateF(string format)
        {
            return DateTime.ParseExact(this.Value, format, System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        public bool ValueAsBoolean
        {
            get
            {
                if (this.Value == "1" || this.Value == "true" || this.Value == "True" || this.Value == "Yes" || this.Value == "yes" || this.Value == "Evet" || this.Value == "evet")
                {
                    return true;
                }

                if (this.Value == "0" || this.Value == "false" || this.Value == "False" || this.Value == "No" || this.Value == "no" || this.Value == "Hayır" || this.Value == "hayır")
                {
                    return false;
                }

                throw new ArgumentException("The value '" + this.Value + "' can not be parsed to bool");
            }
        }

        public bool? ValueAsNullableBool
        {
            get
            {
                return new bool?(ValueAsBoolean);
            }
        }

        public int ValueAsInt
        {
            get
            {
                return int.Parse(this.Value);
            }
        }

        public int? ValueAsNullableInt
        {
            get
            {
                if (string.IsNullOrEmpty(Value))
                    return new int?();
                else return new int?(ValueAsInt);
            }
        }

        public long ValueAsInt64
        {
            get
            {
                return long.Parse(this.Value);
            }
        }

        public double ValueAsDouble
        {
            get
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                return Double.Parse(this.Value, culture);
            }
        }

        public decimal ValueAsDecimal
        {
            get
            {
                IFormatProvider culture = new CultureInfo("en-US", true);
                return Decimal.Parse(this.Value, culture);
            }
        }

        public decimal? ValueAsNullableDecimal
        {
            get
            {
                if (string.IsNullOrEmpty(Value))
                    return new decimal?();
                else return new decimal?(ValueAsDecimal);
            }
        }

        public object ValueAsNumeric
        {
            get
            {
                int vi;

                if (int.TryParse(this.Value, out vi))
                {
                    return vi;
                }

                return this.ValueAsDouble;
            }
        }

        public static FilteringInfo CreateFromCondition(FilterCondition condition)
        {
            FilteringInfo r = new FilteringInfo(condition.Name);
            r.FilterType = condition.FilterType;
            r.Comparison = condition.Comparison;
            r.Value = condition.Value;
            if (condition.FilterType == FilterType.List && condition.ValuesList != null)
                r.ValuesList.AddRange(condition.ValuesList);
            return r;
        }

        internal System.Linq.Expressions.Expression BuildExpression(MemberExpression propertyAccess, ParameterExpression param)
        {

            PropertyInfo property = propertyAccess.Member as PropertyInfo;
            LambdaExpression left = Expression.Lambda(propertyAccess, param);
            ConstantExpression right = Expression.Constant(Value);

            Expression boolExp = null;

            switch (FilterType)
            {
                case Ext.Net.FilterType.List:
                    {
                        Expression temp = null;
                        foreach (var item in ValuesList)
                        {
                            right = Expression.Constant(item);
                            temp = Expression.Equal(left.Body, right);
                            if (boolExp == null)
                                boolExp = temp;
                            else boolExp = Expression.Or(boolExp, temp);
                        }
                        break;
                    }
                case Ext.Net.FilterType.Boolean:
                    {
                        if (property.PropertyType == typeof(bool))
                            right = Expression.Constant(ValueAsBoolean);
                        else if (property.PropertyType == typeof(bool?))
                            right = Expression.Constant(ValueAsNullableBool);
                        boolExp = Expression.Equal(left.Body, right);
                        break;
                    }
                case Ext.Net.FilterType.Date:
                    {
                        if (property.PropertyType == typeof(DateTime))
                            right = Expression.Constant(ValueAsDate);
                        else if (property.PropertyType == typeof(DateTime?))
                            right = Expression.Constant(ValueAsNullableDate, typeof(DateTime?));
                        switch (Comparison)
                        {
                            default:
                                {
                                    DateTime max = ValueAsDate.GetMaxDate();
                                    ConstantExpression right2 = property.PropertyType == typeof(DateTime) ?
                                        Expression.Constant(max) : Expression.Constant(new DateTime?(max), typeof(DateTime?));
                                    boolExp = BinaryExpression.And(Expression.GreaterThanOrEqual(left.Body, right),
                                        Expression.LessThanOrEqual(left.Body, right2));
                                    break;
                                }
                            case Ext.Net.Comparison.Gt:
                                {
                                    boolExp = Expression.GreaterThanOrEqual(left.Body, right);
                                    break;
                                }
                            case Ext.Net.Comparison.Lt:
                                {
                                    boolExp = Expression.LessThanOrEqual(left.Body, right);
                                    break;
                                }
                        }
                        break;
                    }
                case Ext.Net.FilterType.String:
                    {
                        string methodName = "";
                        if (Value.EndsWith("*"))
                            methodName = "Contains";
                        else if (Value.EndsWith("%"))
                            methodName = "StartsWith";
                        else if (Value.StartsWith("%"))
                            methodName = "EndsWith";
                        if (!string.IsNullOrEmpty(methodName))
                        {
                            right = Expression.Constant(Value.TrimEnd('*', '%').TrimStart('*', '%'));
                            boolExp = Expression.Call(left.Body, methodName, null, right);
                        }
                        else boolExp = Expression.Equal(left.Body, right);
                        break;
                    }
                case Ext.Net.FilterType.Numeric:
                    {
                        if (property.PropertyType == typeof(int))
                            right = Expression.Constant(ValueAsInt);
                        else if (property.PropertyType == typeof(long))
                            right = Expression.Constant(ValueAsInt64);
                        else if (property.PropertyType == typeof(int?))
                            right = Expression.Constant(ValueAsNullableInt, typeof(int?));
                        else if (property.PropertyType == typeof(decimal?))
                            right = Expression.Constant(ValueAsNullableDecimal);
                        else if (property.PropertyType == typeof(decimal))
                            right = Expression.Constant(ValueAsDecimal);
                        else right = Expression.Constant(ValueAsDouble);
                        switch (Comparison)
                        {
                            default:
                                {
                                    boolExp = Expression.Equal(left.Body, right);
                                    break;
                                }
                            case Ext.Net.Comparison.Gt:
                                {
                                    boolExp = Expression.GreaterThanOrEqual(left.Body, right);
                                    break;
                                }
                            case Ext.Net.Comparison.Lt:
                                {
                                    boolExp = Expression.LessThanOrEqual(left.Body, right);
                                    break;
                                }
                        }
                        break;
                    }
            }

            return boolExp;
        }

        internal static FilteringInfo CreateFromFilter(Filter f)
        {
            FilteringInfo fInfo = new FilteringInfo(f.field);

            fInfo.FilterType = (FilterType)Enum.Parse(typeof(FilterType), f.data.type.Capitialize());
            if (!string.IsNullOrEmpty(f.data.comparison))
                fInfo.Comparison = (Comparison)Enum.Parse(typeof(Comparison), f.data.comparison.Capitialize());
            if (f.data.value != null)
            {
                if (f.data.value is JArray)
                {
                    JArray arr = f.data.value as JArray;
                    foreach (JToken item in arr)
                        fInfo.ValuesList.Add(item.Value<string>());
                }
                else fInfo.Value = f.data.value.ToString();
            }
            else fInfo.Value = "";
            return fInfo;
        }
    }
}
