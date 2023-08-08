using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Web
{
    public class Global : System.Web.HttpApplication
    {
        Task riskHistoryTask;
        bool appEnding = false;
        object lockObk = new object();

        protected void Application_Start(object sender, EventArgs e)
        {
            riskHistoryTask = Task.Factory.StartNew(() =>
            {
                DateTime lastOperationDate = DateTime.MinValue;
                while (!isAppEnding())
                {
                    var now = DateTime.Now;
                    if (now.Hour == 23 && lastOperationDate.Date != now.Date)
                    {
                        try
                        {
                            new BirimAylikRiskSkorBusiness().CreateBirimAylikSkor();
                            lastOperationDate = now;
                        }
                        catch (Exception)
                        {
                            // throw;
                        }
                        if (!isAppEnding())
                            Thread.Sleep(30000);
                    }
                }
            });
        }

        private bool isAppEnding()
        {
            lock (lockObk)
            {
                return appEnding;
            }
        }

        private void setAppEnding()
        {
            lock (lockObk)
            {
                appEnding = true;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            setAppEnding();
            Debug.WriteLine("Waiting for history thread ...");
            riskHistoryTask.Wait();
        }
    }
}