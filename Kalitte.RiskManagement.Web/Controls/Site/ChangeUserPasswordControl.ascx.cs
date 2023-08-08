﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Ext.Net;
using System.Threading;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Utility;


namespace Kalitte.RiskManagement.Web.Controls.Site
{
    public partial class ChangeUserPasswordControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static ChangeUserPasswordControl LoadInto(Page page)
        {
            ChangeUserPasswordControl window = (ChangeUserPasswordControl)page.LoadControl("~/Controls/Site/ChangeUserPasswordControl.ascx");
            window.ID = "changePwdControl";
            return window;
        }

        protected void btnChangePassword_Click(object sender, DirectEventArgs e)
        {
            MembershipUser user = Membership.GetUser(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                if (user.ChangePassword(txtOldPassword.Text, txtNewPassword.Text))
                {
                    
                    WebHelper.ShowMessage("Şifreniz başarıyla değiştirildi.", MessageType.Info);
                    ChangePasswordWindow.Hide();                    

                }
                else
                {
                    throw new BusinessException("Şifre değiştirme gerçekleştirilemedi.");
                }
            }
        }
    }
}