using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Core
{
    public class ListingParameters
    {
        public StoreRefreshDataEventArgs RefreshArgs { get; set; }

        private string sortField = null;
        private SortDirection? dir = null;

        public bool HasUnits
        {
            get
            {
                bool res = UserParams.ContainsKey("_ForcedUnits");
                if (res)
                    res = ((HashSet<int>)UserParams["_ForcedUnits"]).Count > 0 && ((HashSet<int>)UserParams["_ForcedUnits"]).First() != 0;
                return res;
            }
        }

        public HashSet<int> Units
        {
            get
            {
                return (HashSet<int>)UserParams["_ForcedUnits"];
            }
            set
            {
                UserParams["_ForcedUnits"] = new HashSet<int>(value);
            }
        }

        private Dictionary<string, object> userParams = null;
        public Dictionary<string, object> UserParams
        {
            get
            {
                if (userParams == null)
                    userParams = new Dictionary<string, object>();
                return userParams;
            }
        }

        public int Total
        {
            get
            {
                return RefreshArgs.Total;
            }

            set
            {
                RefreshArgs.Total = value;
            }
        }

        public int Start
        {
            get
            {
                return RefreshArgs.Start;
            }
        }

        public int Limit
        {
            get
            {
                return RefreshArgs.Limit;
            }
        }

        public string Query
        {
            get
            {
                return RefreshArgs.Parameters["query"];
            }
        }

        public string SortField
        {
            get
            {
                if (string.IsNullOrEmpty(sortField))
                    return string.IsNullOrEmpty(RefreshArgs.Sort) ? "ID" : RefreshArgs.Sort;
                else return sortField;
            }

            set
            {
                sortField = value;
            }

        }

        public SortDirection Dir
        {
            get
            {
                if (!dir.HasValue)
                    return RefreshArgs.Dir;
                else return dir.Value;
            }

            set
            {
                dir = value;
            }

        }

        public int PageIndex { get; set; }

        private FilterList fieldFilters = null;

        public FilterList FieldFilters
        {
            get
            {
                if (fieldFilters == null)
                    fieldFilters = new FilterList();
                return fieldFilters;
            }
        }


        public ListingParameters(StoreRefreshDataEventArgs e)
        {
            if (e == null)
            {
                RefreshArgs = new StoreRefreshDataEventArgs();
            }
            else
            {
                RefreshArgs = e;
            }
            Total = -1;
            if (RefreshArgs.Limit != 0)
                PageIndex = RefreshArgs.Start / RefreshArgs.Limit;
            else PageIndex = 1;
        }

        public ListingParameters()
            : this(null)
        {

        }

        public void SetFieldFilters(FilterConditions condition)
        {
            FieldFilters.Clear();
            if (condition != null)
                FieldFilters.AddRange(condition.Conditions.Select(p => FilteringInfo.CreateFromCondition(p)));
        }

        internal void LoadFilter(List<Filter> flist)
        {
            FieldFilters.Clear();
            foreach (var f in flist)
            {
                FilteringInfo fInfo = FilteringInfo.CreateFromFilter(f);
                FieldFilters.Add(fInfo);
            }
        }
    }
}
