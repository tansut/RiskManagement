using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.Analytics.UserInterface;

namespace Kalitte.BI.Analytics.RiskAnalizModule
{
    public class RiskAnalizModule : BaseAnalyticModule
    {
        public override void Execute(Kalitte.BI.Shared.AnalyticsService.ModuleInfo moduleInfo, IAnaliticsApp application)
        {
            AnalyticsForm f;

            switch (moduleInfo.ModuleName)
            {
                case "Risk":
                    f = new RiskAnalizForm(moduleInfo, application);
                    f.Execute();
                    break;
                case "Kontrol":
                    f = new KontrolAnalizForm(moduleInfo, application);
                    f.Execute();
                    break;
                case "Süreç":
                    f = new SurecAnalizForm(moduleInfo, application);
                    f.Execute();
                    break;
                case "Risk Geçmiş":
                    f = new RiskGecmisAnalizForm(moduleInfo, application);
                    f.Execute();
                    break;          
                default:
                    break;
            }
        }
    }
}
