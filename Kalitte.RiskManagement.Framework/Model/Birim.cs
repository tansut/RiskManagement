using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Model
{
    public partial class Birim
    {
        private string fullUnitName = null;

        private void setUnitFullname(Birim birim)
        {
            StringBuilder sb = new StringBuilder();
            if (!birim.UstBirimID.HasValue)
                birim.FullUnitName = birim.Ad;
            else
            {
                setUnitFullname(birim.Birim2);
                birim.FullUnitName = birim.Birim2.FullUnitName + "/" + birim.Ad;
            }
        }

        public string FullUnitName
        {
            get
            {
                if (string.IsNullOrEmpty(fullUnitName))
                    setUnitFullname(this);
                return fullUnitName;
            }
            private set
            {
                fullUnitName = value;
            }
        }

        [Relation("IL.Ad")]
        public string CityName
        {
            get
            {
                return this.IL.Ad;
            }
        }
    }
}
