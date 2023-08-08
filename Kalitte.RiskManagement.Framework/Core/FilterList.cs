using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Core
{
    public enum FilterListOperator
    {
        And,
        Or
    }

    public class FilterList: List<FilteringInfo>
    {
        public FilterListOperator Operator { get; set; }

        public FilterList() {
            Operator = FilterListOperator.And;
        }
    }
}
