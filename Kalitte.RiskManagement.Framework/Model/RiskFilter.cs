using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Common;
using Kalitte.RiskManagement.Framework.Utility;

namespace Kalitte.RiskManagement.Framework.Model
{
    [Serializable]
    public class RiskFilter
    {
        public List<int> ArtikSkors { get; set; }
        public List<int> ArtikEtkiSkors { get; set; }
        public List<int> ArtikOlasilikSkors { get; set; }
        public RiskDurum? RiskDurum { get; set; }

        public RiskFilter()
        {
            ArtikSkors = new List<int>();
            ArtikEtkiSkors = new List<int>();
            ArtikOlasilikSkors = new List<int>();            
        }


        public bool HasItems ()
        {
            return ArtikSkors.Any() || ArtikEtkiSkors.Any() || ArtikOlasilikSkors.Any() || RiskDurum.HasValue;
        }
        public override string ToString()
        {
            return SerializationHelper.JSONSerialize(this);
        }

        public static RiskFilter FromString(string s)
        {
            return (RiskFilter)SerializationHelper.JSONDeserialize(s, typeof(RiskFilter));
        }
    }
}
