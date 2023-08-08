using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Model
{
    public partial class Surec
    {

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

        public bool DosyaEkExists
        {
            get
            {
                return new DosyaEkBusiness().GetDosyaEk(this.ID, DosyaKayitTip.Surec.ToString()).Count > 0;
            }
        }
    }
}
