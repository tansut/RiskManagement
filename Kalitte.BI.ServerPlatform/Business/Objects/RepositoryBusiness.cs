using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using System.Threading;
using System.Web.Security;
using System.Configuration;

namespace Kalitte.BI.ServerPlatform.Business.Objects
{
    public class RepositoryBusiness : BaseBusiness
    {


        public List<Module> GetModules()
        {


            List<Module> result = (from p in RepositoryContext.Modules  select p).ToList();

            return result;

        }

        //public List<Chart> GetCharts()
        //{


        //    List<Chart> result = (from p in RepositoryContext.Charts orderby p.Chartname select p).ToList();

        //    return result;

        //}





        //internal Chart GetChart(object chartKey)
        //{
        //    return RepositoryContext.Charts.Single(p => p.ID == (Guid)chartKey);

        //}
    }

}
