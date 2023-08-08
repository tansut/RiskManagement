using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Framework.Model
{
    public partial class SoruGrupTanim
    {
        public SoruGrupTur Tur
        {
            get
            {
                return (SoruGrupTur)Enum.Parse(typeof(SoruGrupTur), this.GrupTur);
            }
        }
    }
}
