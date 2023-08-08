using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class RiskGroupQuestionGroupBusiness : EntityBusiness<SoruGrupRiskGrup>
    {
        public List<SoruGrupRiskGrup> RetreiveAllItemsOfGroup(int[] riskGroups)
        {
            var query = GetQueryable().Where(p=>riskGroups.Contains(p.RiskGrupID));
            return ExecuteListQuery(query);
        }
    }
}
