using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.UI
{
    public interface IUnitSelector
    {
        event EventHandler OnFilter;
        void Bind();
    }
}
