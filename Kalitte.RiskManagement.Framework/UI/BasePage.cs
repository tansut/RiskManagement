using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Ext.Net;
using System.Web;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Framework.UI
{
    public class BasePage: Page
    {
        private static readonly HashSet<string> validDomains;

        static BasePage()
        {
            validDomains = new HashSet<string>();
            validDomains.Add("localhost");
            validDomains.Add("risk.cevresehircilik.gov.tr");
            validDomains.Add("risk.kalitte.local");
        }

        protected override void OnInit(EventArgs e)
        {
            ValidateDomain();
            base.OnInit(e);
        }

        private void ValidateDomain()
        {
            string current = HttpContext.Current.Request.Url.Host.ToLowerInvariant();
            bool isValid = current.Contains(current.ToLowerInvariant());
            if (!isValid)
                throw new TechnicalException("Invalid domain");
        }

        public ResourceManager ResMan
        {
            get
            {
                return ResourceManager.GetInstance(this);
            }
        }
    }
}
