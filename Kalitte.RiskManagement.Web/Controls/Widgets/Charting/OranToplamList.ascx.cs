using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class OranToplamList : BaseDataList
    {
        protected override void BindData(Dashboard.Framework.Types.WidgetInstance instance)
        {
            
        }
        protected override UpdatePanel ThisUpdatePanel
        {
            get { return this.up; }
        }
    }
}