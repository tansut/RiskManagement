using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Providers;
using System.Data.SqlClient;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class RiskGecmisBusiness : EntityBusiness<RiskGecmis>
    {
        public void CreateRiskHistory(Risk RiskEntity)
        {
            RiskGecmis entity = new RiskGecmis();
            entity.RiskID = RiskEntity.ID;
            entity.ArtikEtki = RiskEntity.ArtikEtki.Value;
            entity.ArtikOlasilik = RiskEntity.ArtikOlasilik.Value;
            entity.ArtikSkor = RiskEntity.ArtikSkor.Value;
            entity.ArtikEtkiSkorTanim = RiskEntity.ArtikEtkiSkorTanim;
            entity.ArtikOlasilikSkorTanim = RiskEntity.ArtikOlasilikSkorTanim;
            entity.ArtikSkorTanim = RiskEntity.ArtikSkorTanim;
            entity.ArtikSkorRenk = RiskEntity.ArtikSkorRenk;
            this.InsertSingle(entity);
            DataContext.SaveChanges();
        }

        public List<RiskHistoryReportData> RetreiveHistoryItems(Core.ListingParameters lp)
        {
            var riziko = new RiskBusiness().GetFilteredQuery(DataContext.Risk, lp).Select(s => s.ID).Distinct().ToList();          
           return  GetQueryable().Where(w => riziko.Contains(w.RiskID)).ToList().Select(s => new RiskHistoryReportData()
            {
                RiskID = s.RiskID,
                RiskAd = s.Risk.Ad,
                SurecAd = s.Risk.Surec.Ad,
                OnayTarihi = s.KayitTarih,
                ArtikEtki = s.ArtikEtkiFullInfo,
                ArtikOlasilik = s.ArtikOlasilikFullInfo,
                ArtikSkor = s.ArtikSkorFullInfo,
                ArtikSkorRenk = s.ArtikSkorRenk,
            }).ToList();
        }

        public List<IllerData> GetGecmisRiskCountBySkorTanim(string skorGrupDeger)
        {
            return DataContext.ExecuteStoreQuery<IllerData>("exec GetMapSkorData @skorTanim",new SqlParameter("skorTanim",skorGrupDeger)).ToList();
        }

        public List<RiskGecmis> GetHistorybyRiskID(int riskid)
        {
            return GetQueryable().Where(p => p.RiskID == riskid).ToList();
        }
    }
}
