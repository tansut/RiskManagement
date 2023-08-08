using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.ServiceModel.Security;
using Kalitte.BI.Shared.ServiceManagement;
using System.Diagnostics;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class FmLogin : Kalitte.BI.Analytics.UserInterface.BaseForm
    {
        internal static string Username { get; private set; }
        internal static string Password { get; private set; }

        public FmLogin()
        {
            InitializeComponent();
        }

        private void FmLogin_Load(object sender, EventArgs e)
        {
#if DEBUG
            ctlUsername.Text = "10384104416";
            ctlPassword.Text = "10384104416";
#endif
        }

        internal static bool Execute()
        {
            FmLogin f = new FmLogin();
            return f.ShowDialog() == DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerServices.Reset();
            ServerServices.UserName = ctlUsername.Text;
            ServerServices.Password = ctlPassword.Text;


            if (DialogResult == DialogResult.Cancel)
                return;
            try
            {
                ServerServices.AnalyticsService.ValidateUsernamePassword(ServerServices.UserName, ServerServices.Password);
            }
            catch (MessageSecurityException ex)
            {
                MessageBox.Show("Geçersiz kullanıcı adı / şifresi veya güvenlik ihlali durumu oluştu" + ex.Message);
                return;
            }


            DialogResult = DialogResult.OK;
        }

        private void ctlCancel_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void ctlUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ctlUsername.Text.Trim()))
                e.Cancel = true;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ServerServices.Params.PortalUrl + "/default.aspx?action=newUser");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(ServerServices.Params.PortalUrl);
        }

        private void ctlForgatPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ServerServices.Params.PortalUrl + "/default.aspx?action=resetPassword");
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("http://analitics.kalitte.com.tr");
        }
    }
}
