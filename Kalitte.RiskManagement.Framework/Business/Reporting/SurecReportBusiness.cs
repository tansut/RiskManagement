using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Reporting;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class SurecReportBusiness : ReportingBusiness<StatEntity>
    {
        public List<StatEntity> RetreiveSurecSayiGroupByUnit(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.Surec>(DataContext.Surec, listingParams);
            return cq.Where(w => w.Aktif).GroupBy(p => p.BirimID).Select(g => new StatEntity() { BirimID = g.Key, Name = g.Select(u => u.Birim.Ad).FirstOrDefault(), Value = g.Count() }).OrderByDescending(z => z.Value).ToList();
        }
    }
}
