using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Kalitte.BI.ServerPlatform.Entities;


namespace Kalitte.BI.ServerPlatform.Services
{
    [ServiceContract]
    public interface IParametersService
    {
        [OperationContract]
        SystemParameterInfo GetParameters();
    }
}
