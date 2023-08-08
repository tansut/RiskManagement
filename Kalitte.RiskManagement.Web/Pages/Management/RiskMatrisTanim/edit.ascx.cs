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

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.RiskMatrisTanim
{
    public partial class edit : EditorViewControl<RiskMatrisBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Color_Changed(object sender, DirectEventArgs e)
        {            
            ctlRenk.StyleSpec = string.Format("background:#{0};", ColorPalette1.Value);
            ctlRenk.Text = string.Format("#{0}", ColorPalette1.Value);
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Risk Matrisi Tanımı";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.PuanBaslangic = ctlPuanBaslangic.ValueAsInt;
            entity.PuanBitis = ctlPuanBitis.ValueAsInt;
            entity.GrupDeger = ctlGrupDeger.Text;
            entity.Renk = ctlRenk.Text;
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();

        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.GrupDeger);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlPuanBaslangic.ValueAsInt = entity.PuanBaslangic;
            ctlPuanBitis.ValueAsInt = entity.PuanBitis;
            ctlGrupDeger.Text = entity.GrupDeger;
            ctlRenk.Text = entity.Renk;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            //var entity = new Kalitte.RiskManagement.Framework.Business.Common.RiskMatrisBusiness();
            var entity = new Framework.Model.RiskMatrisTanim();
            entity.PuanBaslangic = ctlPuanBaslangic.ValueAsInt;
            entity.PuanBitis = ctlPuanBitis.ValueAsInt;
            entity.GrupDeger = ctlGrupDeger.Text;
            entity.Renk = ctlRenk.Text;
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