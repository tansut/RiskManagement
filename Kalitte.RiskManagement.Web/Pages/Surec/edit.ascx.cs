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

namespace Kalitte.RiskManagement.Web.Pages.Surec
{
    public partial class edit : EditorViewControl<WorkflowBusiness>
    {
        [Serializable]
        public class HedefBinding
        {
            public int ID { get; private set; }
            public string Ad { get; private set; }
            public string Aciklama { get; private set; }

            public HedefBinding(int id, string ad, string aciklama)
            {
                this.ID = id;
                this.Ad = ad;
                this.Aciklama = aciklama;
            }
        }

        [Serializable]
        public class SurecIliskiBinding
        {
            public int ID { get; private set; }
            public string Ad { get; private set; }

            public SurecIliskiBinding(int id, string ad)
            {
                this.ID = id;
                this.Ad = ad;
            }
        }

        [Serializable]
        public class UserBinding
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


        List<string> CurrentDayanakList
        {
            get
            {
                if (ViewState["cd"] == null)
                    return new List<string>();
                return (List<string>)ViewState["cd"];
            }
            set
            {
                ViewState["cd"] = value;
            }
        }

        List<HedefBinding> CurrentHedefList
        {
            get
            {
                if (ViewState["ch"] == null)
                    return new List<HedefBinding>();
                return (List<HedefBinding>)ViewState["ch"];
            }
            set
            {
                ViewState["ch"] = value;
            }
        }
        List<string> CurrentYararlananList
        {
            get
            {
                if (ViewState["cy"] == null)
                    return new List<string>();
                return (List<string>)ViewState["cy"];
            }
            set
            {
                ViewState["cy"] = value;
            }
        }

        List<SurecIliskiBinding> CurrentSurecList
        {
            get
            {
                if (ViewState["cs"] == null)
                    return new List<SurecIliskiBinding>();
                return (List<SurecIliskiBinding>)ViewState["cs"];
            }
            set
            {
                ViewState["cs"] = value;
            }
        }

        List<UserBinding> CurrentUserBinding
        {
            get
            {
                if (ViewState["cub"] == null)
                    return new List<UserBinding>();
                return (List<UserBinding>)ViewState["cub"];
            }
            set
            {
                ViewState["cub"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DayanakGridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {
            if (e.Command == "delete")
            {
                string dayanak = e.E.ExtraParams["Ad"].ToString();
                var bindings = CurrentDayanakList;
                bindings.RemoveAll(p => p == dayanak);
                CurrentDayanakList = bindings;
                ctlDayanakGrid.Store.Primary.DataSource = CurrentDayanakList.Select(p => new { Ad = p }).ToList();
                ctlDayanakGrid.Store.Primary.DataBind();
            }
        }

        protected void HedefGridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {
            if (e.Command == "delete")
            {
                var bindings = CurrentHedefList;
                bindings.RemoveAll(p => p.ID == e.RecordID);
                CurrentHedefList = bindings;
                ctlHedefGrid.Store.Primary.DataSource = CurrentHedefList;
                ctlHedefGrid.Store.Primary.DataBind();
            }
        }

        protected void YararlananGridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {
            if (e.Command == "delete")
            {
                string yararlanan = e.E.ExtraParams["Ad"].ToString();
                var bindings = CurrentYararlananList;
                bindings.RemoveAll(p => p == yararlanan);
                CurrentYararlananList = bindings;
                ctlYararlananGrid.Store.Primary.DataSource = CurrentYararlananList.Select(p => new { Ad = p }).ToList();
                ctlYararlananGrid.Store.Primary.DataBind();
            }
        }

        protected void SurecGridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {
            if (e.Command == "delete")
            {
                var bindings = CurrentSurecList;
                bindings.RemoveAll(p => p.ID == e.RecordID);
                CurrentSurecList = bindings;
                ctlSurecGrid.Store.Primary.DataSource = CurrentSurecList;
                ctlSurecGrid.Store.Primary.DataBind();
            }
        }

        protected void UserGridRowCommandHandler(object sender, GridRowCommandEventArgs e)
        {
            if (e.Command == "delete")
            {
                var bindings = CurrentUserBinding;
                bindings.RemoveAll(p => p.ID == e.RecordID);
                CurrentUserBinding = bindings;
                ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentUserBinding;
                ctlCurrentUsersGrid.Store.Primary.DataBind();
            }
            else if (e.Command == "ShowUserInfo")
            {
                ((_default)Page).GetUserInfo().Bind(e.RecordID);
            }
        }

        protected void AddDayanakList(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(ctlDayanak.SelectedText))
            {

                if (CurrentDayanakList.Contains(ctlDayanak.SelectedText))
                    throw new BusinessException("Süreç zaten eklemek istediğiniz dayanağı içermektedir");

                var bindings = CurrentDayanakList;
                bindings.Add(ctlDayanak.SelectedText);
                CurrentDayanakList = bindings;
                ctlDayanakGrid.Store.Primary.DataSource = CurrentDayanakList.Select(p => new { Ad = p }).ToList();
                ctlDayanakGrid.Store.Primary.DataBind();
                ctlDayanak.EndEdit();
                ctlDayanak.Text = string.Empty;
            }
        }

        protected void AddHedefList(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(ctlHedefAutoComplete.SelectedAsString))
            {
                int hedefid = ctlHedefAutoComplete.SelectedAsInt;
                if (CurrentHedefList.Any(p => p.ID == hedefid))
                    throw new BusinessException("Süreç zaten eklemek istediğiniz hedefi içermektedir");

                var hedeftanim = new HedefBusiness().Retrieve(hedefid);
                var bindings = CurrentHedefList;
                bindings.Add(new HedefBinding(hedefid, hedeftanim.Ad, hedeftanim.Aciklama));
                CurrentHedefList = bindings;
                ctlHedefGrid.Store.Primary.DataSource = CurrentHedefList;
                ctlHedefGrid.Store.Primary.DataBind();
            }
        }

        protected void AddYararlananList(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(ctlYararlanan.SelectedText))
            {
                if (CurrentYararlananList.Contains(ctlYararlanan.SelectedText))
                    throw new BusinessException("Süreç zaten eklemek istediğiniz yararlananı içermektedir");

                var bindings = CurrentYararlananList;
                bindings.Add(ctlYararlanan.SelectedText);
                CurrentYararlananList = bindings;
                ctlYararlananGrid.Store.Primary.DataSource = CurrentYararlananList.Select(p => new { Ad = p }).ToList();
                ctlYararlananGrid.Store.Primary.DataBind();
                ctlYararlanan.EndEdit();
                ctlYararlanan.Text = string.Empty;
            }
        }

        protected void AddSurecList(object sender, DirectEventArgs e)
        {
            if (!string.IsNullOrEmpty(ctliliskiliSurec.SelectedAsString))
            {
                int surecid = ctliliskiliSurec.SelectedAsInt;
                if (CurrentSurecList.Any(p => p.ID == surecid))
                    throw new BusinessException("Eklemek istediğiniz süreç, zaten mevcut süreçle ilişkilendirilmiştir.");

                var surectanim = new WorkflowBusiness().Retrieve(surecid);
                var bindings = CurrentSurecList;
                bindings.Add(new SurecIliskiBinding(surecid, surectanim.Ad));
                CurrentSurecList = bindings;
                ctlSurecGrid.Store.Primary.DataSource = CurrentSurecList;
                ctlSurecGrid.Store.Primary.DataBind();
            }
        }

        protected void AddUserToList(object sender, DirectEventArgs e)
        {
            var userName = ctlUser.SelectedAsString;
            if (!string.IsNullOrEmpty(userName))
            {
                if (CurrentUserBinding.Any(p => p.Username == userName))
                    throw new BusinessException("Kullanıcı zaten bu grubun üyesidir");
                var userBll = new UserBusiness();
                var user = userBll.ValidateUser(userName);
                var bindings = CurrentUserBinding;
                bindings.Add(new UserBinding(userName, user.AdSoyad, user.UserId, user.ID));
                CurrentUserBinding = bindings;
                ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentUserBinding;
                ctlCurrentUsersGrid.Store.Primary.DataBind();
            }
        }

        protected void AutoCompleteHedef(object sender, AutoCompleteEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ctlStratejikAmac.SelectedAsString))
            {
                e.Parameters.FieldFilters.Add(new FilteringInfo("StratejikAmacID", ctlStratejikAmac.SelectedAsString, FilterType.Numeric));
                e.Data = new HedefBusiness().RetreiveItems(e.Parameters);
            }
        }

        protected void AutoCompleteIliskiliSurec(object sender, AutoCompleteEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ctlBirim.SelectedAsString))
            {
                e.Parameters.FieldFilters.Add(new FilteringInfo("BirimID", ctlBirim.SelectedAsString, FilterType.Numeric));
                e.Data = new WorkflowBusiness().GetListing(e.Parameters, CurrentID);
            }
        }

        protected void AutoCompleteUser(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UserBusiness().RetreiveItems(e.Parameters);
        }

        protected void AutoCompleteStratejikAmac(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new StratejikAmacBusiness().RetreiveItems(e.Parameters);
        }

        protected void AutoCompleteBirimHandler(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UnitBusiness().RetreiveItems(e.Parameters);
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateInEditor)]
        public void CreateInEditorHandler(object sender, CommandInfo command)
        {
            entityWindow.Title = "Süreç Oluştur";
            ctlSave.CommandName = KnownCommand.CreateEntity.ToString();
            ClearFields();
            ctlAktif.Checked = true;
            CurrentDayanakList = null;
            ctlDayanakGrid.Store.Primary.DataSource = CurrentDayanakList.Select(p => new { Ad = p }).ToList(); ;
            ctlDayanakGrid.Store.Primary.DataBind();
            CurrentHedefList = null;
            ctlHedefGrid.Store.Primary.DataSource = CurrentHedefList;
            ctlHedefGrid.Store.Primary.DataBind();
            CurrentYararlananList = null;
            ctlYararlananGrid.Store.Primary.DataSource = CurrentYararlananList.Select(p => new { Ad = p }).ToList();
            ctlYararlananGrid.Store.Primary.DataBind();
            CurrentUserBinding = null;
            ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentUserBinding;
            ctlCurrentUsersGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            ctlSurecSorumlusuTC.Text = UserBusiness.GetUser().TCKimlikNo.ToString();
            ctlSurecSorumlusuAd.Text = UserBusiness.GetUser().AdSoyad;
            ctlPasifTarihi.Hidden = true;
            entityWindow.Show();
        }

        [CommandHandler(KnownCommand = KnownCommand.ExportData)]
        public void ExportToWord(object sender, CommandInfo command)
        {

        }

        

        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var otb = new OtoTamamlaBusiness();
            var entity = BusinessObject.Retrieve(CurrentID);
            uiToEntity(entity);
            CurrentDayanakList.ForEach(p => entity.SurecDayanak.Add(new SurecDayanak() { SurecID = entity.ID, Dayanak = p }));
            CurrentHedefList.ForEach(p => entity.SurecHedef.Add(new SurecHedef() { SurecID = entity.ID, HedefID = p.ID }));
            CurrentYararlananList.ForEach(p => entity.SurecYararlanan.Add(new SurecYararlanan() { SurecID = entity.ID, Yararlanan = p }));
            CurrentSurecList.ForEach(p => entity.SurecIliski.Add(new SurecIliski() { KaynakSurecID = entity.ID, HedefSurecID = p.ID }));
            CurrentUserBinding.ForEach(p => entity.SurecCalisan.Add(new SurecCalisan() { SurecID = entity.ID, KullaniciID = p.UserId }));
            BusinessObject.UpdateSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            ClearFields();
            entityWindow.Title = string.Format("Düzenle: {0}", entity.Ad);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;
            ctlName.Text = entity.Ad;
            ctlTanim.Text = entity.Tanim;
            ctlHedef.Text = entity.Hedef;
            ctlGirdi.Text = entity.Girdi;
            ctlCikti.Text = entity.Cikti;
            ctlRevizyonNo.Text = entity.RevizyonNo;
            if (entity.RevizyonTarih.HasValue)
                ctlRevizyonTarih.SelectedDate = entity.RevizyonTarih.Value;
            ctlYayinTarih.SelectedDate = entity.YayinTarih;
            ctlDurum.SelectedAsString = entity.Durum;
            ctlAktif.Checked = entity.Aktif;
            if (!entity.Aktif)
            {
                ctlPasifTarihi.Hidden = false;
                ctlPasifTarihi.SelectedDate = entity.PasifTarihi.Value;
            }
            else
                ctlPasifTarihi.Hidden = true;
            ctlPeriod.Text = entity.Periyod;
            ctlSurecSorumlusuAd.Text = entity.aspnet_Users.AdSoyad;
            ctlSurecSorumlusuTC.Text = entity.aspnet_Users.TCKimlikNo.ToString();
            CurrentDayanakList = entity.SurecDayanak.Where(p => p.SurecID == entity.ID).Select(u => u.Dayanak).ToList();
            CurrentHedefList = entity.SurecHedef.Select(p => new HedefBinding(p.Hedef.ID, p.Hedef.Ad, p.Hedef.Aciklama)).ToList();
            CurrentYararlananList = entity.SurecYararlanan.Where(p => p.SurecID == entity.ID).Select(u => u.Yararlanan).ToList();
            CurrentSurecList = entity.SurecIliski.Select(p => new SurecIliskiBinding(p.Surec1.ID, p.Surec1.Ad)).ToList();
            CurrentUserBinding = entity.SurecCalisan.Select(p => new UserBinding(p.aspnet_Users.UserName, p.aspnet_Users.AdSoyad, p.aspnet_Users.UserId, p.aspnet_Users.ID)).ToList();
            ctlDayanakGrid.Store.Primary.DataSource = CurrentDayanakList.Select(p => new { Ad = p }).ToList(); ;
            ctlDayanakGrid.Store.Primary.DataBind();
            ctlHedefGrid.Store.Primary.DataSource = CurrentHedefList;
            ctlHedefGrid.Store.Primary.DataBind();
            ctlYararlananGrid.Store.Primary.DataSource = CurrentYararlananList.Select(p => new { Ad = p }).ToList();
            ctlYararlananGrid.Store.Primary.DataBind();
            ctlHedefGrid.Store.Primary.DataBind();
            ctlSurecGrid.Store.Primary.DataSource = CurrentSurecList;
            ctlSurecGrid.Store.Primary.DataBind();
            ctlCurrentUsersGrid.Store.Primary.DataSource = CurrentUserBinding;
            ctlCurrentUsersGrid.Store.Primary.DataBind();
            ctlTabs.ActiveTabIndex = 0;
            entityWindow.Show();
        }

        private void uiToEntity(Kalitte.RiskManagement.Framework.Model.Surec entity)
        {
            entity.Ad = ctlName.Text;
            entity.Tanim = ctlTanim.Text;
            entity.Hedef = ctlHedef.Text;
            entity.Girdi = ctlGirdi.Text;
            entity.Cikti = ctlCikti.Text;
            if (entity.Aktif)
            {
                entity.PasifTarihi = null;
                if (!ctlAktif.Checked)
                    entity.PasifTarihi = DateTime.Today;
            }
            entity.Aktif = ctlAktif.Checked;
            entity.RevizyonNo = ctlRevizyonNo.Text;
            if (ctlRevizyonTarih.IsEmpty)
                entity.RevizyonTarih = null;
            else entity.RevizyonTarih = ctlRevizyonTarih.SelectedDate;
            entity.YayinTarih = ctlYayinTarih.SelectedDate;
            entity.Durum = ctlDurum.SelectedAsString;
            entity.Periyod = ctlPeriod.Text;
        }

        private void ClearFields()
        {
            ctlGenForm.ClearFields();
            ctlHedefAutoComplete.Clear();
            ctlStratejikAmac.Clear();
            ctlDayanak.Clear();
            ctlYararlanan.Clear();
            ctliliskiliSurec.Clear();
            ctlUser.Clear();
        }

        [CommandHandler(KnownCommand = KnownCommand.CreateEntity)]
        public void CreateEntityHandler(object sender, CommandInfo command)
        {
            var otb = new OtoTamamlaBusiness();
            var entity = new Kalitte.RiskManagement.Framework.Model.Surec();
            uiToEntity(entity);
            CurrentDayanakList.ForEach(p => entity.SurecDayanak.Add(new SurecDayanak() { Dayanak = p }));
            CurrentHedefList.ForEach(p => entity.SurecHedef.Add(new SurecHedef() { HedefID = p.ID }));
            CurrentYararlananList.ForEach(p => entity.SurecYararlanan.Add(new SurecYararlanan() { Yararlanan = p }));
            CurrentSurecList.ForEach(p => entity.SurecIliski.Add(new SurecIliski() { HedefSurecID = p.ID }));
            CurrentUserBinding.ForEach(p => entity.SurecCalisan.Add(new SurecCalisan() { KullaniciID = p.UserId }));
            entity.Aktif = true;
            BusinessObject.InsertSingle(entity);
            CurrentLister.LoadItems();
            entityWindow.Hide();
        }

        [CommandHandler(KnownCommand = KnownCommand.DeleteEntity)]
        public void DeleteEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            if (!new SurecIliskiBusiness().IsExistingAsHedef(entity.ID))
            {
                BusinessObject.DeleteSingle(entity);
                CurrentLister.LoadItems();
            }
            else
                throw new BusinessException("Silmek istenilen sürecin ilişkili olduğu süreçler vardır. Silme işleminden önce bu ilişkileri kaldırmanız gerekmektedir.");
        }

        [CommandHandler(CommandName = "ChangeUser")]
        protected void ChangeWorkflowUser(object sender, CommandInfo command)
        {
            var userTo = new UserBusiness().RetreiveByUsername(ctlWorkflowRedirectionUser.SelectedAsLong.ToString()).UserId;
            var surec = BusinessObject.Retrieve(CurrentID);
            surec.KullaniciID = userTo;
            var risks = new RiskBusiness().GetRisksByWorkflowID(surec.ID);
            foreach (var risk in risks)
            {
                risk.KullaniciID = userTo;
            }
            BusinessObject.DataContext.SaveChanges();
        }
        
    }
}