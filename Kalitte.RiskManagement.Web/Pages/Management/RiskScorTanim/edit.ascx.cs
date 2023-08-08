using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Business.Common;
using Ext.Net;

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.RiskScorTanim
{
    public partial class edit : EditorViewControl<RiskScoreBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Risk Score Tanımı";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.EtkiBaslik = ctlEtkiBaslik.Text;
            entity.OlasilikBaslik = ctlOlasilikBaslik.Text;
            entity.EtkiAciklama = ctlEtkiAciklama.Text;
            entity.OlasilikAciklama = ctlOlasilikAciklama.Text;
            entity.Deger = ctlDeger.ValueAsInt;
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();

        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.EtkiBaslik);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlEtkiBaslik.Text = entity.EtkiBaslik;
            ctlOlasilikBaslik.Text = entity.OlasilikBaslik;
            ctlEtkiAciklama.Text = entity.EtkiAciklama;
            ctlOlasilikAciklama.Text = entity.OlasilikAciklama;
            ctlDeger.ValueAsInt = entity.Deger;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            //var entity = new Kalitte.RiskManagement.Framework.Business.Common.RiskMatrisBusiness();
            var entity = new Framework.Model.RiskSkorTanim();
            entity.EtkiBaslik = ctlEtkiBaslik.Text;
            entity.OlasilikBaslik = ctlOlasilikBaslik.Text;
            entity.EtkiAciklama = ctlEtkiAciklama.Text;
            entity.OlasilikAciklama = ctlOlasilikAciklama.Text;
            entity.Deger = ctlDeger.ValueAsInt;
            BusinessObject.InsertSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.DeleteEntity)]
        public void DeleteEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.DeleteSingle(entity);
            CurrentLister.LoadItems();
        }
    }
}