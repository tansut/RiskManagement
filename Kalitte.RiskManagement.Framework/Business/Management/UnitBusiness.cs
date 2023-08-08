using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class UnitBusiness : EntityBusiness<Birim>
    {
        private static object synObj = new object();
        //private static List<Birim> cache;


        public List<Birim> AllUnits
        {
            get
            {
                //lock (synObj)
                //{
                //    if (cache != null)
                //        return cache;
                //    else
                //    {
                //        cache = DataContext.Birim.Select(p => p).ToList();
                //        return cache;
                //    }
                //}
                return DataContext.Birim.Select(p => p).ToList();
            }
        }

        //public override void Update(Birim entity)
        //{
        //    base.Update(entity);
        //    lock (synObj)
        //    {
        //        cache.Clear();
        //    }
        //}

        public UnitBusiness()
            : base()
        {

        }

        //public override void Insert(Birim entity)
        //{
        //    base.Insert(entity);
        //    lock (synObj)
        //    {
        //        cache.Clear();
        //    }
        //}

        //public override void Delete(Birim entity)
        //{
        //    base.Delete(entity);
        //    lock (synObj)
        //    {
        //        cache.Clear();
        //    }
        //}

        public List<object> RetreiveUnitHierarchyName(HashSet<int> units)
        {
            var allUnits = AllUnits;
            List<string> result = new List<string>();
            foreach (var item in units)
            {
                List<string> hierarchyNames = new List<string>();
                var unit = allUnits.Where(p => p.ID == item).Single();
                hierarchyNames.Add(unit.Ad);

                while (unit.UstBirimID.HasValue)
                {
                    unit = allUnits.Where(p => p.ID == unit.UstBirimID.Value).Single();
                    hierarchyNames.Add(unit.Ad);
                }

                StringBuilder sb = new StringBuilder();
                for (int i = hierarchyNames.Count - 1; i >= 0; i--)
                {
                    sb.AppendFormat("{0} > ", hierarchyNames[i]);
                }
                result.Add(sb.ToString().TrimEnd(" > ".ToCharArray()));
            }
            if (!units.Any()) result.Add("T.C. Çevre ve Şehircilik Bakanlığı");
            return result.Select(p => new { Ad = p } as object).ToList();
        }

        public List<Birim> GetUnitsByIlId(int ilId)
        {
            return GetQueryable().Where(p => p.ILID == ilId).ToList();
        }
        public List<Birim> GetSubUnits(int parentId)
        {
            if (parentId > 0)
                return AllUnits.Where(p => p.UstBirimID == parentId).ToList();
            else return AllUnits.Where(p => p.UstBirimID.HasValue == false).ToList();
        }


        private void GetRelatedUnits(HashSet<int> current, int[] list)
        {
            foreach (var item in list)
            {
                if (!current.Contains(item))
                    current.Add(item);
                GetRelatedUnits(current, GetSubUnits(item).Select(p => p.ID).ToArray());
            }

        }

        public int GetBirimByIlName(string name)
        {
            name = name.Trim();
            return GetQueryable().Where(p => p.Ad == name).Select(s => s.ID).Single();
        }

        public HashSet<int> GetRelatedUnits(int[] list)
        {
            var set = new HashSet<int>();
            GetRelatedUnits(set, list);
            return set;
        }

        public List<Birim> GetRootUnits()
        {
            return GetQueryable().Where(p => p.UstBirimID == null).ToList();
        }

        public List<Birim> Retreive(HashSet<int> units)
        {
            var q = GetQueryable();
            q = q.Where(p => units.Contains(p.ID));
            return ExecuteListQuery(q);
        }

        protected override IQueryable<Birim> FilteredQuery(IQueryable<Birim> query, Core.ListingParameters e = null)
        {
            if (e != null && !String.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Add(new Core.FilteringInfo("Ad", e.Query, Core.StringSearchType.Contains));
                e.FieldFilters.Add(new Core.FilteringInfo("CityName", e.Query, Core.StringSearchType.Contains));
                e.FieldFilters.Operator = Core.FilterListOperator.Or;

            }
            return base.FilteredQuery(query, e);
        }

        protected override IQueryable<Birim> PermissionQuery(IQueryable<Birim> query, ListingParameters listingParams = null)
        {
            if (PermissionMode != EntityPermissonMode.All && !UserBusiness.UserHasGlobalRights())
            {
                var units = AuthenticationManager.UnitsUserHasPermission;
                query = query.Where(p => units.Contains(p.ID));
            }
            return base.PermissionQuery(query);
        }
    }
}
