using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Kalitte.BI.ServerPlatform.Security
{
    class BIIdentity: IIdentity
    {
        private bool iauthenticated = false;
        private string name = "Ziyaretçi";

        public string AuthenticationType
        {
            get { return "Kalitte BI Authentication"; }
        }

        public bool IsAuthenticated
        {
            get { return iauthenticated; }
        }

        public string Name
        {
            get { return name; }
        }

        internal BIIdentity()
        {

        }

        internal BIIdentity(string name)
        {
            this.name = name;
            iauthenticated = true;
        }

        
    }
}
