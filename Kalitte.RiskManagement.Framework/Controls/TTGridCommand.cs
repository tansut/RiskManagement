using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Core;


namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTGridCommand : GridCommand, ISupportsPermission
    {
        public bool Confirm { get; set; }
        public bool UseDblClick { get; set; }
        private bool? isVisible = null;
        public string Permission { get; set; }


        public TTGridCommand()
        {
            
        }

        public override ConfigOptionsCollection ConfigOptions
        {
            get
            {
                ConfigOptionsCollection coll = base.ConfigOptions;
                coll.Remove("confirm");
                coll.Remove("useDblClick");
                if (Confirm)
                {
                    coll.Add("confirm", false, true);
                }
                if (UseDblClick)
                    coll.Add("useDblClick", false, true);
                return coll;
            }
        }
    }
}
