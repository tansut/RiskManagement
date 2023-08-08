using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class UnvanBusiness : EntityBusiness<Unvan>
    {

        protected override IQueryable<Unvan> FilteredQuery(IQueryable<Unvan> query, Core.ListingParameters e)
        {
            if (!string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));
            }
            return base.FilteredQuery(query, e);
        }

        public Unvan GetIlbyAd(int IlId)
        {
            return GetQueryable().First(p => p.ID == IlId);
        }

       

        
    }
}
