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
    public partial class KontrolList : ListerViewControl<RiskControlBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        [CommandHandler(CommandName = "ShowControls")]
        public void ShowControlsWindow(object sender, CommandInfo cmd)
        {
            dsMain.DataSource = BusinessObject.RetreiveItemsOfRisk(cmd.RecordID);
            dsMain.DataBind();
            
            var riskad = new RiskBusiness().Retrieve(cmd.RecordID).Ad;
            entityWindow.Title = riskad;
            entityWindow.Show();
        }
    }
}