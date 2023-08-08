using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using System.Data;

namespace Kalitte.BI.ServerPlatform.Business.Objects
{
    public class ChartBusiness : BaseBusiness
    {
        //public DataSet GetChartData(object chartId)
        //{
        //    Chart chart = RepositoryContext.Charts.Single(c => c.ID == (Guid)chartId);
        //    DbCommandBusiness cmdbll = new DbCommandBusiness();
        //    List<DbCommand> cmdList = (from p in chart.ChartSeries select p.DbCommand).ToList();
        //    DataSet result = new DataSet();
        //    cmdbll.FillDataSet(cmdList, result);
        //    return result;
        //}
    }
}
