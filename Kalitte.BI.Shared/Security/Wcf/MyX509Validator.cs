using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace Kalitte.BI.Shared.Security.Wcf
{
    public class MyX509Validator : X509CertificateValidator
    {
        public override void Validate(X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");

            if (certificate.SubjectName.Name != "CN=MyServerCert")
                throw new SecurityTokenValidationException("Certificated was not issued by thrusted issuer");
        }
    }
}
