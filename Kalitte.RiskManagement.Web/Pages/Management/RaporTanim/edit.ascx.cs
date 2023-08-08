using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.UI;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Web.Pages.Management.RaporTanim
{
    public partial class edit : EditorViewControl<ReportBusiness>
    {
        [Serializable]
        public class RoleData
        {
            public string RoleName { get; private set; }

            public RoleData(string roleName)
            {
                this.RoleName = roleName;
            }
        }

        List<RoleData> CurrentRoles
        {
            get
            {
                if (ViewState["cb"] == null)
                    return new List<RoleData>();
                return (List<RoleData>)ViewState["cb"];
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
                ctlRoles.Store.Primary.DataSource = BusinessObject.GetRoles().Select(p => new RoleData(p)).ToList();
                ctlRoles.Store.Primary.DataBind();
            }
        }

        protected void AddRolesToReportHandler(object sender, DirectEventArgs e)
        {
            var rolename = ctlRoles.SelectedAsString;

            if (!string.IsNullOrEmpty(rolename))
            {
                if (CurrentRoles.Any(p => p.RoleName == rolename))
                    throw new BusinessException("Rapor zaten bu role sahiptir");
                var bindings = CurrentRoles;
                bindings.Add(new RoleData(rolename));
                CurrentRoles = bindings;
                ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
                ctlCurrentRolesGrid.Store.Primary.DataBind();
            }
        }

        protected void GridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {

            if (e.Command == "delete")
            {
                var bindings = CurrentRoles;
                bindings.RemoveAll(p => p.RoleName == e.ID);
                CurrentRoles = bindings;
                ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
                ctlCurrentRolesGrid.Store.Primary.DataBind();
            }
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Rapor Tanımla";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            CurrentRoles = null;
            ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
            ctlCurrentRolesGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlRoles.SelectedIndex = -1;
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        public void UItoEntity(Kalitte.RiskManagement.Framework.Model.RaporTanim entity)
        {
            entity.Baslik = ctlBaslik.Text;
            entity.ReportKey = ctlReportKey.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.ReportPath = ctlReportPath.Text;
            entity.InputPath = ctlInputPath.Text;
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            if (CurrentRoles.Count == 0)
                throw new BusinessException("Raporun en az bir adet rolü olmalıdır");
            var entity = BusinessObject.Retrieve(CurrentID);
            BusinessObject.updateReportRoles(CurrentID, CurrentRoles.Select(p => p.RoleName).ToArray());
            UItoEntity(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            CurrentRoles = new ReportRoleBusiness().GetReportRoles(entity.ID).Select(p => new RoleData(p)).ToList();
            ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
            ctlCurrentRolesGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlRoles.SelectedIndex = -1;
            ctlGenForm.ClearFields();

            entityWindow.Title = string.Format("Düzenle: {0}", entity.Baslik);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlReportKey.Text = entity.ReportKey;
            ctlBaslik.Text = entity.Baslik;
            ctlAciklama.Text = entity.Aciklama;
            ctlInputPath.Text = entity.InputPath;
            ctlReportPath.Text = entity.ReportPath;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            if (CurrentRoles.Count == 0)
                throw new BusinessException("Raporun en az bir adet rolü olmalıdır");

            var entity = new Kalitte.RiskManagement.Framework.Model.RaporTanim();
            UItoEntity(entity);
            CurrentRoles.ForEach(p => entity.RaporRol.Add(new RaporRol() { RoleId = new RoleBusiness().RetrieveByRoleName(p.RoleName).RoleId }));
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