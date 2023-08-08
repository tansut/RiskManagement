using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Web.Pages.Management.Unit
{
    public partial class list : ListerViewControl<UnitBusiness>
    {
        protected KnownCommand lastCommand
        { 
            get
            {
                return ViewState["lc"] == null ? KnownCommand.None : (KnownCommand)ViewState["lc"];
            }
            set
            {
                ViewState["lc"] = value;
            }
        }

        protected override RiskManagement.Framework.Controls.TTStore LocateListerStore()
        {
            return null;
        }

        protected void EnsureSelection(object sender, CommandInfo e)
        {
            var selectedNode = (ctlUnitTree.SelectionModel.Primary as DefaultSelectionModel).SelectedNode;
            if (selectedNode == null)
                throw new BusinessException("Lütfen seçim yapınız");
            e.RecordID = int.Parse(selectedNode.NodeID);
            lastCommand = e.KnownCommand;
        }

        public override void LoadItems()
        {
            var selectedNode = (ctlUnitTree.SelectionModel.Primary as DefaultSelectionModel).SelectedNode;
            if (selectedNode != null)
            {
                if (lastCommand == KnownCommand.CreateInEditor)
                {
                    ctlUnitTree.ReloadAsyncNode(selectedNode.NodeID, new JFunction());
                    ctlUnitTree.ExpandNode(selectedNode.NodeID);
                }
                else if (lastCommand == KnownCommand.EditInEditor)
                {
                    var unit = BusinessObject.Retrieve(int.Parse(selectedNode.NodeID));
                    ctlUnitTree.SetNodeText(selectedNode.NodeID, unit.Ad);
                }

                else if (lastCommand == KnownCommand.DeleteEntity && selectedNode.NodeID != "0")
                {
                    ctlUnitTree.RemoveNode(selectedNode.NodeID);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                ctlUnitTree.SelectNode("0");
                ctlUnitTree.AddScript(string.Format("{0}.getRootNode().expand();", ctlUnitTree.ClientID));
            }
        }


        [CommandHandler(CommandName = "AddSubUnit")]
        protected void AddSubUnitCommandHandler(object sender, CommandInfo cmd)
        {
            var selectedNode = (ctlUnitTree.SelectionModel.Primary as DefaultSelectionModel).SelectedNode;
            var birim = BusinessObject.RetreiveOrDefault(int.Parse(selectedNode.NodeID));

            ctlGenForm.ClearFields();
            if (birim != null)
                entityWindow.Title = string.Format("Alt Birim Ekle: {0}", birim.Ad);
            entityWindow.Show();
        }

        protected void NodeLoad(object sender, NodeLoadEventArgs e)
        {
            string prefix = e.ExtraParams["prefix"] ?? "";

            if (!string.IsNullOrEmpty(e.NodeID))
            {
                var data = BusinessObject.GetSubUnits(int.Parse(e.NodeID)).OrderBy(p => p.Sira);
               
                foreach (var item in data)
                {
                    AsyncTreeNode asyncNode = new AsyncTreeNode();
                    asyncNode.Text = item.Ad;
                    asyncNode.NodeID = item.ID.ToString();
                    e.Nodes.Add(asyncNode);
                }
            }
        }

        public override ViewControlType ControlType
        {
            get { return ViewControlType.Lister; }
        }

        [CommandHandler(CommandName = "SaveSubUnits")]
        public void SaveSubUnitsCommandHandler(object sender, CommandInfo cmd)
        {
            var selectedNode = (ctlUnitTree.SelectionModel.Primary as DefaultSelectionModel).SelectedNode;

            var unitbll = new UnitBusiness();
            var SubUnits = new List<Birim>();
            if (int.Parse(selectedNode.NodeID) > 0)
                SubUnits = unitbll.GetSubUnits(int.Parse(selectedNode.NodeID));
            else
                SubUnits = unitbll.GetRootUnits();

            foreach (var item in SubUnits)
            {
                Birim birim = new Birim();
                birim.Ad = ctlAltBirim.Text;
                birim.UstBirimID = item.ID;
                birim.ILID = item.ILID;
                birim.Sanal = false;
                unitbll.Insert(birim);
            }

            unitbll.DataContext.SaveChanges();
            entityWindow.Hide();
        }
    }
}