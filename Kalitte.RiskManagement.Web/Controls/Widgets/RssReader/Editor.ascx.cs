/*  ----------------------------------------------------------------------------
 *  Kalitte Professional Information Technologies
 *  ----------------------------------------------------------------------------
 *  Dynamic Dashboards
 *  ----------------------------------------------------------------------------
 *  File:       Editor.ascx.cs
 *  ----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;

namespace Kalitte.WidgetLibrary.RssReader
{
    public partial class Editor : System.Web.UI.UserControl, IWidgetEditor
    {
        public void Edit(object instanceKey)
        {
            ViewState["Key"] = instanceKey;
            WidgetInstance instance = Kalitte.Dashboard.Framework.DashboardFramework.GetWidgetInstance(ViewState["Key"]);
            if (instance.WidgetSettings.ContainsKey("RssUrl"))
            {
                ctlRss.Text = instance.WidgetSettings["RssUrl"].ToString();
                ctlInterval.Text = instance.WidgetSettings["Interval"].ToString();
                ctlShowImg.Checked = (bool)instance.WidgetSettings["ShowImg"];
                ctlDesc.Checked = (bool)instance.WidgetSettings["ShowBody"]; ;
                if (instance.WidgetSettings.ContainsKey("MaxChar"))
                    ctlMaxChar.Text = (string)instance.WidgetSettings["MaxChar"];
                else ctlMaxChar.Text = "";
            }
            
        }

        public bool EndEdit(Dictionary<string, object> arguments)
        {
            if (Page.IsValid)
            {
                
                
                    WidgetInstance instance = Kalitte.Dashboard.Framework.DashboardFramework.GetWidgetInstance(ViewState["Key"]);
                    instance.WidgetSettings["RssUrl"] = ctlRss.Text;
                    instance.WidgetSettings["Interval"] = int.Parse(ctlInterval.Text);
                    instance.WidgetSettings["ShowImg"] = ctlShowImg.Checked;
                    instance.WidgetSettings["ShowBody"] = ctlDesc.Checked;
                    instance.WidgetSettings["MaxChar"] = ctlMaxChar.Text;
                    Kalitte.Dashboard.Framework.DashboardFramework.UpdateWidget(instance);
                    return true;

            }
            else return false;
        }
    }
}
