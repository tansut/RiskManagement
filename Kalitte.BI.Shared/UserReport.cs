using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.BI.Shared
{
    [Serializable]
    public class UserReport
    {
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportType { get; set; }
        public string CreationDate { get; set; }
        public string UserName { get; set; }

        public UserReport(int id, string repName, string repType, DateTime creationDate,string userName)
        {
            ReportId = id;
            ReportName = repName;
            ReportType = repType;
            CreationDate = creationDate.ToString("dd.MM.yyyy");
            UserName = userName;
        }

    }
}
