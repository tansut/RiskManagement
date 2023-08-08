using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.IO;
using System.IO.Compression;

namespace Kalitte.BI.Shared
{
    public static class BIDBUtil
    {
        public static DataSet GetFilterData(FilterSelectData filter)
        {
            DataSet set = new DataSet();

            DataTable table = new DataTable("Ids");
            table.Columns.Add("Id", typeof(int));
            //table.Columns.Add("PersonelNK", typeof(string));
            //table.Columns.Add("Ad", typeof(string));
            //table.Columns.Add("Soyad", typeof(string));

            foreach (DataColumn col in table.Columns)
                col.ColumnMapping = MappingType.Attribute;

            if (filter.IDList != null)
                foreach (var id in filter.IDList)
                    table.Rows.Add(id);
            set.Tables.Add(table);
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["OlapConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetDrillThroughData", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@in_values", SqlDbType.NText);
            cmd.Parameters.Add("@table_name", SqlDbType.NVarChar);

            cmd.Parameters[0].Value = set.GetXml();
            cmd.Parameters[1].Value = filter.FilterType;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataSet resultSet = new DataSet();
            resultSet.Tables.Add("Drill");

            adapter.Fill(resultSet.Tables[0]);
            //TODO: filter.IDList ile resultSet

            return resultSet;
        }

        public static string CompressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        public static string DecompressString(string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }
    }
}
