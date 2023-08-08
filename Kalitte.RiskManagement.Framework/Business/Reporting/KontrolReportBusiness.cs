using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Reporting;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class KontrolReportBusiness : ReportingBusiness<StatEntity>
    {
        public List<StatEntity> RetreiveKontrolSayiGroupByUnit(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Kontrol>(DataContext.Kontrol, listingParams);
            return cq.Where(w => w.Risk.ArtikSkor.HasValue && w.Risk.Surec.Aktif).GroupBy(p => p.Risk.BirimID).Select(g => new StatEntity() { BirimID = g.Key, Name = g.Select(u => u.Birim.Ad).FirstOrDefault(), Value = g.Count() }).OrderByDescending(z => z.Value).ToList();
        }
    }
}
