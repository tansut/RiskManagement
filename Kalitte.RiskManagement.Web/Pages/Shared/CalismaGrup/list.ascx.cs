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

namespace Kalitte.RiskManagement.Web.Pages.Shared.CalismaGrup
{
    public partial class list : ListerViewControl<CalismaGrupBusiness>
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override System.Collections.IList GetItems()
        {
            BusinessObject.PermissionMode = RiskManagement.Framework.Business.EntityPermissonMode.User;
            return base.GetItems();
        }
    }
}