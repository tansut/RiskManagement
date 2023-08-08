using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTIbanField:TTTextField
    {
        public TTIbanField()
            : base()
        {
            this.Text = "TR";
            this.MaxLength = 26;
            this.Regex = @"^[A-Z][A-Z]\d+$";
            this.RegexText = "Iban no ilk iki karakter harf diğer karakterler rakam olmak üzere 24 karakterli olmalıdır";   
        }
         
    }
}
