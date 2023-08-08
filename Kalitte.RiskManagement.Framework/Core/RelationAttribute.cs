using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Core
{
    public class RelationAttribute : Attribute
    {
        public string Identifier { get; set; }
        public RelationAttribute(string identifier)
            : base()
        {
            this.Identifier = identifier;
        }
    }
}
