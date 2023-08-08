using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.Dashboard.Framework;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.RiskMap
{
    public partial class view : System.Web.UI.UserControl, IWidgetControl
    {
        private DashboardSurface surface;
        private WidgetInstance instance;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void InitControl(WidgetInitParameters parameters)
        {
            surface = parameters.Surface;
            instance = parameters.Instance;
            surface.DashboardContentsUpdated += new EventHandler(surface_DashboardContentsUpdated);


        }

        void surface_DashboardContentsUpdated(object sender, EventArgs e)
        {
            if (Page.IsPostBack && ViewState["instance"] != null)
            {
                WidgetInstance instance = DashboardFramework.GetWidgetInstance(ViewState["instance"]);
                Bind(instance);
            }
        }

        #region IWidgetControl Members

        public void Bind(WidgetInstance instance)
        {
            ctlRiskMatrix.Store.Primary.DataSource = new TTRiskScoreStore().GetRows();
            ctlRiskMatrix.Store.Primary.DataBind();
            ViewState["instance"] = instance.InstanceKey.ToString();
            if (Page.IsPostBack)
                Ext.Net.ResourceManager.GetInstance().AddUpdatePanelToRefresh(ctlUp);            
        }

        public UpdatePanel[] Command(WidgetInstance instance, Dashboard.Framework.WidgetCommandInfo cmd, ref UpdateMode updateMode)
        {
            switch (cmd.CommandType)
            {
                case WidgetCommandType.Refresh:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { ctlUp };
                    }
                case WidgetCommandType.SettingsChanged:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { ctlUp };
                    }
                default: return null;
            }

        }



        #endregion
    }
}