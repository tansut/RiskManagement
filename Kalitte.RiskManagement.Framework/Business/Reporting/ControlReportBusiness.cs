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
    public class ControlReportBusiness : ReportingBusiness<StatEntity>
    {
        public List<StatEntity> RetreiveKontrolParameter(string p, ListingParameters listingParams = null)
        {
            IQueryable<StatEntity> q;
            var cq = PermissionQueryFor<Kontrol>(DataContext.Kontrol, listingParams);
            cq = cq.Where(w => w.Risk.ArtikSkor.HasValue);
            switch (p)
            {
                case "Tip":
                    q = from kontrol in cq 
                        where kontrol.Risk.Surec.Aktif
                        group kontrol by new { kontrol.Tip } into grp
                        select new StatEntity() { Name = grp.Key.Tip, Value = grp.Count() };
                    break;
                case "Isleyis":
                    q = from kontrol in cq
                        where kontrol.Risk.Surec.Aktif
                        group kontrol by new { kontrol.Isleyis } into grp
                        select new StatEntity() { Name = grp.Key.Isleyis, Value = grp.Count() };
                    break;
                case "Siklik":
                    q = from kontrol in cq
                        where kontrol.Risk.Surec.Aktif
                        group kontrol by new { kontrol.Siklik } into grp
                        select new StatEntity() { Name = grp.Key.Siklik, Value = grp.Count() };
                    break;
                default:
                    q = null;
                    break;
            }

            return ExecuteListQuery(q);
        }
    }
}
