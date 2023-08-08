using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework;
using Kalitte.Dashboard.Framework.Types;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public abstract partial class BaseDataList : System.Web.UI.UserControl, IWidgetControl
    {
        protected DashboardSurface surface;
        protected WidgetInstance instance;

        protected abstract void BindData(WidgetInstance instance);        
        protected abstract UpdatePanel ThisUpdatePanel { get; }


        protected void Page_Load(object sender, EventArgs e)
        {            
        }

                protected bool IsMaximized
        {
            get
            {
                return ViewState["max"] != null;
            }
            set
            {
                if (value)
                    ViewState["max"] = value;
                else
                {
                    ViewState["max"] = null;
                    MaximizedHeight = 0;
                    MaximizedWidth = 0;
                }
            }
        }

        protected int MaximizedWidth
        {
            get
            {
                return ViewState["MaximizedWidth"] == null ? 0 : (int)ViewState["MaximizedWidth"];
            }
            set
            {
                if (value == 0)
                    ViewState["MaximizedWidth"] = null;
                else ViewState["MaximizedWidth"] = value;
            }
        }

        protected int MaximizedHeight
        {
            get
            {
                return ViewState["MaximizedHeight"] == null ? 0 : (int)ViewState["MaximizedHeight"];
            }
            set
            {
                if (value == 0)
                    ViewState["MaximizedHeight"] = null;
                else ViewState["MaximizedHeight"] = value;
            }
        }


        public UpdatePanel[] Command(WidgetInstance instance, Kalitte.Dashboard.Framework.WidgetCommandInfo commandData, ref UpdateMode updateMode)
        {
            if (commandData.CommandType == WidgetCommandType.Refresh || commandData.CommandType == WidgetCommandType.SettingsChanged)
            {
                Bind(instance);
                return new UpdatePanel[] { ThisUpdatePanel };
            }
            else if (commandData.CommandType == WidgetCommandType.Maximized)
            {
                IsMaximized = true;
                MaximizedWidth = int.Parse(commandData.Arguments["width"].ToString());
                MaximizedHeight = int.Parse(commandData.Arguments["height"].ToString());
                Bind(instance);
                return new UpdatePanel[] { ThisUpdatePanel };
            }
            else if (commandData.CommandType == WidgetCommandType.Restored)
            {
                IsMaximized = false;
                Bind(instance);
                return new UpdatePanel[] { ThisUpdatePanel };
            }
            else return null;
        }

        public void Bind(WidgetInstance instance)
        {
            BindData(instance);
            ViewState["instance"] = instance.InstanceKey;
        }

        

        public void InitControl(WidgetInitParameters parameters)
        {
            surface = parameters.Surface;
            instance = parameters.Instance;       
        }

       
    }
}