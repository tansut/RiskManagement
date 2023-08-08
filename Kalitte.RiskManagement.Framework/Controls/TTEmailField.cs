using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTEmailField : TTTextField
    {
        public TTEmailField()
            : base()
        {
            this.MaskRe = @"[a-zA-Z0-9_\.\-@]";
            this.Regex = @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            this.RegexText = "Giriş yapılan e-posta formatı geçerli değil";
            AllowBlank = false;
            FieldLabel = "E-Posta";
        }
    }
}
