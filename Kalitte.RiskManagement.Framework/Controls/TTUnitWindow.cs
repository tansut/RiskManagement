using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.UI;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTUnitWindow : TTWindow
    {        
        protected override void OnInit(EventArgs e)
        {
              base.OnInit(e);              
              this.ID = "unitSelectWindow";
              this.Title = "Birim Seç";
              this.Width = new System.Web.UI.WebControls.Unit(400);
              this.Height = new System.Web.UI.WebControls.Unit(250);
              this.AutoScroll = true;
              this.BodyBorder = false;
              this.Layout = "fit";              
        }
    }
}
