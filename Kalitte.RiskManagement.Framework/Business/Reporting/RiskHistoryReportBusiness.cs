using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Reporting;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class RiskHistoryReportBusiness : ReportingBusiness<RiskHistoryEntity>
    {
        public List<RiskHistoryEntity> RetreiveHistory(ListingParameters listingParams = null)
        {
            IQueryable<RiskHistoryEntity> q;
            var cq = PermissionQueryFor<RiskGecmis>(DataContext.RiskGecmis, listingParams);

            q = from risk in DataContext.RiskGecmis
                orderby risk.KayitTarih
                group risk by new { risk.ArtikSkorTanim, risk.KayitTarih.Year, risk.KayitTarih.Month } into grp
                select new RiskHistoryEntity { Name = grp.Key.ArtikSkorTanim, Value = grp.Count(), Year = grp.Key.Year, Month = grp.Key.Month };

            return ExecuteListQuery(q);
        }


    }
}
