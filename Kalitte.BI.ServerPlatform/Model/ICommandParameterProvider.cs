using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;

namespace Kalitte.BI.ServerPlatform.Model
{
    public interface ICommandParameterProvider
    {
        void SetParameters(DbCommand cmdData, IDbCommand cmd);
    }
}
