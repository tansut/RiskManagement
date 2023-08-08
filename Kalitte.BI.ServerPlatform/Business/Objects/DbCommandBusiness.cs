using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using System.Data;
using Kalitte.BI.ServerPlatform.Model;
using System.Web.Configuration;
using System.Configuration;
using System.Data.SqlClient;


namespace Kalitte.BI.ServerPlatform.Business.Objects
{
    class DbCommandBusiness : BaseBusiness
    {
        private const string SQLProviderName = "System.Data.SqlClient";

        private IDbConnection GetConnection(string conn)
        {
            ConnectionStringSettings connStr = WebConfigurationManager.ConnectionStrings[conn.Trim()];
            if (connStr == null)
                throw new ArgumentException("Bağlantı dizesi tanımı geçersizdir", "DbCommand.ConnectionString");
            IDbConnection result = null;
            switch (connStr.ProviderName)
            {
                case DbCommandBusiness.SQLProviderName:
                    {
                        result = new SqlConnection(connStr.ConnectionString);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Veri sağlayıcısı tanınmıyor", connStr.ProviderName);
                    }
            }

            return result;
        }

        public void FillDataSet(List<DbCommand> cmds, DataSet dataSet)
        {
            FillDataSet(cmds, dataSet, null);
        }
        
        public void FillDataSet(List<DbCommand> cmds, DataSet dataSet, ICommandParameterProvider parameterProvider)
        {
            foreach (DbCommand cmd in cmds)
            {
                dataSet.EnforceConstraints = false;
                DataTable table = dataSet.Tables.Add(cmd.Tablename);
                FillTable(cmd, table, parameterProvider);
            }
        }

        public void FillTable(DbCommand cmd, DataTable table) {
            FillTable(cmd, table, null);
        }

        public void FillTable(DbCommand cmd, DataTable table, ICommandParameterProvider parameterProvider)
        {
            IDbConnection conn = GetConnection(cmd.ConnectionString);
            IDbCommand dbCmd = GetCommand(conn);

            dbCmd.CommandText = cmd.CommandName;
            dbCmd.CommandType = (CommandType)cmd.CommandType;
            if (parameterProvider != null)
                parameterProvider.SetParameters(cmd, dbCmd);
            conn.Open();
            using (conn)
            {
                FillTable(table, dbCmd);                
                conn.Close();
            }

        }

        private IDbDataAdapter FillTable(DataTable table, IDbCommand cmd)
        {
            if (cmd is SqlCommand)
            {
                SqlDataAdapter adapter =new SqlDataAdapter((SqlCommand)cmd);
                table.BeginLoadData();
                adapter.Fill(table);
                table.EndLoadData();
                return adapter;
            }
            else return null;


        }

        private IDbCommand GetCommand(IDbConnection conn)
        {
            IDbCommand cmd = conn.CreateCommand();
            return cmd;
        }

    }
}
