using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTTabPanel : TabPanel
    {

        public TTTabPanel()
            : base()
        {
            BodyBorder = false;
            BodyStyle = "background-color:transparent;";
            EnableTabScroll = true;
        }

        public override bool LayoutOnTabChange
        {
            get
            {
                return true;
            }
            set
            {
                base.LayoutOnTabChange = true;
            }
        }

        public override int ActiveIndex
        {
            get
            {
                return base.ActiveIndex;
            }
            set
            {
                base.ActiveIndex = value;
                
            }
        }


        public override bool DeferredRender
        {
            get
            {
                return false;
            }
            set
            {
                base.DeferredRender = false;
            }
        }
    }
}
