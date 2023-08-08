using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Web.Pages.Risk
{
    public partial class _default : ViewPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public list getLister()
        {
            return this.ctlLister;
        }
        public AdvancedFiltering getAdvanceFiltering()
        {
            return ctlAdvancedFiltering;
        }

        public CalismaGrubuList getCalismaGrubuList()
        {
            return ctlCalismaGrubuList;
        }

        public KontrolList getKontrolList()
        {
            return ctlKontrolList;
        }
    }
}