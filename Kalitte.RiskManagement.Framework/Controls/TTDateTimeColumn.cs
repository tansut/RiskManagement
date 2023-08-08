using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTDateTimeColumn : Ext.Net.DateColumn
    {
        public TTDateTimeColumn()
        {
            Format = "dd.MM.yyyy hh:mm:ss";
        }
    }
}
