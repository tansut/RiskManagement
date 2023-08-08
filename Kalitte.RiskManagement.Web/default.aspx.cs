using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Kalitte.RiskManagement.Web;
using Kalitte.Dashboard.Framework.Types;
using System.Threading;
using Ext.Net;
using System.Web.Security;
using Kalitte.RiskManagement.Web.Controls.Site;
using Kalitte.Dashboard.Framework;
using System.Configuration;
using Kalitte.RiskManagement.Framework.Business;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Business.Management;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Kalitte.BI.Shared;
using System.Text;
using System.Web.Script.Serialization;
using Kalitte.RiskManagement.Framework.Utility;

namespace Kalitte.RiskManagement.Web
{
    public partial class _default : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                var duyurulist = new DuyuruBusiness().GetCurrentAnnouncements();
                string duyurutext = string.Empty;
                foreach (var item in duyurulist)
                {
                    duyurutext += string.Format("<b>Duyuru:  {0}</b> <br><br> {1} <br> <br> <br> <br>", item.Baslik, item.Icerik);
                }

                if (duyurulist.Any())
                    X.MessageBox.Show(new MessageBoxConfig() { Buttons = MessageBox.Button.OK, Message = duyurutext, Title = "Duyurular" });

                SiteMapNode siteNode = SiteMap.RootNode;
                TreeNode root = this.CreateNode(siteNode);
                ManageNodes(root, root);
                AddUserDashboards(root);
                AddSharedDashboards(root);
                ctlMenuTree.Root.Add(root);
                ResourceManager.GetInstance(this).AddScript(string.Format("loadPage(Ext.getCmp('{0}'), Ext.getCmp('{1}').getRootNode()); ", ExampleTabs.ClientID, ctlMenuTree.ClientID));
                ResourceManager.GetInstance(this).AddScript(string.Format("Ext.getCmp('{0}').expandAll();", ctlMenuTree.ClientID));
                ctlVersion.Text = string.Format("Build {0}", ConfigurationManager.AppSettings["ProductVersion"]);
            }

            string action = Request["action"];
            if (action == "filterData" && !string.IsNullOrEmpty(Request["filterData"]))
            {
                string pd = Request["filterData"];
                byte[] arr = Convert.FromBase64String(pd);
                var filter = Encoding.Default.GetString(arr);
                filter = HttpUtility.UrlDecode(filter);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                FilterSelectData selectData = serializer.Deserialize<FilterSelectData>(filter);
                Session["SelectData-" + selectData.Key] = selectData;

                string tabTitle, url;

                switch (selectData.FilterType)
                {
                    case "Risk":
                        {
                            tabTitle = "Risk Değerlendirme";
                            url = "~/Pages/Risk/default.aspx";
                            break;
                        }
                    case "Süreç":
                        {
                            tabTitle = "Süreç ve Risk Tanımları";
                            url = "~/Pages/Surec/default.aspx";
                            break;
                        }
                    default:
                        {
                            tabTitle = "";
                            url = "";
                            break;
                        }
                }
                if (!string.IsNullOrEmpty(url))
                {
                    string script = string.Format("openPageAsTab('{0}?selectDataKey={1}','{2}');", Page.ResolveUrl(url), HttpUtility.UrlEncode(selectData.Key.ToString()), tabTitle);
                    ResourceManager.GetInstance(this).AddScript(script);
                    ResourceManager.GetInstance(this).AddScript(string.Format("Ext.getCmp('{0}').collapse();", Panel2.ClientID));
                }
            }
        }


        private void AddSharedDashboards(TreeNode root)
        {
            List<DashboardInstance> list;
            list = DashboardFramework.GetDashboards(DashboardShareType.AllUsers).Select(p => p).ToList();
            bool userGlobal = UserBusiness.UserHasGlobalRights();
            if (list.Count > 0)
            {
                TreeNode node = new TreeNode("Ortak Paneller");
                root.Nodes.Add(node);
                foreach (DashboardInstance instance in list)
                {
                    if (!userGlobal && (instance.UserTag == "MERKEZANASAYFA" || instance.UserTag == "BIRIMANASAYFA"))
                        continue;
                    TreeNode dNode = new TreeNode(instance.Title, (Icon)Enum.Parse(typeof(Icon), instance.Icon.ToString()));
                    dNode.Href = ResolveClientUrl(string.Format("~/Pages/Dynamic/ViewDashboard.aspx?d={0}", instance.InstanceKey));
                    node.Nodes.Add(dNode);
                }
            }
        }

        private void AddUserDashboards(TreeNode root)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated && !Thread.CurrentPrincipal.IsInRole(RoleConstants.Auditor) && (UserBusiness.UserHasGlobalRights() || Thread.CurrentPrincipal.IsInRole(RoleConstants.UnitWorkflowManager)))
            {
                List<DashboardInstance> list = DashboardFramework.GetDashboards(Thread.CurrentPrincipal.Identity.Name);
                TreeNode node = new TreeNode("Panellerim");
                root.Nodes.Add(node);
                foreach (DashboardInstance instance in list)
                {
                    TreeNode dNode = new TreeNode(instance.Title, (Icon)Enum.Parse(typeof(Icon), instance.Icon.ToString()));
                    dNode.Href = ResolveClientUrl(string.Format("~/Pages/Dynamic/ViewDashboard.aspx?d={0}", instance.InstanceKey));
                    node.Nodes.Add(dNode);
                }
                if (list.Count == 0)
                {
                    TreeNode dNode = new TreeNode("Düzenle", Icon.PageEdit);
                    dNode.Href = ResolveClientUrl(string.Format("~/Pages/Dynamic/EditDashboards.aspx"));
                    node.Nodes.Add(dNode);
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CreateChildControlsDynamicControls();
        }

        protected void Logoff(object sender, DirectEventArgs e)
        {
            Session.RemoveAll();
            Session.Abandon();
            Session.Clear(); 
            AuthenticationManager.Logout();
        
        }

        private MenuItem createThemeMenuItem(Theme theme, string title)
        {
            MenuItem i = new MenuItem(title);
            if (ResMan.Theme == theme)
                i.Icon = Icon.Tick;
            i.CustomConfig.Add(new ConfigItem("theme", "'" + ResMan.GetThemeUrl(theme) + "'", ParameterMode.Value));
            i.Listeners.Click.Handler = "changeTheme(el)";
            return i;
        }


        private void CreateChildControlsDynamicControls()
        {

            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                if (!X.IsAjaxRequest)
                {
                    authenticatedUserToolbar.Visible = true;
                    anonymousUserToolbar.Visible = false;
                    var user = UserBusiness.GetUser();
                    ctlUserInfo.Text = string.Format("{0}, {1} ({2})", Thread.CurrentPrincipal.Identity.Name, user.AdSoyad, user.UnvanAd);
                    ctlBirimInfo.Text = user.BirimAd;
                }
                dynamicControls.Controls.Add(ChangeUserPasswordControl.LoadInto(this));
            }
            else
            {
                dynamicControls.Controls.Add(LoginWindowControl.LoadInto(this));
                if (!X.IsAjaxRequest)
                {
                    authenticatedUserToolbar.Visible = false;
                    anonymousUserToolbar.Visible = true;
                }
            }

        }

        [DirectMethod]
        public string RefreshMenu()
        {
            SiteMapNode siteNode = SiteMap.RootNode;
            TreeNode root = this.CreateNode(siteNode);
            ManageNodes(root, root);
            AddUserDashboards(root);
            AddSharedDashboards(root);
            Ext.Net.TreeNodeCollection nodes = new TreeNodeCollection() { root };
            return nodes.ToJson();
        }

        [DirectMethod]
        public string GetThemeUrl(string theme)
        {
            Theme temp = (Theme)Enum.Parse(typeof(Theme), theme);

            this.Session["Ext.Net.Theme"] = temp;

            return (temp == Ext.Net.Theme.Default) ? "Default" : ResMan.GetThemeUrl(temp);
        }

        [DirectMethod]
        public static int GetHashCode(string s)
        {
            return Math.Abs(("/Pages" + s).ToLower().GetHashCode());
        }

        private void ManageNodes(TreeNode root, TreeNode node)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                TreeNode n = node.Nodes[i] as TreeNode;

                for (int j = 0; j < root.Nodes.Count + 3; j++)
                {
                    ManageNodes(root, n);
                    if (n.ParentNode != null && n.Nodes.Count == 0 && n.Href == "#")
                        node.Nodes.Remove(n);
                }

            }
            if (root.Nodes.Count == 1 && (root.Nodes[0] as TreeNode).Nodes.Count == 0)
                root.Nodes.RemoveAt(0);
        }

        private TreeNode CreateNode(SiteMapNode siteMapNode)
        {
            TreeNode treeNode = new TreeNode();
            if (!string.IsNullOrEmpty(siteMapNode.Url) && !siteMapNode.Url.StartsWith("/x"))
            {
                treeNode.Href = this.Page.ResolveUrl(siteMapNode.Url);
            }
            treeNode.NodeID = siteMapNode.Key;
            treeNode.Text = siteMapNode.Title;

            if (!string.IsNullOrEmpty(siteMapNode.Description))
            {
                string[] parts = siteMapNode.Description.Split(':');
                if (parts.Length == 2)
                {
                    //treeNode.Qtip = parts[1];
                    treeNode.Icon = (Icon)Enum.Parse(typeof(Icon), parts[1]);
                }
                //else treeNode.Icon = (Icon)Enum.Parse(typeof(Icon), siteMapNode.Description);
            }

            SiteMapNodeCollection children = siteMapNode.ChildNodes;
            if (children != null && children.Count > 0)
            {
                foreach (SiteMapNode mapNode in siteMapNode.ChildNodes)
                {
                    TreeNode newNode = this.CreateNode(mapNode);

                    if (userHasRight(mapNode))
                        treeNode.Nodes.Add(newNode);
                }
            }

            return treeNode;
        }

        private bool userHasRight(SiteMapNode mapNode)
        {
            if (mapNode.Roles.Count == 0)
                return true;
            foreach (string role in mapNode.Roles)
            {
                if (Roles.IsUserInRole(role))
                    return true;
            }
            return false;
        }




    }
}