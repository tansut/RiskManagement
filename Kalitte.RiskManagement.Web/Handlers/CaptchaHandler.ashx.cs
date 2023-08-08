using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kalitte.RiskManagement.Framework.Captcha;
using System.Drawing.Imaging;
using Kalitte.RiskManagement.Framework.Utility;

namespace Kalitte.RiskManagement.Web.Handlers
{
    /// <summary>
    /// Summary description for CaptchaHandler
    /// </summary>
    public class CaptchaHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            string captchaText = context.Request.QueryString["ct"].ToString();
            string unprotectedstr = SerializationHelper.UnProtect(captchaText);
            CaptchaImage ci = new CaptchaImage(unprotectedstr, 200, 50, "Century Schoolbook");
            ci.Image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            ci.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}