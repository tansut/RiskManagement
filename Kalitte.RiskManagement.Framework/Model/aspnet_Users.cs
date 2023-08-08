using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Model
{
    public partial class aspnet_Users
    {
        [Relation("Unvan.Ad")]
        public string UnvanAd
        {
            get
            {
                return this.Unvan == null ? "" : Unvan.Ad;

            }
        }

        

        public long TCKimlikNo
        {
            get
            {
                return long.Parse(UserName);
            }

            set
            {
                UserName = value.ToString();
            }
        }

        [Relation("Birim.Ad")]
        public string BirimAd
        {
            get
            {
                return this.Birim == null ? "" : Birim.Ad;

            }
        }

        [Relation("Birim.Ad")]
        public string UnitFullName
        {
            get
            {
                return this.Birim == null ? "" : Birim.FullUnitName;

            }
        }

        [Relation("Birim.IL.Ad")]
        public string CityName
        {
            get
            {
                return this.Birim == null ? "" : Birim.IL.Ad;

            }
        }

        [Relation("Ad")]
        public string AdSoyad
        {
            get
            {
                return string.Format("{0} {1}", Ad, Soyad);

            }
        }

        [Relation("aspnet_Roles.RoleName")]
        public string RolAd
        {
            get
            {
                return string.Join(", ",Roles.GetRolesForUser(this.UserName));
            }
        }

    }
}
