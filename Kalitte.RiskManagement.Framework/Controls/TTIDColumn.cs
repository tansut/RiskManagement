using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTIDColumn: TTColumn
    {
        public TTIDColumn()
            : base()
        {
            Header = "#";
            Width = new System.Web.UI.WebControls.Unit(50);
            DataIndex = "ID";
        }
    }
}
