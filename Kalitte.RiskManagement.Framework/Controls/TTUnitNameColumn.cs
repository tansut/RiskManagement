using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTUnitNameColumn: TTColumn
    {
        public TTUnitNameColumn()
            : base()
        {
            DataIndex = "UnitFullName";
            Header = "Birim";
        }
    }
}
