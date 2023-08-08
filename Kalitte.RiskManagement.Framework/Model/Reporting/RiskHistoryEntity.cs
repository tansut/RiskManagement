using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Model.Reporting
{
    public class RiskHistoryEntity: StatEntity
    {
        public DateTime ApproveDate
        {
            get
            {
                return new DateTime(Year, Month, 1);
            }
        }

        public int Year { get; set; }
        public int Month { get; set; }

        public string DateName
        {
            get {
                return ApproveDate.ToString("yyyy,MMM");
            }

        }

        public RiskHistoryEntity()
            : base()
        {
        }
    }
}
