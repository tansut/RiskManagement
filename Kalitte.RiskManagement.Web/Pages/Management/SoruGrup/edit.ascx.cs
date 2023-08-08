﻿using System;
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

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.SoruGrup
{

    public partial class edit : EditorViewControl<QuestionGroupDefinitionBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Soru Grubu Tanımla";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Ad = ctlAd.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.GrupTur = ctlGrupTur.SelectedAsString;
            entity.EtkinDurum = ctlAktif.SelectedAsString;
            entity.ZorunlulukDurumu = ctlZorunluluk.SelectedAsString;
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlAd.Text = entity.Ad;
            ctlAciklama.Text = entity.Aciklama;
            ctlGrupTur.SelectedAsString = entity.GrupTur;
            ctlAktif.SelectedAsString = entity.EtkinDurum;
            ctlZorunluluk.SelectedAsString = entity.ZorunlulukDurumu;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.SoruGrupTanim();
            entity.Ad = ctlAd.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.GrupTur = ctlGrupTur.SelectedAsString;
            entity.EtkinDurum = ctlAktif.SelectedAsString;
            entity.ZorunlulukDurumu = ctlZorunluluk.SelectedAsString;
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