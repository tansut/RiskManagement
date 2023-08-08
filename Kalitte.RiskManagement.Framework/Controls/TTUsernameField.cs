using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTUsernameField: TTNumberField
    {
        public TTUsernameField()
            : base()
        {
            FieldLabel = "T.C. Kimlik No";
            AllowBlank = false;
            MinValue = 10000000000;
            MaxValue = 99999999999;
        }

        
    }
}
