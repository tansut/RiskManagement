using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class RiskGroupDefinitionBusiness: EntityBusiness<RiskManagement.Framework.Model.RiskGrupTanim>
    {
        public override void UpdateSingle(Model.RiskGrupTanim entity)
        {
            var relations = DataContext.SoruGrupRiskGrup.Where(p => p.RiskGrupID == entity.ID).Select(p => p);
            foreach (var relation in relations)
            {
                DataContext.DeleteObject(relation);
            }
            base.UpdateSingle(entity);
        }

        public List<RiskGrupTanim> RetreiveMandatoryItems()
        {
            var value = Mantiksal.Evet.ToString();
            return ExecuteListQuery(GetQueryable().Where(p => p.ZorunlulukDurumu == value));
        }

        
    }
}
