using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Reporting;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Framework.Business.Reporting
{
    public class RiskSkorReportBusiness : ReportingBusiness<RiskSkorEntity>
    {
        public List<RiskSkorEntity> RetrieveRiskParameters(string p, ListingParameters listingParams = null)
        {
            IQueryable<RiskSkorEntity> q;
            var cq = PermissionQueryFor<Risk>(DataContext.Risk, listingParams);

            switch (p)
            {
                case "ArtikRiskSkor":
                    q = from risk in cq
                        where risk.ArtikSkor.HasValue && risk.Surec.Aktif
                        group risk by new { risk.ArtikSkorTanim, risk.ArtikSkor } into grp
                        orderby grp.Count() descending
                        select new RiskSkorEntity() { Skor = grp.Key.ArtikSkorTanim, SkorValue = grp.Key.ArtikSkor.Value, Count = grp.Count() };
                    break;

                case "ArtikRiskEtki":
                    q = from risk in cq
                        where risk.ArtikSkor.HasValue && risk.Surec.Aktif
                        group risk by new { risk.ArtikEtkiSkorTanim, risk.ArtikEtki } into grp
                        orderby grp.Count() descending
                        select new RiskSkorEntity() { Skor = grp.Key.ArtikEtkiSkorTanim, SkorValue = grp.Key.ArtikEtki.Value, Count = grp.Count() };
                    break;

                case "ArtikRiskOlasilik":
                    q = from risk in cq
                        where risk.ArtikSkor.HasValue && risk.Surec.Aktif
                        group risk by new { risk.ArtikOlasilikSkorTanim, risk.ArtikOlasilik } into grp
                        orderby grp.Count() descending
                        select new RiskSkorEntity() { Skor = grp.Key.ArtikOlasilikSkorTanim, SkorValue = grp.Key.ArtikOlasilik.Value, Count = grp.Count() };
                    break;

                default:
                    q = null;
                    break;
            }

            return ExecuteListQuery(q);
        }

        public List<RiskSkorEntity> GetTopSurecWithRisk(ListingParameters listingParams = null)
        {
            var cq = PermissionQueryFor<Risk>(DataContext.Risk, listingParams);
          
            return cq.Where(d => d.Surec.Aktif && d.ArtikSkor > 0).OrderByDescending(d => d.ArtikSkor).Take(5).Select(s => new RiskSkorEntity() { SurecID = s.Surec.ID, Skor ="<b>"+ s.Surec.Ad+"</b><br>"+s.Ad, SkorValue = s.ArtikSkor.HasValue ? s.ArtikSkor.Value:0 }).ToList();
        }
    }
}
