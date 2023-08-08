using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTCityNameColumn: TTColumn
    {
        public TTCityNameColumn()
            : base()
        {
            Header = "IL";
            Width = new System.Web.UI.WebControls.Unit(100);
            DataIndex = "CityName";
        }
    }
}
