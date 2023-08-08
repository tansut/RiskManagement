using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.Shared.AnalyticsService;
using Kalitte.BI.Shared.ParametersService;

namespace Kalitte.BI.Shared.ServiceManagement
{
    public static class ServerServices
    {
        private static AnalyticsServiceClient client = null;
        private static ParametersServiceClient parametersClient = null;
        private static SystemParameterInfo _params = null;

        public static string UserName { get; set; }
        public static string Password { get; set; }

        static ServerServices()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
            {
                return true;
            };
        }


        public static AnalyticsServiceClient AnalyticsService
        {
            get
            {
                client = new AnalyticsServiceClient();


                client.ClientCredentials.UserName.UserName = UserName;
                client.ClientCredentials.UserName.Password = Password;



                return client;
            }
        }

        static void InnerChannel_Closing(object sender, EventArgs e)
        {

        }

        static void InnerChannel_Opening(object sender, EventArgs e)
        {
        }

        public static ParametersServiceClient ParametersService
        {
            get
            {
                parametersClient = new ParametersServiceClient();

                return parametersClient;
            }
        }

        public static SystemParameterInfo Params
        {
            get
            {
                if (_params == null)
                    _params = ParametersService.GetParameters();

                return _params;

            }
        }


        public static void Reset()
        {
            client = null;
            parametersClient = null;
        }

    }
}



