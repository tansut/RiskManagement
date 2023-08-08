using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using System.Runtime.Serialization;

namespace Kalitte.BI.ServerPlatform.Entities
{
    [DataContract]
    public class SystemParameterInfo
    {
        [DataMember]
        public string PortalUrl { get; private set; }
        [DataMember]
        public string DecisionSupportBrowserWinUrl { get; private set; }

        public SystemParameterInfo(SystemParameter p)
        {
            this.PortalUrl = p.PortalUrl;
            this.DecisionSupportBrowserWinUrl = p.DecisionSupportBrowserWinUrl;
        }
    }
}
