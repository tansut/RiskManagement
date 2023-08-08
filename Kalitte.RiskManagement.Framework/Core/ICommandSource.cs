using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Business;

namespace Kalitte.RiskManagement.Framework.Core
{
    public interface ICommandSource
    {
        BusinessBase ControllerObject { get; }
    }
}
