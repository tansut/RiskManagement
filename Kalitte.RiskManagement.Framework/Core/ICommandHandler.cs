using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Controls;

namespace Kalitte.RiskManagement.Framework.Core
{
    public interface ICommandHandler
    {
        bool ProcessCommand(object sender, CommandInfo cmd);
    }
}
