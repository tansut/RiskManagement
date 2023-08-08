using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Web.Pages.Surec
{
    public partial class _default : ViewPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Surec"] != null)
            {
                var sureclist = new List<Kalitte.RiskManagement.Framework.Model.Surec>();
                var s = Request.QueryString["Surec"];
                string[] surecs = s.Split(',');
                foreach (var item in surecs)
                {
                    sureclist.Add(new WorkflowBusiness().Retrieve(Convert.ToInt32(item)));
                }
                GetLister().BindFromBI(sureclist);
            }
        }

        public UserInfo GetUserInfo()
        {
            return ctlUserInfo;
        }

        public list GetLister()
        {
            return ctlLister;
        }
    }
}