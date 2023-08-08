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

namespace Kalitte.RiskManagement.Web.Pages.Management.Permission
{
    public partial class edit : EditorViewControl<RoleBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [CommandHandler(KnownCommand = KnownCommand.UpdateEntity)]
        public void UpdateEntityHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.RetreiveById(CurrentID);
            List<int> selectedPermIds = new List<int>();
            foreach (var item in ctlPermGrid.CheckboxSelection.SelectedRows)
            {
                selectedPermIds.Add(int.Parse(item.RecordID));
            }
            new PermissionBindingBusiness().UpdateBindings(entity, selectedPermIds);
            
            entityWindow.Hide();

        }

        [CommandHandler(KnownCommand = KnownCommand.EditInEditor)]
        public void EditInEditorHandler(object sender, CommandInfo command)
        {
            var entity = BusinessObject.RetreiveById(command.RecordID);
 
            entityWindow.Title = string.Format("Yetkileri Düzenle: {0}", entity.RoleName);
            ctlSave.CommandName = KnownCommand.UpdateEntity.ToString();
            CurrentID = entity.ID;

            var list = new PermissionBusiness().RetreiveEntityItems().OrderBy(p=>p.Yetki1);
            ctlPermGrid.Store.Primary.DataSource = list;
            ctlPermGrid.Store.Primary.DataBind();
            ctlPermGrid.ClearSelections();
            var bindings = new PermissionBindingBusiness().RetreiveItemsOfRole(entity);
            foreach (var binding in bindings)
            {
                SelectedRow sr = new SelectedRow(binding.YetkiID.ToString());
                ctlPermGrid.CheckboxSelection.SelectedRows.Add(sr);
            }
            ctlPermGrid.CheckboxSelection.UpdateSelection();
            entityWindow.Show();
        }



    }
}