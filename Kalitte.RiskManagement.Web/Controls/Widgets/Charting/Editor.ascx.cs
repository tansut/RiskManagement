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
using Kalitte.Dashboard.Framework;
using Kalitte.WidgetLibrary.Charting;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{
    public partial class Editor : System.Web.UI.UserControl, IWidgetEditor
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region IWidgetEditor Members

        public void Edit(object instanceKey)
        {
            ViewState["Key"] = instanceKey;
            WidgetInstance instance = DashboardFramework.GetWidgetInstance(instanceKey);
            ChartSettings settings = instance.SerializedData as ChartSettings;
            
            ctlChartSettings.DoEdit(instance, settings);

            ////ctlCharts.DataSource = RepositoryManager.GetCharts();
            //ctlCharts.DataBind();
            //if (settings != null && settings.ChartKey != null)
            //    ctlCharts.SelectedValue = settings.ChartKey.ToString();
        }

        public bool EndEdit(Dictionary<string, object> arguments)
        {
            if (Page.IsValid)
            {
                
                    WidgetInstance instance = DashboardFramework.GetWidgetInstance(ViewState["Key"]);
                    ChartSettings settings = ctlChartSettings.EndEdit(instance, null);
                    //settings.ChartKey = new Guid(ctlCharts.SelectedValue);
                    instance.SerializedData = settings;
                    DashboardFramework.UpdateWidget(instance);
                    return true;
 
            }
            else return false;
        }

        #endregion
    }
}
