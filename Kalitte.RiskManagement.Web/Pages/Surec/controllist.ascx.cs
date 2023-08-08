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
    public partial class controllist : ListerViewControl<RiskControlBusiness>
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Ext.Net.X.IsAjaxRequest)
            {

            }
        }

        protected override ListingParameters GetListingParameters()
        {
            var parameters = base.GetListingParameters();
            parameters.FieldFilters.Add(new FilteringInfo("RiskID", CurrentID.ToString(), Ext.Net.FilterType.Numeric));
            return parameters;
        }

        protected override Framework.Controls.TTStore LocateListerStore()
        {
            return this.dsMain;
        }

        protected override Framework.Controls.TTGrid LocateGrid()
        {
            return this.grid;
        }

        protected void SelectControlHandler(object sender, DirectEventArgs e)
        {
            if (grid.RowSelection.SelectedRow == null)
                return;
            CurrentDetailID = int.Parse(grid.RowSelection.SelectedRecordID);
            var entity = BusinessObject.Retrieve(CurrentDetailID);
            ctlName.Text = entity.Ad;
            ctlAciklama.Text = entity.Aciklama;
            ctlPeriod.SelectedText = entity.Siklik;
            ctlIsleyis.SelectedText = entity.Isleyis;
            ctlTip.SelectedText = entity.Tip;
        }


        [CommandHandler(CommandName = "NewControl")]
        protected void NewControlCommandHandler(object sender, CommandInfo command)
        {
            CurrentDetailID = -1;
            ctlGenForm.ClearFields();
        }

        private void uiToEntity(Kontrol entity)
        {
            entity.RiskID = CurrentID;
            entity.Ad = ctlName.Text;
            entity.Aciklama = ctlAciklama.Text;
            entity.Tip = ctlTip.SelectedText;
            entity.Siklik = ctlPeriod.SelectedText;
            entity.Isleyis = ctlIsleyis.SelectedText;
        }

        [CommandHandler(CommandName = "UpdateControl")]
        protected void UpdateRiskCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            uiToEntity(entity);
            BusinessObject.UpdateSingle(entity);
            ctlTip.EndEdit();
            ctlPeriod.EndEdit();
            ctlIsleyis.EndEdit();
            LoadItems();
        }

        [CommandHandler(CommandName = "SaveControl")]
        protected void SaveControlCommandHandler(object sender, CommandInfo command)
        {
            if (CurrentDetailID > 0)
            {
                UpdateRiskCommandHandler(sender, command);
            }
            else CreateControlCommandHandler(sender, command);
        }

        [CommandHandler(CommandName = "CreateControl")]
        protected void CreateControlCommandHandler(object sender, CommandInfo command)
        {
            var entity = new Kontrol();
            uiToEntity(entity);
            BusinessObject.InsertSingle(entity);
            LoadItems();
            grid.SelectById(entity.ID);
            ctlTip.EndEdit();
            ctlPeriod.EndEdit();
            ctlIsleyis.EndEdit();
        }

        [CommandHandler(CommandName = "DeleteControl")]
        protected void DeleteControlCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.DeleteSingle(entity);
            CurrentDetailID = -1;
            LoadItems();
        }


        [CommandHandler(CommandName = "ShowControls")]
        protected void ShowControlsCommandHandler(object sender, CommandInfo command)
        {
            CurrentID = command.RecordID;
            entityWindow.Title = string.Format("Kontroller: {0}", new RiskBusiness().Retrieve(CurrentID).Ad);
            CurrentDetailID = -1;
            NewControlCommandHandler(sender, command);
            LoadItems();
            entityWindow.Show();
        }
    }
}