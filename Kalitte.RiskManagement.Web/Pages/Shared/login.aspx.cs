using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business;
using Ext.Net;
using System.Configuration;
using System.Net;
using Kalitte.RiskManagement.Framework.Captcha;
using Kalitte.RiskManagement.Framework.Utility;

namespace Kalitte.RiskManagement.Web.Pages.Shared
{
    public partial class login : System.Web.UI.Page
    {


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ClientScript.RegisterStartupScript(this.GetType(), "CheckIFrame", "if (window != window.top)window.parent.location.href = window.location.href;", true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Ext.Net.X.IsAjaxRequest)
            {
                //ctlLoginHelp.Visible = ConfigurationManager.AppSettings["showInitialLoginHelp"] == "true";
            }
            if (!Page.IsPostBack)
            {
                SetCaptchaToSesionAndImage();
            }
        }

        public void SetCaptchaToSesionAndImage()
        {
            string code = CaptchaHelper.GenerateRandomCode();
            string protectedstr = SerializationHelper.Protect(code);
            this.Session["CaptchaImageText"] = code;
            ctlCaptchaImage.ImageUrl = string.Format("{0}?ct={1}", Page.ResolveClientUrl("~/Handlers/CaptchaHandler.ashx"), HttpUtility.UrlEncode(protectedstr));


        }

        internal void ShowErrorMessage()
        {
            ctlErrorLabel.Style.Value = "color: #FF0000";
            ctlErrorLabel.Text = "Hatalı Giriş! Resimdeki onay kodunu tekrar giriniz.";
            SetCaptchaToSesionAndImage();
        }

        private bool ControlCaptcha()
        {
            bool b = false;
            string captchaText = this.Session["CaptchaImageText"].ToString();
            string userCaptchaCode = ctlCaptchaCode.Text;
            if (captchaText != userCaptchaCode)
            {
                ctlErrorLabel.Style.Value = "color: #FF0000";
                ctlErrorLabel.Text = "Hata! Resimdeki onay kodunu yanlış girdiniz.";
                SetCaptchaToSesionAndImage();
            }
            else
            {
                ctlErrorLabel.Text = "Eşleşme Sağlandı";
                ctlErrorLabel.Style.Value = "color: #3BC647";
                b = true;
            }
            return b;
        }

        protected void ctlLogin_Click(object sender, DirectEventArgs e)
        {

            if (!ControlCaptcha())
                return;
            Kalitte.RiskManagement.Framework.Security.AuthenticationManager.Login(ctlUsername.Text, ctlPassword.Text, ctlRemember.Checked);
        }
    }
}