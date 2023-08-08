using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTWindow: Window
    {
        public TTWindow()
            : base()
        {
            Collapsible = false;
            Hidden = true;
            Maximizable = true;
        }
    }
}
