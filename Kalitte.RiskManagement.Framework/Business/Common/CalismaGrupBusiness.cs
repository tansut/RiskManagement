using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Framework.Business.Common
{
    public class CalismaGrupBusiness : EntityBusiness<CalismaGrupTanim>
    {

        public void UpdateSingle(CalismaGrupTanim entity, List<CalismaGrupKullanici> newBindings)
        {
            var temp = entity.CalismaGrupKullanici.Select(p => p).ToList();
            var bll = new CalismaGrupKullaniciBusiness();
            foreach (var item in temp)
            {
                bll.Delete(item);
            }

            entity.CalismaGrupKullanici.Clear();
            foreach (var newBinding in newBindings)
            {
                entity.CalismaGrupKullanici.Add(newBinding);
            }
            base.UpdateSingle(entity);
        }

        public List<aspnet_Users> GetAllGroupUsersOfCurrentUser(bool addCurrentUser=false)
        {
            List<aspnet_Users> result = new List<aspnet_Users>();
            List<Guid> relatedUsers = DataContext.CalismaGrupKullanici.Where(p=>p.CalismaGrupTanim.KullaniciID == AuthenticationManager.CurrentUserID).Select(p=>p.KatilimciKullaniciID).Distinct().ToList();
            result.AddRange(DataContext.aspnet_Users.Where(p=>p.Disabled==null && relatedUsers.Contains(p.UserId)).Select(p=>p).ToList());
            if (addCurrentUser)
            {
                var user = DataContext.aspnet_Users.First(d => d.UserId == AuthenticationManager.CurrentUserID);
                if(!result.Contains(user))
                    result.Add(user);
            }
            return result;
        }


        public List<CalismaGrupTanim> GetWorkGroupsByUser(Guid userID)
        {
        return    DataContext.CalismaGrupTanim.Where(d => d.KullaniciID == userID).ToList();

        }


       
    }
}
