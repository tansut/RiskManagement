using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Model.Reporting
{
    public class StatEntity
    {
        public int BirimID { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }

        public StatEntity()
        {
        }
    }
}
