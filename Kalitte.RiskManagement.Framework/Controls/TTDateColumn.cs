using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTDateColumn : Ext.Net.DateColumn
    {
        public TTDateColumn()
        {
            Width = new System.Web.UI.WebControls.Unit(75);
            Fixed = true;
            Format = "dd.MM.yyyy";
        }
    }
}
