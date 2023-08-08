using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class SurecDayanakTanimBusiness : EntityBusiness<SurecDayanakTanim>
    {
        protected override IQueryable<SurecDayanakTanim> FilteredQuery(IQueryable<SurecDayanakTanim> query, Core.ListingParameters e = null)
        {
            if (e != null && !string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));
                e.FieldFilters.Add(new Core.FilteringInfo("Aciklama", e.Query, Core.StringSearchType.Contains));
                e.FieldFilters.Operator = Core.FilterListOperator.Or;
            }
            return base.FilteredQuery(query, e);
        }
    }
}
