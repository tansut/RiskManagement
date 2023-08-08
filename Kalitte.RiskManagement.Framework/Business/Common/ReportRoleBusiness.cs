using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class ReportRoleBusiness : EntityBusiness<RaporRol>
    {
        public string[] GetReportRoles(int reportid)
        {
            return GetQueryable().Where(p => p.RaporID == reportid).Select(u => u.aspnet_Roles.RoleName).ToArray();
        }

        public void DeleteRolesfromReport(int reportid)
        {
            var reportRoles = GetQueryable().Where(p => p.RaporID == reportid).ToList();
            foreach (var item in reportRoles)
            {
                DeleteSingle(item);
            }
        }

        public void AddRolestoReport(int reportid, string[] rolename)
        {
            foreach (var item in rolename)
            {
                aspnet_Roles role = new RoleBusiness().RetrieveByRoleName(item);
                RaporRol rr = new RaporRol();
                rr.RaporID = reportid;
                rr.RoleId = role.RoleId;
                InsertSingle(rr);
            }
        }
    }
}
