using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class KullaniciPuanlamaDurum
    {
        private Guid userid;
        private string adsoyad;
        private string durum;

        public Guid UserID
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }

        public string AdSoyad
        {
            get
            {
                return adsoyad;
            }
            set
            {
                adsoyad = value;
            }
        }

        public string Durum
        {
            get
            {
                return durum;
            }
            set
            {
                durum = value;
            }
        }

    }
    public class CalismaGrupKullaniciBusiness : EntityBusiness<CalismaGrupKullanici>
    {
        public List<KullaniciPuanlamaDurum> GetCalismaGrupDurum(int riskid)
        {
            var calismagrupid = new RiskBusiness().Retrieve(riskid).CalisanGrupID;
            var users = GetQueryable().Where(p => p.CalismaGrupID == calismagrupid).Select(u => u.KatilimciKullaniciID).ToList();
            var PuanlamaDurumList = new List<KullaniciPuanlamaDurum>();
            foreach (var item in users)
            {
                KullaniciPuanlamaDurum PuanlamaDurum = new KullaniciPuanlamaDurum();
                var user = new UserBusiness().GetQueryable().Where(p => p.UserId == item).Single();
                PuanlamaDurum.UserID = item;
                PuanlamaDurum.AdSoyad = user.AdSoyad;
                if (new RiskQuestionAnswerBusiness().GetQueryable().Where(p => p.RiskID == riskid && p.KullaniciID == item).Any())
                    PuanlamaDurum.Durum = RiskKullaniciDurum.PuanlamaTamamlandı.ToString();
                else
                    PuanlamaDurum.Durum = RiskKullaniciDurum.PuanlamaBekler.ToString();
                PuanlamaDurumList.Add(PuanlamaDurum);
            }

            return PuanlamaDurumList;
        }

        public void RemoveUserFromGroup(aspnet_Users user)
        {
            var list = GetQueryable().Where(p => p.KatilimciKullaniciID == user.UserId).ToList();
            DeleteCollection(list);
        }

        public bool CheckAllUserAnswers(Risk risk)
        {
            var userlist = GetQueryable().Where(p => p.aspnet_Users.Disabled==null &&  p.CalismaGrupID == risk.CalisanGrupID).Select(u => u.KatilimciKullaniciID).Distinct().ToList();
                if (userlist.Contains(risk.KullaniciID))
                    userlist.Remove(risk.KullaniciID);
            var useranswers = new RiskQuestionAnswerBusiness().GetQueryable().Where(p => p.aspnet_Users.Disabled==null && p.RiskID == risk.ID).Select(s => s.KullaniciID).Distinct().ToList();
            return !(userlist.Except(useranswers).ToList().Count > 0);
        }
    }
}
