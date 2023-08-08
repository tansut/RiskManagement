using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTUserColumn: TTColumn
    {
        public TTUserColumn()
            : base()
        {
            DataIndex = "Kullanici";
            Width = new System.Web.UI.WebControls.Unit(90);
            Header = "Kullanıcı";
        }
    }
}
