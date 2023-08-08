using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Model.Reporting;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class UnitReportingBusiness : ReportingBusiness<StatEntity>
    {
        public int GetSurecCount(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Surec>(DataContext.Surec, listingParams);
            return cq.Where(w => w.Aktif).Count();
        }
        public int GetRiskCount(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            var templateRiskStr = RiskDurum.Taslak.ToString();
            return cq.Where(w => w.Surec.Aktif && w.Durum != templateRiskStr).Count();
        }
        public int GetApprovedRiskCount(ListingParameters listingParams = null)
        {
            var durum = RiskDurum.Onaylandı.ToString();
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            return cq.Where(w => w.ArtikSkor.HasValue && w.Durum == durum && w.Surec.Aktif).Count();
        }
        public int GetNotAppovedRiskCount(ListingParameters listingParams = null)
        {
            var durum = RiskDurum.PuanlamaBekler.ToString();
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            return cq.Where(w => w.Durum == durum && w.Surec.Aktif).Count();
        }
        public int GetWaitingApproveRiskCount(ListingParameters listingParams = null)
        {
            var durum = RiskDurum.OnayBekler.ToString();
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            return cq.Where(w => w.Durum == durum && w.Surec.Aktif).Count();
        }
        public int GetKontrolCount(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Kontrol>(DataContext.Kontrol, listingParams);
            return cq.Where(w => w.Risk.ArtikSkor.HasValue && w.Risk.Surec.Aktif).Count();
        }
        public int GetUserCount(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.aspnet_Users>(DataContext.aspnet_Users, listingParams);
            return cq.Count();
        }
        public DateTime? GetIlkPuanDate(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            var result = cq.Where(w => w.ArtikSkor.HasValue && w.Surec.Aktif).OrderBy(u => u.KayitTarih).Select(r => r.KayitTarih).FirstOrDefault();
            if (result == default(DateTime)) return null; else return result;
        }
        public DateTime? GetSonPuanDate(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            var result = cq.Where(w => w.ArtikSkor.HasValue && w.Surec.Aktif).OrderByDescending(u => u.KayitTarih).Select(r => r.KayitTarih).FirstOrDefault();
            if (result == default(DateTime)) return null; else return result;
        }
        public double GetAverageRiskEtki(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            var result = cq.Where(w => w.ArtikEtki.HasValue && w.Surec.Aktif).ToList();
            if (result.Any())
            {
                return Math.Round(result.Average(a => a.ArtikEtki.Value), 2);
            }
            else return 0;
        }
        public double GetAverageRiskOlasilik(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            var result = cq.Where(w => w.ArtikOlasilik.HasValue && w.Surec.Aktif).ToList();
            if (result.Any())
            {
                return Math.Round(result.Average(a => a.ArtikOlasilik.Value), 2);
            }
            else return 0;
        }
        public double GetAverageRiskSkor(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            var result = cq.Where(w => w.ArtikSkor.HasValue && w.Surec.Aktif).ToList();
            if (result.Any())
            {
                return Math.Round(result.Average(a => a.ArtikSkor.Value), 2); // todo:tanımıda döndür
            }
            else return 0;
        }
        public RiskMatrisEntity GetAverageRiskSkorColor(int skor)
        {
            var matrix = new RiskMatrisBusiness().GetMatrixAsList();
            var itemScore = RiskMatrisBusiness.GetMatixStore(matrix, skor);
            return itemScore;            

        }
        public List<RiskMatrisEntity> GetAverageRiskSkorColorList(List<double> skor)
        {
            List<RiskMatrisEntity> result = new List<RiskMatrisEntity>();
            var matrix = new RiskMatrisBusiness().GetMatrixAsList();
            foreach (var item in skor)
            {
                result.Add(RiskMatrisBusiness.GetMatixStore(matrix, item));
                
            }
            return result;

        }
    }
}
