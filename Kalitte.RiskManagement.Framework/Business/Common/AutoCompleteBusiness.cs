using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class GroupType
    {
        public string Group { get; set; }
        public GroupType(string group)
        {
            this.Group = group;
        }
    }

    public class AutoCompleteBusiness : EntityBusiness<OtoTamamla>
    {
        protected override IQueryable<OtoTamamla> FilteredQuery(IQueryable<OtoTamamla> query, ListingParameters e = null)
        {
            if (e != null && !string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Deger", e.Query, Core.StringSearchType.Contains));
            }
            return base.FilteredQuery(query, e);
        }


        public List<GroupType> RetreiveGroupNames(ListingParameters e)
        {
            e.FieldFilters.Add(new FilteringInfo("Grup", e.Query, Core.StringSearchType.Like));
            List<string> items  = DataContext.OtoTamamla.Select(p => p.Grup).Where(p=>p.Contains(e.Query)).Distinct().ToList();
            var result = new List<GroupType>();
            foreach (var item in items)
            {
                result.Add(new GroupType(item));
            }
            return result;
        }

        internal void AddToList(string group, string field, string value)
        {
            if (!DataContext.OtoTamamla.Any(p => p.Grup == group && p.Alan == field && p.Deger == value))
            {
                var entity = new OtoTamamla();
                entity.Grup = group;
                entity.Alan = field;
                entity.Deger = value;
                InsertSingle(entity);
            }
        }
    }
}
