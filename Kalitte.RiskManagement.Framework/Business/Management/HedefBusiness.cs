using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class HedefBusiness : EntityBusiness<Hedef>
    {
        protected override IQueryable<Hedef> FilteredQuery(IQueryable<Hedef> query, Core.ListingParameters e = null)
        {
            if (e != null && !string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));

            }
            return base.FilteredQuery(query, e);
        }
    }
}
