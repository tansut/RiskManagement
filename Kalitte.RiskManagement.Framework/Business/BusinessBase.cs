using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using System.Web;
using System.Collections;
using Kalitte.RiskManagement.Framework.Core;
using System.Data.EntityClient;
using System.Configuration;

namespace Kalitte.RiskManagement.Framework.Business
{
    public enum EntityPermissonMode
    {
        Unit,
        User,
        All
    }

    public abstract class BusinessBase
    {
        public EntityPermissonMode PermissionMode { get; set; }


        private string buildConnectionString()
        {
            string providerName = "System.Data.SqlClient";
            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

            entityBuilder.Provider = providerName;

            entityBuilder.ProviderConnectionString = ConfigurationManager.ConnectionStrings["SqlConStr"].ConnectionString;

            entityBuilder.Metadata = @"res://*/Model.RiskModel.csdl|
                            res://*/Model.RiskModel.ssdl|
                            res://*/Model.RiskModel.msl";
            return entityBuilder.ToString();
        }

        protected RiskManagementDb dataContext = null;

        public RiskManagementDb DataContext
        {
            get
            {
                if (dataContext == null)
                {

                    if (HttpContext.Current.Items.Contains("__datacontext"))
                        dataContext = (RiskManagementDb)HttpContext.Current.Items["__datacontext"];
                    else
                    {
                        dataContext = NewDataContext();
                        if (HttpContext.Current != null)
                            HttpContext.Current.Items["__datacontext"] = dataContext;
                    }
                    return dataContext;
                }
                else return dataContext;
            }
        }

        public BusinessBase()
        {
            PermissionMode = EntityPermissonMode.Unit;
        }

        public RiskManagementDb NewDataContext()
        {
            dataContext = new RiskManagementDb(buildConnectionString());
            return dataContext;
        }

        public abstract IList RetreiveItems(ListingParameters parameters = null);
        public abstract Type EntityType { get; }
    }
}
