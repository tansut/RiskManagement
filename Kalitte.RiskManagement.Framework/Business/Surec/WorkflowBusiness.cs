using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class WorkflowBusiness: EntityBusiness<Kalitte.RiskManagement.Framework.Model.Surec>
    {
        protected override IQueryable<Model.Surec> FilteredQuery(IQueryable<Model.Surec> query, Core.ListingParameters e = null)
        {
            if (e != null && !string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));
            }

            if (e != null && e.UserParams.ContainsKey("idFilter"))
            {
                var filter = (int[])e.UserParams["idFilter"];
                query = query.Where(p => filter.Contains(p.ID));
            }
            return base.FilteredQuery(query, e);
        }


        public override void UpdateSingle(Model.Surec entity)
        {
            new SurecDayanakBusiness().DeleteBySurec(entity.ID);
            new SurecHedefBusiness().DeleteBySurec(entity.ID);
            new SurecYararlananBusiness().DeleteBySurec(entity.ID);
            new SurecIliskiBusiness().DeleteBySurec(entity.ID);
            new SurecCalisanBusiness().DeleteBySurec(entity.ID);
            base.UpdateSingle(entity);
        }

        public System.Collections.IList GetListing(Core.ListingParameters listingParameters, int exceptId)
        {
            var query = GetQueryable().Where(p=>p.ID != exceptId);
            query = CreateListingQuery(query, listingParameters);
            return ExecuteListQuery(query);
        }

        public List<Model.Surec> RetrieveWorkflowsofUser(Guid userid)
        {
            return GetQueryable().Where(p => p.KullaniciID == userid).ToList();
        }
    }
}
