using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using System.Threading;
using System.Security;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class PermissionBusiness : EntityBusiness<Yetki>
    {
        private static object synObj = new object();
        private static Dictionary<string, HashSet<string>> cache;

        static PermissionBusiness()
        {
            cache = new Dictionary<string, HashSet<string>>(25);
        }

        public static bool UserHasPermission(string permission)
        {
            HashSet<string> roles = null;
            lock (synObj)
            {
                if (cache.ContainsKey(permission))
                    roles = new HashSet<string>(cache[permission]);
            }
            if (roles == null)
            {
                lock (synObj)
                {
                    if (cache.ContainsKey(permission))
                        roles = new HashSet<string>(cache[permission]);
                    else
                    {
                        var bll = new PermissionBusiness();
                        var entity = bll.GetQueryable().SingleOrDefault(p => p.Yetki1 == permission);
                        if (entity == null)
                        {
                            entity = new Yetki();
                            entity.Yetki1 = permission;
                            bll.InsertSingle(entity);
                        }
                        roles = new HashSet<string>(entity.YetkiRol.Select(p => p.aspnet_Roles.RoleName));
                        cache.Add(permission, roles);
                    }
                }
            }
            foreach (var item in roles)
	        {
                if (Thread.CurrentPrincipal.IsInRole(item))
                    return true;
	        }
            return false;
        }

        public override void DeletetingEntity(Yetki entity)
        {
            base.DeletetingEntity(entity);
            //lock (synObj)
            //{
            //    cache.Clear();
            //}
        }



        public static void ValidatePermission(string permission)
        {
            if (!UserHasPermission(permission))
                throw new SecurityException("Yetersiz işlem yetkisi: " + permission);
        }

        protected override void UpdatingEntity(Yetki entity)
        {
            base.UpdatingEntity(entity);
            //lock (synObj)
            //{
            //    cache.Clear();
            //}
        }

        public static void ClearCache()
        {
            lock (synObj)
            {
                cache.Clear();
            }
        }



        protected override void InsertingEntity(Yetki entity)
        {
            base.InsertingEntity(entity);
            //lock (synObj)
            //{
            //    cache.Clear();
            //}
        }

        public static void ValidatePermission(object permissionObj)
        {
            var obj = permissionObj as ISupportsPermission;
            if (obj == null)
                return;
            string permission = obj.Permission;
            if (!string.IsNullOrEmpty(permission) && !UserHasPermission(permission))
                throw new SecurityException("Yetersiz işlem yetkisi: " + permission);
        }
    }
}
