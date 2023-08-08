using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTMenuCommand : MenuCommand, ISupportsPermission
    {
        public string Permission { get; set; }

    }
}
