using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;
using System.Threading;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Framework.Model
{

    public partial class Risk
    {
        public RiskDurum RiskDurum
        {
            get
            {
                return (Common.RiskDurum)Enum.Parse(typeof(RiskDurum), this.Durum);
            }
            set
            {
                this.Durum = value.ToString();
            }
        }

        [Relation("Surec.Ad")]
        public string SurecAd
        {
            get
            {
                return Surec.Ad;
            }
        }

        [Relation("Birim.Ad")]
        public string BirimAd
        {
            get
            {
                return Birim.Ad;
            }
        }

        public string SurecBirim
        {
            get
            {
                return string.Format("{0} - {1}", Surec.Ad, Birim.Ad);
            }
        }

        public RiskKullaniciDurum KullaniciDurum
        {
            get
            {
                return GetUserStatus(Thread.CurrentPrincipal.Identity.Name);
            }
        }

        public RiskKullaniciDurum GetUserStatus(string userName)
        {
            var user = new UserBusiness().RetreiveByUsername(userName);
            var userInGroup = this.CalismaGrupTanim.CalismaGrupKullanici.Any(p => p.KatilimciKullaniciID == user.UserId);
            if (!userInGroup)
                return RiskKullaniciDurum.GrupDışı;
            return this.RiskSoruCevap.Any(p => p.KullaniciID == user.UserId) ? RiskKullaniciDurum.PuanlamaTamamlandı : RiskKullaniciDurum.PuanlamaBekler;
        }


        public bool HasSkor
        {
            get
            {
                return this.ArtikEtki != null;
            }
        }

        public void ClearSkor()
        {
            this.ArtikEtki = null;
            this.ArtikOlasilik = null;
            this.ArtikSkor = null;
            this.ArtikSkorRenk = string.Empty;
            this.ArtikSkorTanim = string.Empty;
            this.ArtikEtkiSkorTanim = string.Empty;
            this.ArtikOlasilikSkorTanim = string.Empty;
        }

        [Relation("ArtikSkor")]
        public string ArtikSkorFullInfo
        {
            get
            {
                var result = Math.Round(this.ArtikSkor ?? 0, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.ArtikSkorTanim);
            }
        }

      
        public string ArtikEtkiFullInfo
        {
            get
            {
                var result = Math.Round(this.ArtikEtki ?? 0, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.ArtikEtkiSkorTanim);
            }
        }

        public string ArtikOlasilikFullInfo
        {
            get
            {
                var result = Math.Round(this.ArtikOlasilik ?? 0, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.ArtikOlasilikSkorTanim);
            }
        }

        public List<string> Calisanlar
        {
            get
            {
                return this.CalismaGrupTanim.CalismaGrupKullanici.Select(p => p.AdSoyad).ToList();
            }
        }

        public string RiskKullanici
        {
            get
            {
                return this.aspnet_Users.AdSoyad;
            }
        }

    }

}
