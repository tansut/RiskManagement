using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using Kalitte.BI.ServerPlatform.Business.Objects;

namespace Kalitte.BI.ServerPlatform.Business.RepositoryManagement
{
    public static class RepositoryManager
    {        
        public static List<Module> GetModules()
        {
            return new RepositoryBusiness().GetModules();
        }

        //public static List<Chart> GetCharts()
        //{
        //    return new RepositoryBusiness().GetCharts();
        //}

        //public static Chart GetChart(object chartKey)
        //{
        //    return new RepositoryBusiness().GetChart(chartKey);
        //}
    }
}
