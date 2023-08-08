using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.Dashboard.Framework;
using System.Threading;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Controls;
using System.Web.UI;
using Kalitte.RiskManagement.Framework.Core;


namespace Kalitte.RiskManagement.Framework.UI
{
    public abstract class BaseDashboardPage : BasePage
    {
        private IUnitSelector unitSelect;

        private void createUnitWindow()
        {
            Control unitSelector = LoadControl("~/Controls/Widgets/UnitSelect/UnitSelectWindow.ascx");
            unitSelect = unitSelector as IUnitSelector;
            unitSelector.ID = this.ID + "selector";
            (unitSelector as ISupportsMultiplePostbackTypes).PostbackType = PostbackType.AspNet;
            unitSelect.OnFilter += new EventHandler(unitSelect_OnFilter);
        }

        void unitSelect_OnFilter(object sender, EventArgs e)
        {
            var cmd = new WidgetCommandInfo() { CommandName = CommandConstant.Refresh };
            Dashboard.SendCommand(sender, cmd);
        }



        private void CopyDashboard(string defaultDashboard)
        {


            DashboardInstance dashboard = DashboardFramework.GetDashboard(defaultDashboard);
            List<WidgetInstance> widgets = DashboardFramework.GetWidgetInstances(defaultDashboard);

            Guid newDashbaord = Guid.NewGuid();
            dashboard.InstanceKey = newDashbaord;

            foreach (var row in dashboard.Rows)
            {
                row.DashboardInstanceKey = dashboard.InstanceKey;
                Guid oldKey = (Guid)row.InstanceKey;
                row.InstanceKey = Guid.NewGuid();
                foreach (var widgetInstance in widgets)
                {
                    widgetInstance.DashboardKey = dashboard.InstanceKey;
                    if ((Guid)widgetInstance.RowKey == oldKey)
                        widgetInstance.RowKey = row.InstanceKey;
                }
            }

            DashboardFramework.CreateDashboard(dashboard);
            foreach (var widgetInstance in widgets)
                DashboardFramework.CreateWidget(widgetInstance);


        }

        public bool ShowUserDashboardsMenu
        {
            get
            {
                return true;
            }
        }

        public WidgetViewMode GetViewMode(DashboardSurface dashboard)
        {

            if (Thread.CurrentPrincipal.Identity.Name == dashboard.CurrentInstance.Username)
                return WidgetViewMode.Edit;
            else
            {
                bool specialDashboards = dashboard.CurrentInstance.UserTag == "MERKEZANASAYFA" || dashboard.CurrentInstance.UserTag == "BIRIMANASAYFA";
                bool userGlobal = UserBusiness.UserHasGlobalRights();
                if (userGlobal && specialDashboards)
                    return WidgetViewMode.Edit;
                else return Thread.CurrentPrincipal.Identity.Name == dashboard.CurrentInstance.Username ? WidgetViewMode.Edit : WidgetViewMode.Browse;
            }


        }

        protected virtual void BindDashboard()
        {
            Dashboard.DataBind();
            SetDashboardView(Dashboard);


        }


        protected override void OnLoad(EventArgs e)
        {
            if (Dashboard != null)
                try
                {
                    if (!Page.IsPostBack)
                    {
                        BindDashboard();
                        unitSelect.Bind();
                    }
                }
                catch (ArgumentException) // Ivalid dashboardKey
                {
                    throw;
                }

            base.OnLoad(e);
        }


        protected virtual void SetDashboardView(DashboardSurface dashboard)
        {
            if (dashboard != null)
            {
                dashboard.ViewMode = GetViewMode(dashboard);
                dashboard.AddtoColumnText = "&nbsp;&nbsp;<b>Ekle</b>&nbsp;";
                dashboard.CreatatingWidgetMask = "Oluşturuluyor ...";
                dashboard.CreateWidgetsButtonText = "Widget Ekle";
                dashboard.WidgetTypePanelTitle = "Eklemek istediklerinizi işaretleyin";


            }

        }

        protected virtual void SetDashboardProperties(DashboardSurface dashboard)
        {

            if (dashboard != null)
            {
                dashboard.DashboardToolbarPrepare += new DashboardToolbarPrepareHandler(dashboard_DashboardToolbarPrepare);
                dashboard.WidgetPropertiesSetting += new WidgetHandler(Dashboard_WidgetPropertiesSetting);
                dashboard.DashboardCommand += new DashboardCommandHandler(Dashboard_DashboardCommand);
                dashboard.DefaultDashboardViewUrl = Page.ResolveUrl("~/pages/dynamic/dashboard.aspx?d={0}");
                dashboard.DashboardMenuPrepare += new DashboardMenuPrepareEventHandler(dashboard_DashboardMenuPrepare);
                dashboard.DashboardPropertiesSetting += new WidgetDashboardHandler(dashboard_DashboardPropertiesSetting);
                dashboard.ShowDashboardListPanel = false;
                dashboard.CreateInSectionText = "{0}. Bölüm";
            }
        }





        void dashboard_DashboardPropertiesSetting(object sender, DashboardEventArgs e)
        {
            e.Instance.WidgetCreateAnimation = "fadeIn({ endOpacity: 1, easing: 'easeIn', duration: 2 })";
            e.Instance.WidgetCommandAnimation = "frame('ff0000', 1, { duration: 1 })";
            e.Instance.WidgetDropAnimation = "fadeIn({ endOpacity: 1, easing: 'easeIn', duration: 0.5 })";
            e.Instance.DashboardUpdateAnimation = "frame('C3DAF9', 1, { duration: 1 })";

            if (Dashboard.ViewMode == WidgetViewMode.Browse)
            {
                e.Instance.BodyBorder = false;
                e.Instance.Border = false;
            }

        }


        void dashboard_DashboardMenuPrepare(object sender, DashboardMenuPrepareEventArgs e)
        {
            var items = (sender as DashboardSurface).GetDefaultDashboardMenuItems();
            var userItems = items.Where(p => p.Instance.Username == (sender as DashboardSurface).GetUsername()).ToList();
            items = items.Where(p => p.Instance.Username != (sender as DashboardSurface).GetUsername()).ToList();
            var userMenu = new List<DashboardMenuItemData>(userItems.Count + 5);
            string lastGroup = "";
            int userDisplayOrder = 0;
            foreach (var userDashboard in userItems)
            {
                if (!string.IsNullOrEmpty(userDashboard.Group) && lastGroup != userDashboard.Group)
                {
                    var groupItem = new DashboardMenuItemData(userDashboard.Instance, userDashboard.Instance.ViewMode);
                    groupItem.RenderMode = DashboardMenuItemRenderMode.TextMenuItem;
                    groupItem.DisplayTitle = string.Format("<b class='menu-title'>{0}</b>", userDashboard.Instance.Group);
                    groupItem.Group = "Gösterge Panelleriniz";
                    groupItem.DisplayOrder = userDisplayOrder++;
                    groupItem.GroupDisplayOrder = int.MaxValue;
                    userMenu.Add(groupItem);
                }
                lastGroup = userDashboard.Group;
                userDashboard.Group = "Gösterge Panelleriniz";
                userDashboard.GroupDisplayOrder = int.MaxValue;
                userDashboard.DisplayOrder = userDisplayOrder++;
                userMenu.Add(userDashboard);
            }

            e.List.AddRange(items);
            e.List.AddRange(userMenu);

        }

        void Dashboard_DashboardCommand(object sender, DashboardCommandArgs e)
        {
            if (e.Command.CommandName == "EditDashboards")
            {
                Response.Redirect(Page.ResolveClientUrl(string.Format("/Pages/Dynamic/EditDashboards.aspx?d={0}", e.Command.Arguments["key"])));
            }
        }

        void dashboard_DashboardToolbarPrepare(object sender, DashboardToolbarPrepareEventArgs e)
        {

            if ((sender as DashboardSurface).ViewMode == WidgetViewMode.Edit)
            {
                e.Toolbar.AddItem(new DashboardToolbarSeperator("ctlSeperator1"));
                DashboardToolbarButton btnEdit = new DashboardToolbarButton("ctlEditDashboard")
                {
                    Text = "Gösterge Panelini Düzenle",
                    Icon = WidgetIcon.PageEdit,
                    CommandName = "EditDashboards",
                    Hint = "<b>Edit</b><br/>Edit dashboard.",
                    MaskMessage = "Düzenleniyor..."
                };
                btnEdit.Arguments.Add("key", e.Instance.InstanceKey.ToString());

                e.Toolbar.AddItem(btnEdit);
                e.Toolbar.AddItem(new DashboardToolbarSeperator("ctlSeperator2"));
            }
            DashboardToolbarButton btnUnitWindow = new DashboardToolbarButton("ctlViewUnitWindow")
            {
                Text = "Birim Filtresi",
                Icon = WidgetIcon.PageFind,
                FunctionName = string.Format("Ext.getCmp('{0}').show();", (unitSelect as IWindowControl).WindowClientID),
                Hint = "<b>Birim Seçim Penceresi</b>",
                CommandName = "showUnitWindow"
            };

            e.Toolbar.AddItem(btnUnitWindow);
            e.Toolbar.AddItem(new DashboardToolbarSeperator("ctlSeperator3"));

            string seperator = Request.QueryString.HasKeys() ? "&" : "?";

            DashboardToolbarButton btnNewWindow = new DashboardToolbarButton("ctlNewUnitWindow")
            {
                Text = "Yeni Pencere",
                Icon = WidgetIcon.NewspaperGo,
                FunctionName = string.Format("window.open('{0}{1}r={2}');", Request.Url.PathAndQuery, seperator, new Random().Next()),
                CommandName = "openInNewWindow"
            };

            e.Toolbar.AddItem(btnNewWindow);



        }

        protected virtual bool UseQueryStringForDashboardKey
        {
            get
            {
                return false;
            }
        }


        protected override void OnInit(EventArgs e)
        {
            createUnitWindow();
            this.Form.Controls.Add(unitSelect as Control);

            if (Dashboard == null)
                base.OnInit(e);

            else if (UseQueryStringForDashboardKey)
            {
                string key = Request["d"];
                bool keyValid = false;
                if (!string.IsNullOrEmpty(key))
                {
                    try
                    {
                        DashboardInstance d = DashboardFramework.GetDashboard(key);
                        keyValid = d != null;
                    }
                    catch
                    {
                        keyValid = false;
                    }
                }
                else keyValid = false;
                if (keyValid)
                    Dashboard.DashboardKey = key;
                else
                {
                    Response.Redirect("~/", true);
                }
            }
            if (!string.IsNullOrEmpty(Request["Birims"]))
            {
                var units = Request["Birims"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                HashSet<int> set = new HashSet<int>();
                int birimID;
                foreach (var item in units)
                {
                    if (int.TryParse(item, out birimID))
                    {
                        set.Add(birimID);
                    }
                }
                UnitFilterManager.SetActiveUnits(set);
            }

            SetDashboardProperties(Dashboard);
            base.OnInit(e);
        }




        void Dashboard_WidgetPropertiesSetting(object sender, WidgetEventArgs e)
        {
            if (((DashboardSurface)sender).ViewMode == WidgetViewMode.Browse)
            {
                if (e.Instance.HeaderDisplayMode == WidgetHeaderDisplayMode.MouseEnter)
                {
                    e.Instance.HeaderDisplayMode = WidgetHeaderDisplayMode.Always;
                    e.Instance.Header = false;
                }
            }
            else
            {

                //if (e.Instance.WidgetSettings.ContainsKey("__set0"))
                //    e.Instance.PanelSettings.Header = false;
                //if (e.Instance.WidgetSettings.ContainsKey("__set1"))
                //{
                //    e.Instance.PanelSettings.HeaderDisplayMode = WidgetHeaderDisplayMode.Always;
                //}
            }
        }

        protected abstract DashboardSurface Dashboard
        {
            get;
        }
    }

}
