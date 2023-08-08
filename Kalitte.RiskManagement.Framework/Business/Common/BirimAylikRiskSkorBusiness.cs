using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Model.Reporting;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class BirimAylikRiskSkorBusiness : EntityBusiness<BirimAylikRiskSkor>
    {

        public void CreateBirimAylikSkor()
        {
            var now = DateTime.Now;
            var context = NewDataContext();

            var q = (from riskgrup in context.Risk
                     where riskgrup.ArtikSkor.HasValue && riskgrup.Surec.Aktif
                     group riskgrup by new { riskgrup.BirimID } into grp

                     select new { BirimID = grp.Key.BirimID, Sum = grp.Sum(o => o.ArtikSkor.Value), Adet = grp.Count() }).ToList();
            foreach (var birim in q)
            {
                try
                {
                    var entity = new BirimAylikRiskSkor();
                    entity.Yil = now.Year;
                    entity.Ay = now.Month;
                    entity.BirimID = birim.BirimID;
                    entity.Skor = birim.Sum;
                    entity.Toplam = birim.Adet;
                    context.BirimAylikRiskSkor.AddObject(entity);
                    context.SaveChanges();
                }
                catch (UpdateException ex)
                {
                    SqlException innerException = ex.InnerException as SqlException;
                    if (innerException != null && innerException.Number == 2627)
                    {
                        var entity = context.BirimAylikRiskSkor.SingleOrDefault(p => p.Yil == now.Year && p.Ay == now.Month && p.BirimID == birim.BirimID);
                        if (entity != null)
                        {
                            entity.Yil = now.Year;
                            entity.Ay = now.Month;
                            entity.BirimID = birim.BirimID;
                            entity.Skor = birim.Sum;
                            entity.Toplam = birim.Adet;
                            context.SaveChanges();
                        }
                    }
                    else throw;                    
                }
            }

        }
    }
}
