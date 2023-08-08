/*  ----------------------------------------------------------------------------
 *  Kalitte Professional Information Technologies
 *  ----------------------------------------------------------------------------
 *  Dynamic Dashboards
 *  ----------------------------------------------------------------------------
 *  File:       HtmlWidget.ascx.cs
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.Dashboard.Framework;
using Kalitte.RiskManagement.Framework.Business.Management;
using Ext.Net;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.UnitSelect
{
    public partial class SelectorWidget : System.Web.UI.UserControl, IWidgetControl
    {
        internal DashboardSurface surface;
        internal WidgetInstance instance;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //ctlSelector.OnFilter += new EventHandler(ctlSelector_OnFilter);
            //ctlSelectUnits.NavigateUrl = string.Format("javascript:Ext.getCmp('{0}').show();", ctlSelector.WindowClientID);

        }

        void ctlSelector_OnFilter(object sender, EventArgs e)
        {
            var cmd = new WidgetCommandInfo() { CommandName = CommandConstant.Refresh };
            surface.SendCommand(this, cmd);
        }

        public void Bind(WidgetInstance instance)
        {
            //ctlSelector.Bind();
            var units = UnitFilterManager.GetActiveUnits();
            var bll = new UnitBusiness();
            var data = bll.RetreiveUnitHierarchyName(units);
            ctlUnitList.DataSource = data;
            ctlUnitList.DataBind();
        }


       




        public UpdatePanel[] Command(WidgetInstance instance, WidgetCommandInfo cmd, ref UpdateMode updateMode)
        {
            switch (cmd.CommandType)
            {
                case WidgetCommandType.Refresh:
                    Bind(instance);
                    return new UpdatePanel[] { ctlUpdatePanel };
                case WidgetCommandType.SettingsChanged:
                    {
                        Bind(instance);
                        return new UpdatePanel [] { ctlUpdatePanel };
                    }
                default: return null;
            }

        }


        public void InitControl(WidgetInitParameters parameters)
        {
            surface = parameters.Surface;
            instance = parameters.Instance;
            surface.DashboardContentsUpdated += new EventHandler(surface_DashboardContentsUpdated);
        }

        void surface_DashboardContentsUpdated(object sender, EventArgs e)
        {

        }



    
    }
}
