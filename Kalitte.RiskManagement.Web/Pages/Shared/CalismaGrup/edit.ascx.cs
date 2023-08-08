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
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Web.Pages.Shared.CalismaGrup
{
    public partial class edit : EditorViewControl<CalismaGrupBusiness>
    {
        [Serializable]
        class UserBinding
        {
            public string Username { get; set; }
            public string AdSoyad { get; set; }
            public Guid UserId { get; set; }
            public int ID { get; set; }

            public UserBinding(string userName, string adSoyad, Guid userid, int id)
            {
                this.AdSoyad = adSoyad;
                this.Username = userName;
                this.UserId = userid;
                this.ID = id;
            }
        }

        List<UserBinding> CurrentBinding
        {
            get
            {
                if (ViewState["cb"] == null)
                    return new List<UserBinding>();
                return (List<UserBinding>)ViewState["cb"];
            }
            set
            {
                ViewState["cb"] = value;
            }
        }

        List<Guid> RemovedUser
        {
            get
            {
                if (ViewState["ru"] == null)
                {
                    ViewState["ru"] =  new List<Guid>();                  
                }
                return (List<Guid>)ViewState["ru"];
            }
            set
            {
                ViewState["ru"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {
            if (e.Command == "delete")
            {
                var bindings = CurrentBinding;
                RemovedUser.Add(new Guid(CurrentBinding.Where(p => p.ID == e.RecordID).Select(q => q.UserId).Single().ToString()));
                bindings.RemoveAll(p => p.ID == e.RecordID);
                CurrentBinding = bindings;
                ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentBinding;
                ctlCurrentUsersGrid.Store.Primary.DataBind();
            }
        }

        protected void AddUserToList(object sender, DirectEventArgs e)
        {
            var userName = ctlUser.SelectedAsString;
            if (!string.IsNullOrEmpty(userName))
            {
                if (CurrentBinding.Any(p => p.Username == userName))
                    throw new BusinessException("Kullanıcı zaten bu grubun üyesidir");
                var userBll = new UserBusiness();
                var user = userBll.ValidateUser(userName);
                var bindings = CurrentBinding;
                bindings.Add(new UserBinding(userName, user.AdSoyad, user.UserId, user.ID));
                CurrentBinding = bindings;

                ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentBinding;
                ctlCurrentUsersGrid.Store.Primary.DataBind();

                if (RemovedUser.Contains(user.UserId))
                    RemovedUser.Remove(user.UserId);

            }
        }

        protected void AutoCompleteUser(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UserBusiness().RetreiveItems(e.Parameters);

            //e.Data = new AutoCompleteBusiness().RetreiveGroupNames(e.Parameters);
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Puanlama Grubu Oluştur";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ctlGenForm.ClearFields();
            ctlUser.Clear();
            CurrentBinding = null;
            RemovedUser = null;
            var bindings = CurrentBinding;
            var user = UserBusiness.GetUser();
            bindings.Add(new UserBinding(user.UserName, user.AdSoyad, user.UserId, user.ID));
            CurrentBinding = bindings;
            ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentBinding;
            ctlCurrentUsersGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(CurrentID);
            entity.Ad = ctlName.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.GrupTur = ctlGroupType.SelectedAsString;
            var newBindings = CurrentBinding.Select(p => new CalismaGrupKullanici() { KatilimciKullaniciID = p.UserId }).ToList();
            DeleteRemovedUserScores(RemovedUser, entity);
            BusinessObject.UpdateSingle(entity, newBindings);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ctlGenForm.ClearFields();
            ctlUser.Clear();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlName.Text = entity.Ad;
            ctlGroupType.SelectedAsString = entity.GrupTur;
            ctlAciklama.Text = entity.Aciklama;
            RemovedUser = null;            
            CurrentBinding = entity.CalismaGrupKullanici.Where(d=>d.aspnet_Users.Disabled==null).Select(p => new UserBinding(p.aspnet_Users.UserName, p.aspnet_Users.AdSoyad, p.aspnet_Users.UserId, p.aspnet_Users.ID)).ToList();
            ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentBinding;
            ctlCurrentUsersGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.CalismaGrupTanim();
            entity.Ad = ctlName.Text;
            entity.GrupTur = ctlGroupType.SelectedAsString;
            entity.Aciklama = ctlAciklama.Text;
            CurrentBinding.ForEach(p => entity.CalismaGrupKullanici.Add(new CalismaGrupKullanici() { KatilimciKullaniciID = p.UserId }));
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

        protected void DeleteRemovedUserScores(List<Guid> removeduser, CalismaGrupTanim entity)
        {
            var risks = new RiskBusiness().GetRisksbyKullaniciID(entity.KullaniciID, entity.ID);
            var bss = new RiskQuestionAnswerBusiness();

            var rsc = bss.RetreiveByRiskListAndUser(risks, removeduser);
            foreach (var sorucevapitem in rsc)
            {
                bss.DeleteSingle(sorucevapitem);
            }
        }

    }
}