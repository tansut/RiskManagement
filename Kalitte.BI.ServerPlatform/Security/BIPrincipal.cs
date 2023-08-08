using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web.Security;

namespace Kalitte.BI.ServerPlatform.Security
{
    class BIPrincipal: IPrincipal
    {
        private BIIdentity identity;

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }

        internal BIPrincipal(BIIdentity identity)
        {
            this.identity = identity;
        }

        
    }
}
