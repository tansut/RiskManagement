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
using System.Web.Security;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.Soru
{
    public partial class edit : EditorViewControl<QuestionBusiness>
    {
        List<Cevap> CurrentBindings
        {
            get
            {
                if (ViewState["cb"] == null)
                    return new List<Cevap>();
                return (List<Cevap>)ViewState["cb"];
            }

            set
            {
                ViewState["cb"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.Net.X.IsAjaxRequest)
            {
                ctlGroupId.Store.Primary.DataSource = new QuestionGroupDefinitionBusiness().RetreiveItems();
                ctlGroupId.Store.Primary.DataBind();
            }
        }

        protected void GridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {

            if (e.Command == "delete")
            {
                var bindings = CurrentBindings;
                bindings.RemoveAt(e.RowIndex);
                CurrentBindings = bindings;
                ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
                ctlCurrentAnswersGrid.Store.Primary.DataBind();
            }
            else if (e.Command == "decrement")
            {
                var bindings = CurrentBindings;
                var currentIndex = e.RowIndex;
                if (currentIndex != 0 && bindings.Count > 1)
                {
                    var save = bindings[currentIndex - 1];
                    bindings[currentIndex - 1] = bindings[currentIndex];
                    bindings[currentIndex] = save;
                    ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
                    ctlCurrentAnswersGrid.Store.Primary.DataBind();
                }
            }
            else if (e.Command == "increment")
            {
                var bindings = CurrentBindings;
                var currentIndex = e.RowIndex;
                if (currentIndex != bindings.Count - 1 && bindings.Count > 1)
                {
                    var save = bindings[currentIndex + 1];
                    bindings[currentIndex + 1] = bindings[currentIndex];
                    bindings[currentIndex] = save;
                    ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
                    ctlCurrentAnswersGrid.Store.Primary.DataBind();
                }
            }
        }

        [CommandHandler(CommandName = "EditAnswerInEditor")]
        public void EditAnswerInEditorHandler(object sender, CommandInfo command)
        {
            if (string.IsNullOrEmpty(ctlGroupId.SelectedAsString))
                throw new BusinessException("Lütfen önce soru grubunu seçiniz");
            command.Parameters["entity"] = CurrentBindings[command.RowIndex];
            command.Parameters["type"] = new QuestionGroupDefinitionBusiness().Retrieve(ctlGroupId.SelectedAsInt).Tur;
            ViewState["editingAnswerIndex"] = command.RowIndex;
            ctlAnswerEditor.EditInEditorHandler(sender, command);
        }

        [CommandHandler(CommandName = "UpdateAnswer")]
        public void UpdateAnswerHandler(object sender, CommandInfo command)
        {
            if (string.IsNullOrEmpty(ctlGroupId.SelectedAsString))
                throw new BusinessException("Lütfen önce soru grubunu seçiniz");
            command.Parameters["entity"] = CurrentBindings[(int)ViewState["editingAnswerIndex"]];
            ctlAnswerEditor.UpdateEntityHandler(sender, command);
            var updatedEntity = command.Parameters["entity"] as Cevap;
            var bindings = CurrentBindings;
            bindings[(int)ViewState["editingAnswerIndex"]] = updatedEntity;
            CurrentBindings = bindings;
            ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
            ctlCurrentAnswersGrid.Store.Primary.DataBind();
        }

        [CommandHandler(CommandName = "CreateAnswerInEditor")]
        public void CreateAnswerInEditorHandler(object sender, CommandInfo command)
        {
            command.Parameters["type"] = new QuestionGroupDefinitionBusiness().Retrieve(ctlGroupId.SelectedAsInt).Tur;
            ctlAnswerEditor.CreateInEditorHandler(sender, command);
        }

        [CommandHandler(CommandName = "CreateAnswer")]
        public void CreateAnswerHandler(object sender, CommandInfo command)
        {
            command.Parameters["entity"] = new Cevap();
            ctlAnswerEditor.CreateEntityHandler(sender, command);
            var bindings = CurrentBindings;
            bindings.Add(command.Parameters["entity"] as Cevap);
            CurrentBindings = bindings;
            ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
            ctlCurrentAnswersGrid.Store.Primary.DataBind();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Soru Tanımla";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            CurrentBindings = null;
            ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
            ctlCurrentAnswersGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlGenForm.ClearFields();
            ctlKatsayi.ValueAsDouble = 1.0;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Ad = ctlAd.Text;
            entity.Yardim = ctlAciklama.Text;
            entity.ZorunlulukDurumu = ctlZorunluluk.SelectedAsString;
            entity.EtkinDurum = ctlAktif.SelectedAsString;
            entity.GrupID = ctlGroupId.SelectedAsInt;
            entity.KatsayiOran = ctlKatsayi.ValueAsDouble;
            BusinessObject.UpdateSingle(entity, CurrentBindings);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            CurrentBindings = entity.Cevap.OrderBy(p=>p.Sira).Select(p => p).ToList();
            ctlCurrentAnswersGrid.Store.Primary.DataSource = CurrentBindings;
            ctlCurrentAnswersGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlAd.Text = entity.Ad;
            ctlAciklama.Text = entity.Yardim;
            ctlZorunluluk.Text = entity.ZorunlulukDurumu;
            ctlAktif.Text = entity.EtkinDurum;
            ctlKatsayi.ValueAsDouble = entity.KatsayiOran;
            ctlGroupId.SelectedAsInt = entity.GrupID;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.Soru();
            entity.Ad = ctlAd.Text;
            entity.Yardim = ctlAciklama.Text;
            entity.ZorunlulukDurumu = ctlZorunluluk.SelectedAsString;
            entity.EtkinDurum = ctlAktif.SelectedAsString;
            entity.GrupID = ctlGroupId.SelectedAsInt;
            entity.KatsayiOran = ctlKatsayi.ValueAsDouble;
            CurrentBindings.ForEach(p=>entity.Cevap.Add(p));
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