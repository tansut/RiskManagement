using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class SurecYararlananTanimBusiness : EntityBusiness<SurecYararlananTanim>
    {
        protected override IQueryable<SurecYararlananTanim> FilteredQuery(IQueryable<SurecYararlananTanim> query, Core.ListingParameters e = null)
        {
            if (e != null && !string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));
                e.FieldFilters.Operator = Core.FilterListOperator.Or;
            }
            return base.FilteredQuery(query, e);
        }

    }
}
