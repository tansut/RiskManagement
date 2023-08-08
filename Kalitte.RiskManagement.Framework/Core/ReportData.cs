using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model.Common;

namespace Kalitte.RiskManagement.Framework.Core
{
    public class ReportData
    {
        public RiskReportParameterCollection ReportParameters { get; set; }

        public RiskReportDataSourceCollection Datasources { get; set; }

        public RiskReportDataSource AddDataSource(string name, object value)
        {
            if (Datasources == null)
                Datasources = new RiskReportDataSourceCollection();
            var source = new RiskReportDataSource() { Name = name, Value = value };
            Datasources.Add(source);
            return source;
        }

        public RiskReportParameter AddParameter(string name, string value)
        {
            if (ReportParameters == null)
                ReportParameters = new RiskReportParameterCollection();
            var para = new RiskReportParameter() { Name = name, Value = value };
            ReportParameters.Add(para);
            return para;
        }
    }
}
