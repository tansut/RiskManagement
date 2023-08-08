using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Reporting;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class RiskReportBusiness : ReportingBusiness<StatEntity>
    {
        public List<StatEntity> RetreiveRiskStatus(ListingParameters listingParams = null)
        {
            IQueryable<StatEntity> q;
            var cq = PermissionQueryFor<Risk>(DataContext.Risk, listingParams); 
        
                q = from risk in cq
                    where risk.Surec.Aktif
                    group risk by new { risk.Durum } into grp
                    select new StatEntity() { Name = grp.Key.Durum, Value = grp.Count() };

            return ExecuteListQuery(q);
        }

        public List<StatEntity> RetreiveRiskSayiGroupByUnit(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            return cq.Where(w => w.ArtikSkor.HasValue && w.Surec.Aktif).GroupBy(p => p.BirimID).Select(g => new StatEntity() { BirimID = g.Key, Name = g.Select(u => u.Birim.Ad).FirstOrDefault(), Value = g.Count() }).OrderByDescending(z => z.Value).ToList();
        }

        public List<RiskSkorEntity> RetreiveRiskPuanGroupByUnit(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Risk>(DataContext.Risk, listingParams);
            return cq.Where(w => w.ArtikSkor.HasValue && w.Surec.Aktif).GroupBy(p => p.BirimID).Select(g => new RiskSkorEntity() { BirimID = g.Key, Skor = g.Select(u => u.Birim.Ad).FirstOrDefault(), SkorValue = Math.Round(g.Average(u => u.ArtikSkor ?? 0), 2) }).OrderBy(z => z.SkorValue).ToList();

        }
    }
}
