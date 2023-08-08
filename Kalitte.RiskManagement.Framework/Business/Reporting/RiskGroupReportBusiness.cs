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
    public class RiskGroupReportBusiness : ReportingBusiness<StatEntity>
    {
        public List<StatEntity> RetrieveRiskGroup(ListingParameters listingParams = null)
        {
            IQueryable<StatEntity> q;
            var cq = PermissionQueryFor<RiskGrup>(DataContext.RiskGrup, listingParams);

            q = from riskgrup in cq
                group riskgrup by new { riskgrup.RiskGrupTanim.ID, riskgrup.RiskGrupTanim.Ad } into grp
                select new StatEntity() { Name = grp.Key.Ad, Value = grp.Count() };

            return ExecuteListQuery(q);
        }
    }
}
