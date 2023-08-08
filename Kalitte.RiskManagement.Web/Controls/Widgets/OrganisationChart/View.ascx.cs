using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Web.Controls.Widgets.OrganisationChart
{
    public partial class View : System.Web.UI.UserControl, IWidgetControl
    {
        private DashboardSurface surface;
        private WidgetInstance instance;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

   




        public void Bind(WidgetInstance instance)
        {
            this.CreateHierarchyList();
        }

        private void CreateHierarchyList()
        {
            if (rootTree.Controls.Count > 0)
                rootTree.Controls.RemoveAt(0);
            UnitBusiness ub = new UnitBusiness();
            var units = ub.AllUnits;              
            var currentUserBirimID = !UserBusiness.UserHasGlobalRights() ? AuthenticationManager.CurrentUserBirimID : 2;
            var root = units.Where(p => p.ID == currentUserBirimID).Single();
            var countBaseSub = units.Where(p => p.UstBirimID == currentUserBirimID).Count();          
            HtmlGenericControl rootList = new HtmlGenericControl("ul");
            rootList.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            rootList.ID = "primaryNav";
            rootList.Attributes.Add("style", string.Format("width:{0}px;", countBaseSub*124));
            //rootList.Attributes.Add("class", "col20");
            HtmlGenericControl rootItem = CreateSubListItem(root.Ad, root.ID.ToString());
            rootItem.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            rootItem.ID = "home";
            CreateSubs(units, rootItem, root.ID);
            rootList.Controls.Add(rootItem);
            rootTree.Controls.Add(rootList);
        }

        private void CreateSubs(List<Framework.Model.Birim> units, HtmlGenericControl rootListItem, int id)
        {
            var subs = units.Where(p => p.UstBirimID == id).ToList();
            if (subs.Any())
            {
                if (id != (!UserBusiness.UserHasGlobalRights() ? AuthenticationManager.CurrentUserBirimID : 2))
                {
                    HtmlGenericControl rootList = new HtmlGenericControl("ul");
                    foreach (var item in subs)
                    {
                        HtmlGenericControl rootItem = CreateSubListItem(item.Ad, item.ID.ToString());
                        CreateSubs(units, rootItem, item.ID);
                        rootList.Controls.Add(rootItem);
                    }
                    rootListItem.Controls.Add(rootList);
                }
                else
                {
                    foreach (var item in subs)
                    {
                        HtmlGenericControl rootItem = CreateSubListItem(item.Ad, item.ID.ToString());
                        CreateSubs(units, rootItem, item.ID);
                        rootListItem.Controls.Add(rootItem);
                    }
                }

            }
        }
        private HtmlGenericControl CreateSubListItem(string ad, string id)
        {
            HtmlGenericControl rootItem = new HtmlGenericControl("li");
            //rootItem.Attributes.Add("style", string.Format("width:{0}%;", defaultColWidth.ToString("0.00", CultureInfo.InvariantCulture)));
            rootItem.Attributes.Add("style", "width:124px;");    
            HtmlGenericControl anchor = new HtmlGenericControl("a");
            anchor.Attributes.Add("onclick", string.Format("openPageAsTab('{0}?Birims={1}','{2}');", Page.ResolveUrl("~/Reports/Pages/BirimKarne.aspx"), id, ad));
            anchor.InnerHtml = ad;
            rootItem.Attributes.Add("birimID", id);
            rootItem.Controls.Add(anchor);
            return rootItem;
        }

        public UpdatePanel[] Command(WidgetInstance instance, Dashboard.Framework.WidgetCommandInfo commandData, ref UpdateMode updateMode)
        {
            switch (commandData.CommandType)
            {
                case WidgetCommandType.Refresh:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { UpdatePanel };
                    }
                case WidgetCommandType.Restored:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { UpdatePanel };
                    }
                default: return null;
            }
        }

        public void InitControl(Dashboard.Framework.WidgetInitParameters parameters)
        {
            this.surface = parameters.Surface;
            this.instance = parameters.Instance;
            this.surface.DashboardContentsUpdated += surface_DashboardContentsUpdated;
        }

        void surface_DashboardContentsUpdated(object sender, EventArgs e)
        {
            this.CreateHierarchyList();
        }
    }
}