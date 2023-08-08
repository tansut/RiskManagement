using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using Kalitte.BI.ServerPlatform.Business.RepositoryManagement;

namespace Kalitte.BI.ServerPlatform.Business.Objects
{
    public class ModuleBusiness : BaseBusiness
    {
        public DataSet GetModuleData(object moduleId)
        {

            Module module = RepositoryContext.Modules.Single(c => c.ID == (Guid)moduleId);
            DbCommandBusiness cmdbll = new DbCommandBusiness();
            List<DbCommand> cmdList = (from p in module.ModuleDetails select p.DbCommand).ToList();
            DataSet result = new DataSet();
            cmdbll.FillDataSet(cmdList, result);

            return result;

        }
    }
}
