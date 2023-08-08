using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Core;
using System.Web.Security;
using Kalitte.RiskManagement.Framework.Security;
using System.Threading;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Business.Surec;

namespace Kalitte.RiskManagement.Framework.Business.Management
{
    public class UserBusiness : EntityBusiness<aspnet_Users>
    {
        protected override IQueryable<aspnet_Users> FilteredQuery(IQueryable<aspnet_Users> query, ListingParameters e = null)
        {
            if (e != null && e.UserParams.ContainsKey("AdSoyad"))
            {
                var filter = e.UserParams["AdSoyad"].ToString();
                string[] str = filter.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var first = str[0].Trim();
                var firstTrimmed = first.Trim("%*".ToCharArray());

                if (first.Length == firstTrimmed.Length)
                    query = query.Where(p => p.Ad == firstTrimmed);
                else
                {
                    if (first[0] == '%')
                        query = query.Where(p => p.Ad.EndsWith(firstTrimmed));
                    else
                    {
                        switch (first[first.Length - 1])
                        {
                            case '%': query = query.Where(p => p.Ad.StartsWith(firstTrimmed));
                                break;
                            case '*': query = query.Where(p => p.Ad.Contains(firstTrimmed));
                                break;
                        }
                    }
                }

                if (str.Length == 2)
                {
                    var second = str[1].Trim();
                    var secondTrimmed = second.Trim("%*".ToCharArray());
                    if (second.Length == secondTrimmed.Length)
                        query = query.Where(p => p.Soyad == secondTrimmed);
                    else
                        switch (second[second.Length - 1])
                        {
                            case '%': query = query.Where(p => p.Soyad.StartsWith(secondTrimmed)); break;
                            case '*': query = query.Where(p => p.Soyad.Contains(secondTrimmed)); break;
                        }
                }
            }
            
            if (e != null && !string.IsNullOrEmpty(e.Query))
            {
                e.FieldFilters.Clear();
                e.FieldFilters.Operator = FilterListOperator.Or;
                e.FieldFilters.Add(new FilteringInfo("UserName", e.Query));
                e.FieldFilters.Add(new FilteringInfo("Ad", e.Query, StringSearchType.Like));
                e.FieldFilters.Add(new FilteringInfo("Soyad", e.Query, StringSearchType.Like));
            }
            var users = base.FilteredQuery(query, e);

            return users.Where(d => d.Disabled == null);
        }

        //protected override IQueryable<aspnet_Users> PermissionQuery(IQueryable<aspnet_Users> query)
        //{
        //    return query;
        //}

        public static aspnet_Users GetUser()
        {
            return new UserBusiness().RetreiveByUsername(Thread.CurrentPrincipal.Identity.Name);
        }

        public override aspnet_Users Retrieve(int id)
        {
            return DataContext.aspnet_Users.Single(p => p.ID == id);
        }

        public override void DeleteSingle(aspnet_Users entity)
        {
                Membership.DeleteUser(entity.UserName);   
        }

        public void LockUser(aspnet_Users entity)
        {
            var user=Membership.GetUser(entity.UserName);
            user.IsApproved = false;
            entity.Disabled = true;
            Membership.UpdateUser(user);
            dataContext.SaveChanges();
        }

        public void UnLockUser(aspnet_Users entity)
        {
            var user = Membership.GetUser(entity.UserName);
            user.IsApproved = true;
            entity.Disabled = null;
            Membership.UpdateUser(user);
            dataContext.SaveChanges();
        }

        public aspnet_Users ValidateUser(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
                throw new BusinessException("Kullanıcı bulunamadı");
            if (!user.IsApproved)
                throw new BusinessException("Kullanıcı henüz onaylanmamış");
            return RetreiveByUsername(userName);
        }

        public aspnet_Users RetreiveByUsername(string username)
        {
            return DataContext.aspnet_Users.SingleOrDefault(p => p.UserName == username);
        }

        public void CreateUser(string username, string password, string email, string ad, string soyad, bool enabled, int birimId, int unvanId, string[] roles)
        {
            if (!IsValidTCIdentityNumber(long.Parse(username)))
                throw new BusinessException("Belirtilen kullanıcı adı geçerli bir T.C. Kimlik numarası değildir");
            MembershipCreateStatus status;
            var newUser = Membership.CreateUser(username, password, email, null, null, enabled, out status);
            bool unlockUser = false;
            switch (status)
            {
                case MembershipCreateStatus.DuplicateEmail:
                
                    throw new BusinessException("Mükerrer e-posta adresi tespit edildiği için kullanıcı oluşturulamadı");
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    throw new BusinessException("Kullanıcı sistemde zaten bulunmaktadır (ID)"); ;
                case MembershipCreateStatus.DuplicateUserName:
                    var existingUser = RetreiveByUsername(username);
                    if (existingUser.Disabled != null)
                    {
                        unlockUser = true;
                        break;

                    }    
                    throw new BusinessException("Kullanıcı sistemde zaten bulunmaktadır"); ;
                case MembershipCreateStatus.InvalidAnswer:
                    throw new BusinessException("Geçersiz şifre sıfırmala yanıtı"); ;
                case MembershipCreateStatus.InvalidEmail:
                    throw new BusinessException("Geçersiz e-posta adresi"); ;
                case MembershipCreateStatus.InvalidPassword:
                    throw new BusinessException("Geçersiz şifre. Lütfen şifrenin yeterli uzunlukta olduğunu kontrol edin."); ;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    throw new BusinessException("Geçersiz sağlayıcı anahtarı"); ;
                case MembershipCreateStatus.InvalidQuestion:
                    throw new BusinessException("Geçersiz şifre sıfırmala sorusu"); ;
                case MembershipCreateStatus.InvalidUserName:
                    throw new BusinessException("Geçersiz kullanıcı adı");
                case MembershipCreateStatus.ProviderError:
                    throw new BusinessException("Bilinmeyen teknik hata"); ;
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.UserRejected:
                    throw new BusinessException("Kullanıcı reddedildi"); ;
                default:
                    break;
            }
            var riskUser = RetreiveByUsername(username);
            
            riskUser.BirimID = birimId;
            riskUser.Ad = ad;
            riskUser.Soyad = soyad;
            riskUser.UnvanID = unvanId;
            UpdateSingle(riskUser);
            updateUserRoles(riskUser.UserName, roles);
            if (unlockUser)
            {
                UnLockUser(riskUser);
                new RiskBusiness().ValidateUsersRiskStatus(riskUser.UserId);
            }
        }

        private void updateUserRoles(string username, string[] roles)
        {
            var currentRoles = Roles.GetRolesForUser(username);
            foreach (var role in currentRoles)
            {
                Roles.RemoveUserFromRole(username, role);
            }
            if (roles.Length > 0)
                Roles.AddUserToRoles(username, roles);
        }

        public void UpdateUser(string username, string email, string ad, string soyad, bool enabled, int birimId, int unvanId, string[] roles)
        {
            var user = Membership.GetUser(username);
            user.Email = email;
            user.IsApproved = enabled;
            Membership.UpdateUser(user);
            var riskUser = RetreiveByUsername(user.UserName);
            if (riskUser.BirimID != birimId)
                new CalismaGrupKullaniciBusiness().RemoveUserFromGroup(riskUser);
            riskUser.BirimID = birimId;
            riskUser.Ad = ad;
            riskUser.Soyad = soyad;
            riskUser.UnvanID = unvanId;
            UpdateSingle(riskUser);
            updateUserRoles(riskUser.UserName, roles);
            
        }

        public static bool IsValidTCIdentityNumber(long TCIdentityNumber)
        {
            if (TCIdentityNumber > 99999999999 || TCIdentityNumber < 10000000000)
                return false;

            long Tmp1, Tmp;
            Tmp = TCIdentityNumber / 100;
            Tmp1 = TCIdentityNumber / 100;

            int odd_sum, even_sum, total, ChkDigit2, ChkDigit1;
            int[] NumArray = new int[9];

            for (int i = 8; i >= 0; i--)
            {
                NumArray[i] = (int)(Tmp1 % 10);
                Tmp1 = Tmp1 / 10;
            }

            odd_sum = NumArray[8] + NumArray[6] + NumArray[4] + NumArray[2] + NumArray[0];
            even_sum = NumArray[7] + NumArray[5] + NumArray[3] + NumArray[1];
            total = odd_sum * 3 + even_sum;
            ChkDigit1 = (10 - (total % 10)) % 10;

            odd_sum = ChkDigit1 + NumArray[7] + NumArray[5] + NumArray[3] + NumArray[1];
            even_sum = NumArray[8] + NumArray[6] + NumArray[4] + NumArray[2] + NumArray[0];
            total = odd_sum * 3 + even_sum;
            ChkDigit2 = (10 - (total % 10)) % 10;

            Tmp = Tmp * 100 + ChkDigit1 * 10 + ChkDigit2;
            if (Tmp != TCIdentityNumber)
            {
                return false;
            }
            return true;
        }

        public string[] GetUserRoles(string userName)
        {
            return Roles.GetRolesForUser(userName);
        }

        public static bool UserHasGlobalRights()
        {
            foreach (var key in RoleConstants.GlobalRoles)
            {
                if (Thread.CurrentPrincipal.IsInRole(key))
                    return true;
            }
            return false;
        }

        public static bool UserHasGlobalRights(string userName)
        {
            foreach (var key in RoleConstants.GlobalRoles)
            {
                if (Roles.IsUserInRole(key))
                    return true;
            }
            return false;
        }

        public string[] GetRoles()
        {
            if (UserHasGlobalRights())
                return Roles.GetAllRoles();
            else return RoleConstants.UnitRoles.ToArray();
        }

        public string ResetPassword(aspnet_Users user)
        {
            return Membership.GetUser(user.UserName).ResetPassword();
        }
    }
}
