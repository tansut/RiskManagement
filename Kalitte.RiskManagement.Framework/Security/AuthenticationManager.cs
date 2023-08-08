using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Security.Cryptography;
using System.Threading;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Framework.Security
{
    public static class AuthenticationManager
    {
        public static void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("~/");
        }

        private static string getUserCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
                throw new BusinessException("Giriş yetkiniz tanımlanamadı. Lütfen sisteme yeniden giriş yapınız.");
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            string userKey = ticket.UserData;
            return userKey;
        }

        public static int [] UnitsUserHasPermission
        {
            get
            {
                string userKey = getUserCookie();
                var unitList = userKey.Split(';')[2].Split('|');

                HashSet<int> units = new HashSet<int>();
                units.Add(CurrentUserBirimID);
                
                foreach (var unit in unitList)
                {
                    if (!string.IsNullOrEmpty(unit))
                    {
                        int unitId = int.Parse(unit);
                        if (!units.Contains(unitId))
                            units.Add(unitId);
                    }
                }
                return units.ToArray();
            }
        }

        public static int CurrentUserBirimID
        {
            get
            {
                string userKey = getUserCookie();
                return int.Parse(userKey.Split(';')[1]);
            }
        }

        public static Guid CurrentUserID
        {
            get
            {
                var userKey = getUserCookie();
                return new Guid(userKey.Split(';')[0]);
            }
        }


        public static void Login(string userName, string password, bool remember)
        {
            if (Membership.ValidateUser(userName, password))
            {
                var riskUser = new UserBusiness() { PermissionMode = Business.EntityPermissonMode.All }.RetreiveByUsername(userName);
                HttpCookie cookie = FormsAuthentication.GetAuthCookie(userName, remember);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                FormsAuthenticationTicket newticket = new FormsAuthenticationTicket(ticket.Version, ticket.Name,
                    ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, string.Format("{0};{1};{2}",
                    Membership.GetUser(userName).ProviderUserKey.ToString(),
                    riskUser.BirimID.Value,
                    buildUserUnits(riskUser)));
                cookie.Value = FormsAuthentication.Encrypt(newticket);
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Response.Redirect("~/");
            }
            else
            {
                throw new BusinessException("Hatalı Kullanıcı Adı / Şifre.");
            }
        }

        private static string buildUserUnits(Kalitte.RiskManagement.Framework.Model.aspnet_Users user)
        {
            if (!UserBusiness.UserHasGlobalRights(user.UserName))
            {
                StringBuilder sb = new StringBuilder();
                var units = new UnitBusiness() { PermissionMode = Business.EntityPermissonMode.All }.GetSubUnits(user.BirimID.Value);
                foreach (var unit in units)
                {
                    sb.AppendFormat("{0}|", unit.ID);
                }
                if (sb.Length > 0)
                    sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
            else return string.Empty;
        }
    }
}
