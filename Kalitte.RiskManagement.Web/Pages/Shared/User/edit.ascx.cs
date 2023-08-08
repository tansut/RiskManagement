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

namespace Kalitte.RiskManagement.Web.Pages.Shared.User
{

    public partial class edit : EditorViewControl<UserBusiness>
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

        protected void AddRolesToUserHandler(object sender, DirectEventArgs e)
        {
            var role = ctlRoles.SelectedAsString;
            if (!string.IsNullOrEmpty(role))
            {
                if (CurrentRoles.Any(p => p.RoleName == role))
                    throw new BusinessException("Kullanıcı zaten bu role sahiptir");
                var bindings = CurrentRoles;
                bindings.Add(new RoleData(role));
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

        protected void AutoCompleteBirimHandler(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UnitBusiness().RetreiveItems(e.Parameters);
        }
        protected void AutoCompleteUnvanHandler(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UnvanBusiness().RetreiveItems(e.Parameters);
        }
        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Kullanıcı Tanımla";
            ctlUnlockBtn.Hidden = true;
            ctlLockedLabel.Hidden = true;
            ctlUsername.ReadOnly = false;
            ctlPassword.Show();
            ctlPassword.AllowBlank = false;
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            //ctlUnvan.Store.Primary.DataSource = new UnvanBusiness().RetreiveItems();
            //ctlUnvan.Store.Primary.DataBind();
            CurrentRoles = null;
            ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
            ctlCurrentRolesGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlRoles.SelectedIndex = -1;
            ctlGenForm.ClearFields();
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "ResetUserPassword")]
        public void ResetUserPasswordHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            string newPassord = BusinessObject.ResetPassword(entity);
            WebHelper.ShowMessage(string.Format("Üretilen şifre: {0}", newPassord), MessageType.Info);
        }

        [CommandHandler(CommandName = "UnlockUser")]
        public void UnlockUserHandler(object sender, CommandInfo command)
        {
            MembershipUser mu = Membership.GetUser(BusinessObject.Retrieve(CurrentID).UserName);
            mu.UnlockUser();
            Membership.UpdateUser(mu);
            WebHelper.ShowMessage("İşlem başarılı. Kullanıcı hesabının kilidi açıldı.");
            ctlUnlockBtn.Hidden = true;
            ctlLockedLabel.Hidden = true;
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            if (CurrentRoles.Count == 0)
                throw new BusinessException("Kullanıcının en az bir adet rolü olmalıdır");
            var entity = BusinessObject.Retrieve(CurrentID);
            BusinessObject.UpdateUser(entity.UserName, ctlEMail.Text, ctlAd.Text, ctlSoyad.Text, ctlEnabled.Checked, ctlBirim.SelectedAsInt, ctlUnvan.SelectedAsInt, CurrentRoles.Select(p => p.RoleName).ToArray());
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            //ctlUnvan.Store.Primary.DataSource = new UnvanBusiness().RetreiveItems();
            //ctlUnvan.Store.Primary.DataBind();
            CurrentRoles = BusinessObject.GetUserRoles(entity.UserName).Select(p => new RoleData(p)).ToList();
            ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
            ctlCurrentRolesGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlRoles.SelectedIndex = -1;
            var user = Membership.GetUser(entity.UserName);
            ctlGenForm.ClearFields();
            ctlUsername.ReadOnly = true;
            ctlPassword.Hide();
            ctlPassword.AllowBlank = true;
            entityWindow.Title = string.Format("Düzenle: {0}", entity.AdSoyad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlUsername.ValueAsLong = entity.TCKimlikNo;
            ctlAd.Text = entity.Ad;
            ctlSoyad.Text = entity.Soyad;
            ctlEnabled.Checked = user.IsApproved;
            ctlEMail.Text = user.Email;
            ctlBirim.Initialize(entity.Birim.ID, entity.Birim.FullUnitName);
            ctlUnvan.Initialize(entity.Unvan.ID,entity.Unvan.Ad);
            MembershipUser mu = Membership.GetUser(entity.UserName);
            ctlLockedLabel.Hidden = !mu.IsLockedOut;
            ctlUnlockBtn.Hidden = !mu.IsLockedOut;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            if (CurrentRoles.Count == 0)
                throw new BusinessException("Kullanıcının en az bir adet rolü olmalıdır");
            BusinessObject.CreateUser(ctlUsername.Text, ctlPassword.Text, ctlEMail.Text, ctlAd.Text, ctlSoyad.Text, ctlEnabled.Checked, ctlBirim.SelectedAsInt, ctlUnvan.SelectedAsInt, CurrentRoles.Select(p => p.RoleName).ToArray());
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.DeleteEntity)]
        public void DeleteEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            try
            {
                BusinessObject.DeleteSingle(entity);
            }
            catch
            {
                BusinessObject.LockUser(entity);
                new RiskBusiness().ValidateUsersRiskStatus(entity.UserId);
            }
            CurrentLister.LoadItems();
        }

        public void Show(int UserID)
        {
            var entity = BusinessObject.Retrieve(UserID);
            //ctlUnvan.Store.Primary.DataSource = new UnvanBusiness().RetreiveItems();
            //ctlUnvan.Store.Primary.DataBind();
            CurrentRoles = BusinessObject.GetUserRoles(entity.UserName).Select(p => new RoleData(p)).ToList();
            ctlCurrentRolesGrid.Store.Primary.DataSource = CurrentRoles;
            ctlCurrentRolesGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlRoles.SelectedIndex = -1;
            var user = Membership.GetUser(entity.UserName);
            ctlGenForm.ClearFields();
            ctlUsername.ReadOnly = true;
            ctlPassword.Hide();
            ctlPassword.AllowBlank = true;
            entityWindow.Title = string.Format("Düzenle: {0}", entity.AdSoyad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlUsername.ValueAsLong = entity.TCKimlikNo;
            ctlAd.Text = entity.Ad;
            ctlSoyad.Text = entity.Soyad;
            ctlEnabled.Checked = user.IsApproved;
            ctlEMail.Text = user.Email;
            ctlBirim.Initialize(entity.Birim.ID, entity.Birim.FullUnitName);
            ctlUnvan.Initialize(entity.Unvan.ID,entity.Unvan.Ad);
            MembershipUser mu = Membership.GetUser(entity.UserName);
            ctlLockedLabel.Hidden = !mu.IsLockedOut;
            ctlUnlockBtn.Hidden = !mu.IsLockedOut;
            entityWindow.Show();
        }
    }
}