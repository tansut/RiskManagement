using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Security
{
    public static class RoleConstants
    {
        public const string GlobalAdmin = "Global Sistem Yöneticisi";
        public const string GlobalWorkflowManager = "Global Süreç Sorumlusu";
        public const string UnitAdmin = "Birim Sistem Yöneticisi";
        public const string UnitWorkflowManager = "Birim Süreç Sorumlusu";
        public const string UnitWorker = "Birim Çalışanı";
        public const string Auditor = "İç Denetçi";

        private static HashSet<string> globalRoles;
        private static HashSet<string> unitRoles;

        public static HashSet<string> GlobalRoles
        {
            get
            {
                return globalRoles;
            }
        }

        public static HashSet<string> UnitRoles
        {
            get
            {
                return unitRoles;
            }
        }

        static RoleConstants()
        {
            globalRoles = new HashSet<string>();
            globalRoles.Add(GlobalAdmin);
            globalRoles.Add(GlobalWorkflowManager);
            globalRoles.Add(Auditor);

            unitRoles = new HashSet<string>();
            unitRoles.Add(UnitAdmin);
            unitRoles.Add(UnitWorker);
            unitRoles.Add(UnitWorkflowManager);
        }
    }
}
