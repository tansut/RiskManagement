using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Web.Pages.Surec
{
    public partial class risklist : ListerViewControl<RiskBusiness>
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.Net.X.IsAjaxRequest)
            {
                var calismaGrupBll = new CalismaGrupBusiness();
                calismaGrupBll.PermissionMode = Framework.Business.EntityPermissonMode.User;
                ctlCalisanGrup.Store.Primary.DataSource = calismaGrupBll.RetreiveItems();
                ctlCalisanGrup.Store.Primary.DataBind();
                ctlGroups.Store.Primary.DataSource = new RiskGroupDefinitionBusiness().RetreiveItems();
                ctlGroups.Store.Primary.DataBind();
            }
        }

        protected override ListingParameters GetListingParameters()
        {
            var parameters = base.GetListingParameters();
            if (!parameters.FieldFilters.Any(p => p.Name == "SurecID"))
                parameters.FieldFilters.Add(new FilteringInfo("SurecID", CurrentID.ToString(), Ext.Net.FilterType.Numeric));
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

        protected void ShowRisk(Kalitte.RiskManagement.Framework.Model.Risk entity)
        {
            ctlName.Text = entity.Ad;
            ctlAciklama.Text = entity.Aciklama;
            ctlDurum.SelectedAsString = entity.Durum;
            ctlCalisanGrup.SelectedAsInt = entity.CalisanGrupID;
            var allGroups = new RiskGroupDefinitionBusiness().RetreiveEntityItems();
            foreach (var group in allGroups)
            {
                ctlGroups.DeselectItem(group.ID.ToString());
            }
            foreach (var group in entity.RiskGrup)
            {
                ctlGroups.SelectItem(group.RiskGrupID.ToString());
            }
        }

        protected void SelectRiskHandler(object sender, DirectEventArgs e)
        {
            if (grid.RowSelection.SelectedRow == null)
                return;
            CurrentDetailID = int.Parse(grid.RowSelection.SelectedRecordID);
            var entity = BusinessObject.Retrieve(CurrentDetailID);
            ShowRisk(entity);
        }


        [CommandHandler(CommandName = "NewRisk")]
        protected void NewRisksCommandHandler(object sender, CommandInfo command)
        {
            CurrentDetailID = -1;
            ctlGenForm.ClearFields();
            var mandatoryGroups = new RiskGroupDefinitionBusiness().RetreiveMandatoryItems();
            foreach (var item in mandatoryGroups)
            {
                ctlGroups.SelectItem(item.ID.ToString());
            }
            ctlDurum.SelectedAsString = RiskDurum.Taslak.ToString();
        }

        [CommandHandler(CommandName = "ChangeStatusTaslak")]
        protected void ChangeStatusTaslakCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.ChangeRiskStatus(entity, RiskDurum.Taslak);
            LoadItems();
            ShowRisk(entity);

        }

        [CommandHandler(CommandName = "ChangeStatusPuanlamaBekler")]
        protected void ChangeStatusPuanlamaBeklerCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.ChangeRiskStatus(entity, RiskDurum.PuanlamaBekler);
            LoadItems();
            ShowRisk(entity);
        }




        private void uiToEntity(Kalitte.RiskManagement.Framework.Model.Risk entity)
        {
            entity.SurecID = CurrentID;
            entity.Ad = ctlName.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.CalisanGrupID = ctlCalisanGrup.SelectedAsInt;            
            foreach (var group in ctlGroups.SelectedItems)
            {
                entity.RiskGrup.Add(new RiskGrup() { RiskGrupID = int.Parse(group.Value) });
            }
        }

        protected override Framework.Controls.TTStore LocateListerStore()
        {
            return this.dsMain;
        }

        protected override Framework.Controls.TTGrid LocateGrid()
        {
            return this.grid;
        }

        [CommandHandler(CommandName = "UpdateRisk")]
        protected void UpdateRiskCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            //if (entity.RiskDurum != RiskDurum.Taslak)
            //    throw new BusinessException("Durumu sadece Taslak olan risk bilgileri değiştirilebilir");
            entity.RiskGrup.Clear();
            uiToEntity(entity);
            BusinessObject.UpdateSingle(entity);
            LoadItems();
        }

        [CommandHandler(CommandName = "SaveRisk")]
        protected void SaveRisksCommandHandler(object sender, CommandInfo command)
        {
            if (CurrentDetailID > 0)
            {
                UpdateRiskCommandHandler(sender, command);
            }
            else CreateRisksCommandHandler(sender, command);
        }

        [CommandHandler(CommandName = "CreateRisk")]
        protected void CreateRisksCommandHandler(object sender, CommandInfo command)
        {
            var mandatoryGroups = new RiskGroupDefinitionBusiness().RetreiveMandatoryItems();
            foreach (var item in mandatoryGroups)
            {
                if (!ctlGroups.SelectedItems.Any(p => p.Value == item.ID.ToString()))
                    throw new BusinessException(string.Format("Zorunlu seçilmesi gereken risk gruplarını lütfen seçiniz. Grup: {0}", item.Ad));
            }
            var entity = new Kalitte.RiskManagement.Framework.Model.Risk();
            uiToEntity(entity);
            entity.RiskDurum = Framework.Model.Common.RiskDurum.Taslak;
            BusinessObject.InsertSingle(entity);
            LoadItems();
            UserID = entity.ID;
        }


        [CommandHandler(CommandName = "DeleteRisk")]
        protected void DeleteRiskCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.DeleteSingle(entity);
            CurrentDetailID = -1;
            LoadItems();
        }

        [CommandHandler(CommandName = "ShowRisks")]
        protected void ShowRisksCommandHandler(object sender, CommandInfo command)
        {
            CurrentID = command.RecordID;
            entityWindow.Title = string.Format("Riskler: {0}", new WorkflowBusiness().Retrieve(CurrentID).Ad);
            CurrentDetailID = -1;

            NewRisksCommandHandler(sender, command);
            LoadItems();
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "ShowRisk")]
        protected void ShowRiskCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            CurrentID = entity.SurecID;
            entityWindow.Title = string.Format("Riskler: {0}", new WorkflowBusiness().Retrieve(CurrentID).Ad);
            CurrentDetailID = -1;
            LoadItems();
            UserID = entity.ID;
            entityWindow.Show();
        }

    }
}