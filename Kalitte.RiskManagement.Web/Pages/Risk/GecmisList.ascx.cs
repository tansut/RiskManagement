using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public partial class GecmisList : ListerViewControl<RiskGecmisBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [CommandHandler(CommandName = "ShowHistory")]
        public void ShowRiskHistory(object sender, CommandInfo cmd)
        {
            dsMain.DataSource = BusinessObject.GetHistorybyRiskID(cmd.RecordID);
            dsMain.DataBind();
            
            var riskEntity = new RiskBusiness().Retrieve(cmd.RecordID);
            entityWindow.Title = string.Format("Risk Geçmişi : {0}", riskEntity.Ad);
            entityWindow.Show();
        }
    }
}