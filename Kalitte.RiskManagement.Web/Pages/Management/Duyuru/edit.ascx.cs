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

namespace Kalitte.RiskManagement.Web.Pages.Management.Duyuru
{
    public partial class edit : EditorViewControl<DuyuruBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Duyuru oluştur";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Baslik = ctlBaslik.Text;
            entity.BaslangicTarihi = ctlBaslangicTarihi.SelectedDate;
            entity.BitisTarihi = ctlBitisTarihi.SelectedDate;
            entity.Icerik = ctlIcerik.Text;
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Baslik);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlBaslik.Text = entity.Baslik;
            ctlBaslangicTarihi.SelectedDate = entity.BaslangicTarihi;
            ctlBitisTarihi.SelectedDate = entity.BitisTarihi;
            ctlIcerik.Text = entity.Icerik;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.Duyuru();
            entity.Baslik = ctlBaslik.Text;
            entity.BaslangicTarihi = ctlBaslangicTarihi.SelectedDate;
            entity.BitisTarihi = ctlBitisTarihi.SelectedDate;
            entity.Icerik = ctlIcerik.Text;
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