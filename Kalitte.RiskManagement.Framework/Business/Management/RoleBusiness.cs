using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class RoleBusiness: EntityBusiness<aspnet_Roles>
    {
        public aspnet_Roles RetreiveById(int id)
        {
            return GetQueryable().Single(p => p.ID == id);
        }

        public aspnet_Roles RetrieveByRoleName(string rolename)
        {
            return GetQueryable().Single(p => p.RoleName == rolename);
        }

    }
}
