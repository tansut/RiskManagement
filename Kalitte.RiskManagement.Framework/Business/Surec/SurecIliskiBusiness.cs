using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class SurecIliskiBusiness : EntityBusiness<SurecIliski>
    {
        public void DeleteBySurec(int surecId)
        {
            var query = DataContext.SurecIliski.Where(p => p.KaynakSurecID == surecId);
            foreach (var item in query)
            {
                DataContext.DeleteObject(item);
            }

        }

        public bool IsExistingAsHedef(int surecid)
        {
            return GetQueryable().Where(p => p.HedefSurecID == surecid).Select(u => u.KaynakSurecID).Any();
        }
    }
}
