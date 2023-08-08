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
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.Charting
{

    public partial class SoruCevapEditor : System.Web.UI.UserControl, IWidgetEditor
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
            
            ctlSoru.DataSource = new QuestionBusiness().RetreiveEntityItems();
            ctlSoru.DataBind();

            if (instance.WidgetSettings.ContainsKey("SoruID"))
                ctlSoru.SelectedValue = instance.WidgetSettings["SoruID"].ToString();

            ctlChartSettings.DoEdit(instance, settings);
        }

        public bool EndEdit(Dictionary<string, object> arguments)
        {
            if (Page.IsValid)
            {

                WidgetInstance instance = DashboardFramework.GetWidgetInstance(ViewState["Key"]);
                ChartSettings settings = ctlChartSettings.EndEdit(instance, null);
                instance.SerializedData = settings;
                if (!instance.WidgetSettings.ContainsKey("SoruID") && string.IsNullOrEmpty(settings.Title))
                    settings.Title = ctlSoru.SelectedItem.Text;
                instance.WidgetSettings["SoruID"] = ctlSoru.SelectedValue;
                DashboardFramework.UpdateWidget(instance);
                return true;

            }
            else return false;
        }

        #endregion
    }
}
