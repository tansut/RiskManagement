using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.Web.Security;
using Kalitte.BI.Shared.Exceptions;
using System.IdentityModel.Tokens;

namespace Kalitte.BI.ServerPlatform.Security.Wcf
{
    class MembershipPasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (!SecurityManager.ValidateUser(userName, password))
                throw new SecurityTokenException("Geçersiz kullanıcı adı / şifresi");
        }
    }
}
