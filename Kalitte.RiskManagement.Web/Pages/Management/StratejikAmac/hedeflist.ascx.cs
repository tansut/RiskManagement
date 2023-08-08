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

namespace Kalitte.RiskManagement.Web.UI.Pages.Management.StratejikAmac
{

    public partial class hedeflist : ListerViewControl<HedefBusiness>
    {
         protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected override ListingParameters GetListingParameters()
        {
            var parameters = base.GetListingParameters();
            if (!parameters.FieldFilters.Any(p => p.Name == "StratejikAmacID"))
                parameters.FieldFilters.Add(new FilteringInfo("StratejikAmacID", CurrentID.ToString(), Ext.Net.FilterType.Numeric));
            return parameters;
        }

        //public override void DataBindStore(System.Collections.IList items)
        //{
        //    base.DataBindStore(items);
        //    if (UserID > 0)
        //    {
        //        grid.SelectById(UserID);
        //        UserID = -1;
        //    }
        //}

        protected void SelectHedefHandler(object sender, DirectEventArgs e)
        {
            if (grid.RowSelection.SelectedRow == null)
                return;
            CurrentDetailID = int.Parse(grid.RowSelection.SelectedRecordID);
            var entity = BusinessObject.Retrieve(CurrentDetailID);
             ctlAd.Text = entity.Ad;
             ctlAciklama.Text = entity.Aciklama;
        }

        private void uiToEntity(Kalitte.RiskManagement.Framework.Model.Hedef entity)
        {
            entity.StratejikAmacID = CurrentID;
            entity.Ad = ctlAd.Text;
            entity.Aciklama = ctlAciklama.Text;
        }

        protected override Framework.Controls.TTStore LocateListerStore()
        {
            return this.dsMain;
        }

        protected override Framework.Controls.TTGrid LocateGrid()
        {
            return this.grid;
        }

        [CommandHandler(CommandName = "UpdateHedef")]
        protected void UpdateHedefCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            uiToEntity(entity);
            BusinessObject.UpdateSingle(entity);
            LoadItems();
        }

        [CommandHandler(CommandName = "SaveHedef")]
        protected void SaveHedefCommandHandler(object sender, CommandInfo command)
        {
            if (CurrentDetailID > 0)
            {
                UpdateHedefCommandHandler(sender, command);
            }
            else CreateHedefCommandHandler(sender, command);
        }

        [CommandHandler(CommandName = "CreateHedef")]
        protected void CreateHedefCommandHandler(object sender, CommandInfo command)
        {
            var entity = new Kalitte.RiskManagement.Framework.Model.Hedef();
            uiToEntity(entity);
            BusinessObject.InsertSingle(entity);
            LoadItems();
            UserID = entity.ID;
        }

        [CommandHandler(CommandName = "DeleteHedef")]
        protected void DeleteHedefCommandHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.Retrieve(command.RecordID);
            BusinessObject.DeleteSingle(entity);
            CurrentDetailID = -1;
            LoadItems();
        }

        [CommandHandler(CommandName = "ShowHedef")]
        protected void ShowHedefCommandHandler(object sender, CommandInfo command)
        {
            CurrentID = command.RecordID;
            entityWindow.Title = string.Format("Hedefler: {0}", new StratejikAmacBusiness().Retrieve(CurrentID).Ad);
            CurrentDetailID = -1;
            ctlGenForm.ClearFields();
            LoadItems();
            entityWindow.Show();
        }
    }
}