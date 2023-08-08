using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using System.Web.Configuration;

namespace Kalitte.BI.ServerPlatform.Business.Objects
{
    public abstract class BaseBusiness
    {
        private RepositoryDataContext repositoryContext = null;

        public RepositoryDataContext RepositoryContext
        {
            get
            {
                if (repositoryContext == null)
                {
                    repositoryContext = new RepositoryDataContext(WebConfigurationManager.ConnectionStrings["SqlConstr"].ConnectionString);
                }

                return repositoryContext;
            }
        }
    }
}
