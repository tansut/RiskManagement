using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class SurecDayanakBusiness: EntityBusiness<SurecDayanak>
    {
        public void DeleteBySurec(int surecId)
        {
            var query = DataContext.SurecDayanak.Where(p=>p.SurecID == surecId);
            foreach (var item in query)
            {
                DataContext.DeleteObject(item);
            }
            
        }
    }
}
