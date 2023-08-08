using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Reporting;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class QuestionAnswerReportBusiness : ReportingBusiness<SoruCevapEntity>
    {
        public List<SoruCevapEntity> RetreiveSoruCevap(int soruId, ListingParameters listingParams = null)
        {
            IQueryable<SoruCevapEntity> q;
            var cq = DataContext.RiskSoruCevap.Where(p => p.SoruID == soruId);
            cq = PermissionQueryFor<RiskSoruCevap>(cq, listingParams);

            q = from cevap in cq
                group cevap by new { cevap.Cevap.ID, cevap.Cevap.Ad } into grp
                select new SoruCevapEntity() { Name = grp.Key.Ad, Value = grp.Count(), SoruID = soruId };

            return ExecuteListQuery(q);
        }

    }
}
