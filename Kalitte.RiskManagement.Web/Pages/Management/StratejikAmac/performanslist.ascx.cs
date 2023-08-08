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

namespace Kalitte.RiskManagement.Web.Pages.Management.StratejikAmac
{
    public partial class performanslist : ListerViewControl<PerformansBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override ListingParameters GetListingParameters()
        {
            var parameters = base.GetListingParameters();
            if (!parameters.FieldFilters.Any(p => p.Name == "HedefID"))
                parameters.FieldFilters.Add(new FilteringInfo("HedefID", CurrentID.ToString(), Ext.Net.FilterType.Numeric));
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

        protected void SelectPerformansHandler(object sender, DirectEventArgs e)
        {
            if (grid.RowSelection.SelectedRow == null)
                return;
            CurrentDetailID = int.Parse(grid.RowSelection.SelectedRecordID);
            var entity = BusinessObject.Retrieve(CurrentDetailID);
            ctlAd.Text = entity.Ad;
            ctlAciklama.Text = entity.Aciklama;
        }

        private void uiToEntity(PerformansGostergesi entity)
        {
            entity.HedefID = CurrentID;
            entity.Ad = ctlAd.Text;
            entity.Aciklama = ctlAciklama.Text;
        }

        [CommandHandler(CommandName = "UpdatePerformans")]
        protected void UpdatePerformansCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            uiToEntity(entity);
            BusinessObject.UpdateSingle(entity);
            LoadItems();
        }

        [CommandHandler(CommandName = "SavePerformans")]
        protected void SavePerformansCommandHandler(object sender, CommandInfo command)
        {
            if (CurrentDetailID > 0)
            {
                UpdatePerformansCommandHandler(sender, command);
            }
            else CreatePerformansCommandHandler(sender, command);
        }

        [CommandHandler(CommandName = "CreatePerformans")]
        protected void CreatePerformansCommandHandler(object sender, CommandInfo command)
        {
            var entity = new PerformansGostergesi();
            uiToEntity(entity);
            BusinessObject.InsertSingle(entity);
            LoadItems();
            grid.SelectById(entity.ID);
        }

        [CommandHandler(CommandName = "DeletePerformans")]
        protected void DeletePerformansCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.DeleteSingle(entity);
            CurrentDetailID = -1;
            LoadItems();
        }

        [CommandHandler(CommandName = "ShowPerformans")]
        protected void ShowPerformansCommandHandler(object sender, CommandInfo command)
        {
            CurrentID = command.RecordID;
            entityWindow.Title = string.Format("Performans Göstergeleri: {0}", new HedefBusiness().Retrieve(CurrentID).Ad);
            CurrentDetailID = -1;
            ctlGenForm.ClearFields();
            LoadItems();
            entityWindow.Show();
        }
    }
}