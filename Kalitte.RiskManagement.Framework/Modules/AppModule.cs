using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.Business;
using System.Net;
using System.Globalization;
using System.Threading;

namespace Kalitte.RiskManagement.Framework.Modules
{
    public class AppModule : IHttpModule
    {

        public AppModule() { }


        public void Init(System.Web.HttpApplication context)
        {
            context.Error += new EventHandler(context_Error);
            context.BeginRequest += new EventHandler(context_BeginRequest);

        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            SetUserLocale();
        }

        public static void SetUserLocale()
        {
            //HttpRequest Request = HttpContext.Current.Request;
            //if (Request.UserLanguages == null)
            //    return;

            //string Lang = Request.UserLanguages[0];
            string Lang = "tr-TR";
            if (Lang != null)
            {
                if (Lang.Length < 3)
                    Lang = Lang + "-" + Lang.ToUpper();
                try
                {
                    CultureInfo culture = new CultureInfo(Lang);
                    System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }
                catch
                { ;}
            }
        }

        void context_Error(object sender, EventArgs e)
        {

            HttpApplication Application = (HttpApplication)sender;
            HttpResponse Response = Application.Context.Response;
            HttpRequest Request = Application.Context.Request;
            Exception exc = Application.Server.GetLastError();
            if (exc == null)
                return;
            Exception convertedExc;
            if (!((exc is HttpException && ((System.Web.HttpException)(exc)).GetHttpCode() == 404)))
            {
                try
                {
                    convertedExc = ExceptionManager.Manage(exc);
                }
                catch (Exception cExc)
                {
                    convertedExc = cExc;
                }

                if (X.IsAjaxRequest)
                {
                    Application.Server.ClearError();
                    var response = new Ext.Net.Response(false);
                    response.Message = convertedExc.Message;
                    Response.ClearContent();
                    Response.Write(new Ext.Net.ClientConfig().Serialize(response));
                    Response.End();
                }
                else
                {
                    if (exc is HttpException)
                    {
                        Application.Context.Items.Add("lastException", convertedExc);
                        Application.Server.Transfer("~/Pages/Shared/UnhandledError.aspx");
                    }
                    else
                    {
                        Response.Write("<h2>Global Page Error</h2>\n");
                        Response.Write(
                            "<p>" + exc.Message + "</p>\n");
                        Response.Write("Return to the <a href='Default.aspx'>" +
                            "Default Page</a>\n");
                    }

                }
            }
        }


        public void Dispose() { }
    }
}
