using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;

namespace Kalitte.BI.Shared
{
    [Serializable]
    public enum DataSelectType
    {
        ID,
        IDList
    }

    [Serializable]
    [DataContract]
    public class FilterSelectData
    {
        [DataMember]
        public DataSelectType SelectType { get; set; }
        [DataMember]
        public int[] IDList { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public Dictionary<string,string> Filters { get; set; }
        [DataMember]
        public Guid Key { get; set; }
        [DataMember]
        public string FilterType { get; set; }
        [DataMember]
        public string DestinationUrl { get; set; }

        public DataTable GetFiltersAsTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Alan", typeof(string));
            table.Columns.Add("Deger", typeof(string));

            foreach (var item in Filters)
                table.Rows.Add(item.Key, item.Value);

            return table;
        }
    }
}
