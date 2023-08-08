using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class PermissionBindingBusiness : EntityBusiness<YetkiRol>
    {
        public List<YetkiRol> RetreiveItemsOfRole(aspnet_Roles role)
        {
            return ExecuteListQuery(GetQueryable().Where(p=>p.RoleID == role.RoleId).OrderBy(p=>p.Yetki.Yetki1));
        }

        public void UpdateBindings(aspnet_Roles entity, List<int> selectedPermIds)
        {
            var currentBindings = RetreiveItemsOfRole(entity);
            foreach (var item in currentBindings)
            {
                DataContext.DeleteObject(item);
            }
            foreach (var item in selectedPermIds)
            {
                var newEntity = new YetkiRol();
                newEntity.RoleID = entity.RoleId;
                newEntity.YetkiID = item;
                Insert(newEntity);
            }
            DataContext.SaveChanges();
            PermissionBusiness.ClearCache();
        }
    }
}
