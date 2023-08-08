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
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.OtoTamamla
{

    public partial class edit : EditorViewControl<OtoTamamlaBusiness>
    {

        protected void AutoCompleteGrup(object sender, AutoCompleteEventArgs e)
        {           
            e.Data = new AutoCompleteBusiness().RetreiveGroupNames(e.Parameters);
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Süreç Dayanak Tanımla";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Grup = ctlGrup.Text;
            entity.Alan = ctlAlan.Text;
            entity.Deger = ctlDeger.Text;
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();

        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Grup);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlGrup.Text = entity.Grup;
            ctlAlan.Text = entity.Alan;
            ctlDeger.Text = entity.Deger;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.OtoTamamla();
            entity.Grup = ctlGrup.Text;
            entity.Alan = ctlAlan.Text;
            entity.Deger = ctlDeger.Text;
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