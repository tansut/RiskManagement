using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Reporting;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class BirimAylikRiskSkorReportBusiness : ReportingBusiness<StatEntity>
    {
        public List<RiskSkorEntity> GetAverageRiskSkor(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Kalitte.RiskManagement.Framework.Model.BirimAylikRiskSkor>(DataContext.BirimAylikRiskSkor, listingParams);
            var result = cq.OrderBy(o => o.Yil).ThenBy(t => t.Ay).GroupBy(g => new { g.Yil, g.Ay }).Select(s => new { Year = s.Key.Yil, Month = s.Key.Ay, Value = s.Sum(u => u.Skor) / s.Sum(u=>u.Toplam) }).ToList();
            return result.Select(s => new RiskSkorEntity() { Skor = string.Format("{0}/{1}", s.Month, s.Year), SkorValue = Math.Round(s.Value, 2) }).ToList();
        }
    }
}
