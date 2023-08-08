using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;

namespace Kalitte.BI.ServerPlatform.Entities
{
    [DataContract]
    public class DashboardInfo
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public object ID { get; set; }

        public DashboardInfo(DashboardInstance instance)
        {
            Title = instance.Title;
            ID = instance.ID;
        }
    }
}
