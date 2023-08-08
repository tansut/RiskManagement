using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Model.Reporting
{
    public class RiskSkorEntity
    {
        public int BirimID { get; set; }
        public string Skor { get; set; }
        public double SkorValue { get; set; }
        public int Count { get; set; }
        public int SurecID { get; set; }

        //public string Etki { get; set; }
        //public string Olasilik { get; set; }
        //public int EtkiValue { get; set; }
        //public int OlasilikValue { get; set; }

        public RiskSkorEntity()
        {
           
        }
    }
}
