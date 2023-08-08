using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Threading;
using System.Web;
using Kalitte.BI.Shared.Exceptions;

namespace Kalitte.BI.ServerPlatform.Security
{
    public static class SecurityManager
    {
        public static bool ValidateUser(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);

        }

        public static void LoginUser(string userName, string password)
        {
            CreatePrincipal(userName);
        }

        private static void CreatePrincipal(string userName)
        {
            BIIdentity identity = new BIIdentity(userName);
            BIPrincipal principal = new BIPrincipal(identity);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}
