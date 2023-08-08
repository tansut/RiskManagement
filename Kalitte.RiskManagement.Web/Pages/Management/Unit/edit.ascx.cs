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
using Kalitte.RiskManagement.Framework.Controls;

namespace Kalitte.RiskManagement.Web.Pages.Management.Unit
{

    public partial class edit : EditorViewControl<UnitBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AutoCompleteCityHandler(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new CityBusiness().RetreiveItems(e.Parameters);
        }

        protected void AutoCompleteBirimHandler(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UnitBusiness().RetreiveItems(e.Parameters);
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            ctlParent.Hidden = true;
            ctlBirim.Hidden = false;
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlBirim.Initialize(entity.Birim2.ID, entity.Birim2.FullUnitName);
            ctlName.Text = entity.Ad;
            ctlAciklama.Text = entity.Aciklama;
            ctlParent.Text = entity.Birim2 == null ? "" : entity.Birim2.Ad;
            ctlCity.Value = entity.IL.Ad;
            ctlCity.Initialize(entity.IL.ID, entity.IL.Ad);
            ctlVirtual.Checked = entity.Sanal;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Birim Tanımla";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            if (command.RecordID == 0)
            {
                ctlParent.Text = "";
                CurrentID = 0;
            }
            else
            {
                var parent = BusinessObject.Retrieve(command.RecordID);
                ctlParent.Text = parent.Ad;
                CurrentID = command.RecordID;
                ctlCity.Value = parent.IL.Ad;
                ctlCity.Initialize(parent.IL.ID, parent.IL.Ad);
            }
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Ad = ctlName.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.ILID = ctlCity.SelectedAsInt;
            entity.Sanal = ctlVirtual.Checked;
            entity.UstBirimID = ctlBirim.SelectedAsInt;
            if (entity.UstBirimID == entity.ID)
                throw new BusinessException("Birimin üst birimi kendisi olamaz.");
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.Birim();
            entity.Ad = ctlName.Text;
            entity.Aciklama = ctlAciklama.Text;
            if (CurrentID != 0)
                entity.UstBirimID = CurrentID;
            entity.ILID = ctlCity.SelectedAsInt;
            entity.Sanal = ctlVirtual.Checked;
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