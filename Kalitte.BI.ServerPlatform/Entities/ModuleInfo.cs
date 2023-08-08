using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;

namespace Kalitte.BI.ServerPlatform.Entities
{
    [DataContract]
    public class ModuleInfo
    {
        [DataMember]
        public object ID { get; set; }
        [DataMember]
        public string ModuleName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ActivatorInfo { get; set; }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public int ImageIndex { get; set; }


        public ModuleInfo(Module module)
        {
            this.ID = module.ID;
            this.Description = module.Description;
            this.ModuleName = module.ModuleName;
            this.ActivatorInfo = module.ActivatorInfo;
            this.GroupName = module.GroupName;
            this.ImageIndex = module.ImageIndex;
        }
    }
}
