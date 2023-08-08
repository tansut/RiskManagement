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
    public partial class View : System.Web.UI.UserControl, IUnitSelector
    {

        protected override void OnInit(EventArgs e)
        {
            if (!UserBusiness.UserHasGlobalRights())
                ctlUnitTree.Root.Primary.NodeID = AuthenticationManager.CurrentUserBirimID.ToString();
            base.OnInit(e);
        }


        protected void NodeLoad(object sender, NodeLoadEventArgs e)
        {
            string prefix = e.ExtraParams["prefix"] ?? "";

            if (!string.IsNullOrEmpty(e.NodeID))
            {
                var data = new UnitBusiness().GetSubUnits(int.Parse(e.NodeID));
                foreach (var item in data)
                {
                    AsyncTreeNode asyncNode = new AsyncTreeNode();
                    asyncNode.Text = item.Ad;
                    asyncNode.NodeID = item.ID.ToString();
                    asyncNode.Checked = UnitFilterManager.IsUnitSelected(item.ID) ? ThreeStateBool.True: ThreeStateBool.False;
                    //if (asyncNode.Checked == ThreeStateBool.True)
                    //ctlUnitTree.FireEvent("checkchange", asyncNode.NodeID);

                    e.Nodes.Add(asyncNode);
                }
            }
        }



        internal void doFilter()
        {
            if (OnFilter != null)
                OnFilter(this, EventArgs.Empty);
        }

        internal HashSet<int> UpdatedUnits
        {
            get
            {
                string[] list = ctlSelectedNodes.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var allUnits = new HashSet<int>(list.Select(p => int.Parse(p)).ToArray());
                ctlSelectedNodes.Value = "";
                return allUnits;

            }
        }




        public event EventHandler OnFilter;



        public void Bind()
        {
            ctlUnitTree.SelectNode(ctlUnitTree.Root.Primary.NodeID);
            ctlUnitTree.AddScript(string.Format("{0}.getRootNode().expand();", ctlUnitTree.ClientID));
        }

    }
}
