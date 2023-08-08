using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
   

    public class RiskControlBusiness : EntityBusiness<Kontrol>
    {
        public List<Kontrol> RetreiveItemsOfRisk(int riskId)
        {
            return GetQueryable().Where(p => p.RiskID == riskId).ToList();
        }

      
    }
}
