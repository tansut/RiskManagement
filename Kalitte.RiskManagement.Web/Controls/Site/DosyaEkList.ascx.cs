using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Business.Management;
using System.IO;
using System.Net;
using System.Threading;
using Kalitte.RiskManagement.Framework.Business.Common;
using System.Security;

namespace Kalitte.RiskManagement.Web.Controls.Site
{
    public partial class DosyaEkList : ListerViewControl<DosyaEkBusiness>
    {

        string RowValues
        {
            get
            {
                if (ViewState["RowValues"] == null) ViewState["RowValues"] = string.Empty;
                return (string)ViewState["RowValues"];
            }
            set
            {
                ViewState["RowValues"] = value;
            }
        }

        string KayitTip
        {
            get
            {
                if (ViewState["KayitTip"] == null) ViewState["KayitTip"] = string.Empty;
                return (string)ViewState["KayitTip"];
            }
            set
            {
                ViewState["KayitTip"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override ListingParameters GetListingParameters()
        {
            var parameters = base.GetListingParameters();
            if (!parameters.FieldFilters.Any(p => p.Name == "KayitID"))
                parameters.FieldFilters.Add(new FilteringInfo("KayitID", CurrentID.ToString(), Ext.Net.FilterType.Numeric));
            if (!parameters.FieldFilters.Any(p => p.Name == "KayitTip"))
                parameters.FieldFilters.Add(new FilteringInfo("KayitTip", KayitTip, Ext.Net.FilterType.String));
            return parameters;
        }

        public override void DataBindStore(System.Collections.IList items)
        {
            base.DataBindStore(items);
            if (UserID > 0)
            {
                grid.SelectById(UserID);
                UserID = -1;
            }
        }

        protected void Show(DosyaEk entity)
        {
            ctlAciklama.Text = entity.Aciklama;
            ctlFile.Text = entity.DosyaAd;
        }

        protected void SelectFileHandler(object sender, DirectEventArgs e)
        {
            RowSelectionModel model = grid.SelectionModel.Primary as RowSelectionModel;
            if (string.IsNullOrEmpty(model.SelectedRecordID))
                return;
            CurrentDetailID = int.Parse(model.SelectedRecordID);
            var entity = BusinessObject.Retrieve(CurrentDetailID);
            Show(entity);
        }

        private void uiToEntity(object sender, DosyaEk entity)
        {
            entity.KayitID = CurrentID;
            entity.KayitTip = KayitTip;
            entity.Aciklama = ctlAciklama.Text;
            ctlAciklama.Clear();

            if (ctlFile.PostedFile.ContentLength > 0)
            {
                string FileExtension = Path.GetExtension(ctlFile.PostedFile.FileName.ToString()).ToLowerInvariant();
                if (!new DosyaEkBusiness().ValidateExtension(FileExtension))
                    throw new BusinessException("Yüklenen dosya türü Word, PDF, ZIP veya RAR formatında olmalıdır");

                BinaryReader br = new BinaryReader(ctlFile.PostedFile.InputStream);
                entity.Icerik = br.ReadBytes(Convert.ToInt32(ctlFile.PostedFile.InputStream.Length));
                entity.DosyaAd = ctlFile.PostedFile.FileName.ToString();
                entity.DosyaTip = Path.GetExtension(entity.DosyaAd);
                ctlFile.Reset();
            }
            else
            {
                if (sender == ctlAddBtn)
                    throw new BusinessException("Lütfen yeni ekleyeceğiniz bir dosya seçiniz");
            }
        }

        [CommandHandler(CommandName = "UpdateFile")]
        protected void UpdateFileAttachCommandHandler(object sender, CommandInfo command)
        {
            var values = command.EventArgs.ExtraParams["Values"].ToString();
            var entity = BusinessObject.Retrieve(command.RecordID);
            PermissionControl("Dosya Eki Düzenleme", entity);
            entity.Puan = BusinessObject.getPuan(values);
            uiToEntity(sender, entity);
            BusinessObject.UpdateSingle(entity);
            LoadItems();
        }

        [CommandHandler(CommandName = "SaveFile")]
        protected void SaveFileAttachCommandHandler(object sender, CommandInfo command)
        {
            if (CurrentDetailID > 0)
            {
                UpdateFileAttachCommandHandler(sender, command);
            }
            else CreateFileCommandHandler(sender, command);
        }

        [CommandHandler(CommandName = "CreateFile")]
        protected void CreateFileCommandHandler(object sender, CommandInfo command)
        {
            var entity = new DosyaEk();
            uiToEntity(sender, entity);
            BusinessObject.InsertSingle(entity);
            LoadItems();
            UserID = entity.ID;
        }

        [CommandHandler(CommandName = "DeleteFile")]
        protected void DeleteFileCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            PermissionControl("Dosya Eki Silme", entity);
            BusinessObject.DeleteSingle(entity);
            CurrentDetailID = -1;
            LoadItems();
        }

        [CommandHandler(CommandName = "ShowFiles")]
        protected void ShowFilesCommandHandler(object sender, CommandInfo command)
        {
            ctlGenForm.ClearFields();
            KayitTip = command.Parameters["ID"].ToString();
            CurrentID = command.RecordID;
            entityWindow.Title = string.Format("Dosya Ekleri");
            CurrentDetailID = -1;
            LoadItems();
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "DownloadFile")]
        protected void DownloadFileCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            string url = string.Format("~/DownloadHandler.ashx?id={0}", HttpUtility.UrlEncode(entity.UniqueKey.ToString()));
            url = Page.ResolveClientUrl(url);
            Response.Redirect(url);
        }

        protected void PermissionControl(string permission, DosyaEk entity)
        {
            var haspermission = PermissionBusiness.UserHasPermission(permission);
            if (!haspermission)
            {
                if (entity.aspnet_Users.UserName != Thread.CurrentPrincipal.Identity.Name)
                    throw new SecurityException("Yetersiz işlem yetkisi: " + permission);
            }
        }
    }
}