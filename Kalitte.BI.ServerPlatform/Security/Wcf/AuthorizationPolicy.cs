using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.IdentityModel.Claims;

namespace Kalitte.BI.ServerPlatform.Security.Wcf
{
    internal sealed class AuthorizationPolicy : IAuthorizationPolicy
    {
        string id = Guid.NewGuid().ToString();

        public bool Evaluate(EvaluationContext context, ref object state)
        {
            object obj;
            if (!context.Properties.TryGetValue("Identities", out obj))
                return false;

            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (obj == null || identities.Count <= 0)
                return false;

            BIIdentity identity = new BIIdentity(identities[0].Name);
            context.Properties["Principal"] = new BIPrincipal(identity);
            return true;
        }

        public System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }


        public string Id
        {
            get { return this.id; }
        }


    }
}
