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

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.RiskGrup
{

    public partial class edit : EditorViewControl<RiskGroupDefinitionBusiness>
    {
        [Serializable]
        public class SoruGrupData
        {
            public int SoruGrupID { get; private set; }
            public string Ad { get; private set; }

            public SoruGrupData(int id, string ad)
            {
                this.SoruGrupID = id;
                this.Ad = ad;
            }
        }

        List<SoruGrupData> CurrentBindings
        {
            get
            {
                if (ViewState["cb"] == null)
                    return new List<SoruGrupData>();
                return (List<SoruGrupData>)ViewState["cb"];
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
                ctlSoruGrup.Store.Primary.DataSource = new QuestionGroupDefinitionBusiness().RetreiveItems();
                ctlSoruGrup.Store.Primary.DataBind();
            }
        }

        protected void AddQuestionGroupToRiskGroupHandler(object sender, DirectEventArgs e)
        {
            var role = ctlSoruGrup.SelectedAsString;
            if (!string.IsNullOrEmpty(role))
            {
                if (CurrentBindings.Any(p => p.SoruGrupID == ctlSoruGrup.SelectedAsInt))
                    throw new BusinessException("Risk grubu zaten bu soru grubu ile ilişkilendirilmiştir.");
                var soruGrup = new QuestionGroupDefinitionBusiness().Retrieve(ctlSoruGrup.SelectedAsInt);
                var bindings = CurrentBindings;
                bindings.Add(new SoruGrupData(ctlSoruGrup.SelectedAsInt, soruGrup.Ad));
                CurrentBindings = bindings;
                ctlCurrentBindingsGrid.Store.Primary.DataSource = CurrentBindings;
                ctlCurrentBindingsGrid.Store.Primary.DataBind();
            }
        }

        protected void GridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {

            if (e.Command == "delete")
            {
                var bindings = CurrentBindings;
                bindings.RemoveAll(p => p.SoruGrupID == e.RecordID);
                CurrentBindings = bindings;
                ctlCurrentBindingsGrid.Store.Primary.DataSource = CurrentBindings;
                ctlCurrentBindingsGrid.Store.Primary.DataBind();
            }
        }


        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Risk Grubu Tanımla";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            CurrentBindings = null;
            ctlCurrentBindingsGrid.Store.Primary.DataSource = CurrentBindings;
            ctlCurrentBindingsGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }




        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Ad = ctlAd.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.ZorunlulukDurumu = ctlZorunluluk.SelectedAsString;
            entity.EtkinDurum = ctlAktif.SelectedAsString;
            entity.SoruGrupRiskGrup.Clear();
            CurrentBindings.ForEach(p => entity.SoruGrupRiskGrup.Add(new SoruGrupRiskGrup() { SoruGrupID = p.SoruGrupID, RiskGrupID = entity.ID }));
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            CurrentBindings = entity.SoruGrupRiskGrup.Select(p => new SoruGrupData(p.SoruGrupID, p.SoruGrupTanim.Ad)).ToList();
            ctlCurrentBindingsGrid.Store.Primary.DataSource = CurrentBindings;
            ctlCurrentBindingsGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlAd.Text = entity.Ad;
            ctlAciklama.Text = entity.Aciklama;
            ctlZorunluluk.Text = entity.ZorunlulukDurumu;
            ctlAktif.Text = entity.EtkinDurum;

            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new RiskGrupTanim();
            entity.Ad = ctlAd.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.ZorunlulukDurumu = ctlZorunluluk.SelectedAsString;
            entity.EtkinDurum = ctlAktif.SelectedAsString;
            CurrentBindings.ForEach(p=>entity.SoruGrupRiskGrup.Add(new SoruGrupRiskGrup() { SoruGrupID = p.SoruGrupID}));
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