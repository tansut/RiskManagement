using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class CityBusiness: EntityBusiness<IL>
    {
        protected override IQueryable<IL> FilteredQuery(IQueryable<IL> query, Core.ListingParameters e)
        {
            if (!string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));
            }
            return base.FilteredQuery(query, e);
        }

       
    }
}
