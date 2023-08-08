using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Surec
{
    public class SurecYararlananBusiness : EntityBusiness<SurecYararlanan>
    {
        public void DeleteBySurec(int surecId)
        {
            var query = DataContext.SurecYararlanan.Where(p => p.SurecID == surecId);
            foreach (var item in query)
            {
                DataContext.DeleteObject(item);
            }

        }
    }
}
