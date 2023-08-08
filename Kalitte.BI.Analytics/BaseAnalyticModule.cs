using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.Shared.AnalyticsService;

namespace Kalitte.BI.Analytics
{
    public abstract class BaseAnalyticModule
    {
        public abstract void Execute(ModuleInfo moduleInfo, IAnaliticsApp AnaliticsApplication);
    }
}
