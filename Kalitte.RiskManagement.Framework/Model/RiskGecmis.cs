using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Model
{
    public partial class RiskGecmis
    {
        [Relation("Risk.Ad")]
        public string RiskAd
        {
            get
            {
                return this.Risk.Ad;
            }
        }

        [Relation("ArtikSkor")]
        public string ArtikSkorFullInfo
        {
            get
            {
                var result = Math.Round(this.ArtikSkor, 2);
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
                var result = Math.Round(this.ArtikEtki, 2);
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
                var result = Math.Round(this.ArtikOlasilik, 2);
                if (result.Equals(0))
                {
                    return "-";
                }
                else
                    return string.Format("{0} - {1}", result, this.ArtikOlasilikSkorTanim);
            }
        }
    }
}
