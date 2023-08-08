using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Management;

namespace Kalitte.RiskManagement.Web.Pages.Surec
{
    public partial class UserInfo : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Bind(int UserId)
        {
            var entity = new UserBusiness().Retrieve(UserId);
            var user = Membership.GetUser(entity.UserName);
            ctlUsername.ValueAsLong = entity.TCKimlikNo;
            ctlAd.Text = entity.Ad;
            ctlSoyad.Text = entity.Soyad;
            ctlEnabled.Checked = user.IsApproved;
            ctlEMail.Text = user.Email;
            ctlBirim.Text = entity.BirimAd;
            ctlUnvan.Text = entity.UnvanAd;
            entityWindow.Show();
        }

    }
}