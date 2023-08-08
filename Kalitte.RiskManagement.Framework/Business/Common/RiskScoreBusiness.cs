using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class RiskScoreBusiness: EntityBusiness<RiskSkorTanim>
    {

        public int GetEtkiID(string etki)
        {
            return GetQueryable().Where(u => u.EtkiBaslik == etki).Select(p => p.ID).Single();
        }

        public int GetOlasilikID(string olasilik)
        {
            return GetQueryable().Where(u => u.OlasilikBaslik == olasilik).Select(p => p.ID).Single();
        }
    }
}
