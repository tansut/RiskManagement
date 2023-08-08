using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Model
{
    public partial class Soru
    {
        [Relation("SoruGrupTanim.Ad")]
        public string GrupAd
        {
            get
            {
                return this.SoruGrupTanim.Ad;
            }
        }

        [Relation("SoruGrupTanim.GrupTur")]
        public string GrupTurAd
        {
            get
            {
                
                return SoruGrupTanim.GrupTur;
            }
        }


        public SoruGrupTur GrupTur
        {
            get
            {
                return (SoruGrupTur)Enum.Parse(typeof(SoruGrupTur), this.SoruGrupTanim.GrupTur);
            }
        }
    }
}
