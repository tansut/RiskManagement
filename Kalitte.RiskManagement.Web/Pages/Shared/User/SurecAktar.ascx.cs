using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Business.Management;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Controls;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Web.Pages.Shared.User
{
    public partial class SurecAktar : EditorViewControl<UserBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AutoCompleteUser(object sender, AutoCompleteEventArgs e)
        {
            e.Data = new UserBusiness().RetreiveItems(e.Parameters);
        }

        [CommandHandler(CommandName = "SurecAktar")]
        public void SurecAktarHandler(object sender, CommandInfo command)
        {
            entityWindow.Show();
        }

        [CommandHandler(CommandName = "Aktar")]
        public void AktarHandler(object sender, CommandInfo command)
        {
            var userFrom = BusinessObject.Retrieve(ctlUserFrom.SelectedAsInt).UserId;
            var userTo = BusinessObject.Retrieve(ctlUserTo.SelectedAsInt).UserId;
            var sureclist = new WorkflowBusiness().RetrieveWorkflowsofUser(userFrom);
            var workGroups = new CalismaGrupBusiness().GetWorkGroupsByUser(userFrom);
            var rb = new RiskBusiness();

            foreach (var item in workGroups)
            {
                item.KullaniciID = userTo;
            }

            foreach (var item in sureclist)
            {
                item.KullaniciID = userTo;
                var risks = rb.GetRisksByWorkflowID(item.ID);
                foreach (var risk in risks)
                {
                    risk.KullaniciID = userTo;
                }
            }

            BusinessObject.DataContext.SaveChanges();
            WebHelper.ShowMessage("Süreçler başarıyla aktarıldı.");
        }
    }
}