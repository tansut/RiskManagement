using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public partial class CalismaGrubuList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Bind(int riskid)
        {
            dsMain.DataSource = dsMain.DataSource = new CalismaGrupKullaniciBusiness().GetCalismaGrupDurum(riskid);
            dsMain.DataBind();
            entityWindow.Show();
        }
    }
}