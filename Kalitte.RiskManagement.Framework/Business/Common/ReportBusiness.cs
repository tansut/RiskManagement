using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Model;
using System.Collections;
using Kalitte.RiskManagement.Framework.Model.Common;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web;
using Kalitte.RiskManagement.Framework.Security;
using System.Threading;
using System.Web.Security;

namespace Kalitte.RiskManagement.Framework.Business.Common
{

    public enum ReportResponse
    {
        PDF,
        Excel,
        ReportViewer
    }



    public class ReportBusiness: EntityBusiness<RaporTanim>
    {
        public ReportBusiness() : base()
        {

        }

        private RaporTanim currentReport = null;

        public ReportBusiness(string reportKey): base()
        {
            currentReport = Retrieve(reportKey);
        }

        public static bool UserHasGlobalRights()
        {
            foreach (var key in RoleConstants.GlobalRoles)
            {
                if (Thread.CurrentPrincipal.IsInRole(key))
                    return true;
            }
            return false;
        }

        public string[] GetRoles()
        {
            if (UserHasGlobalRights())
                return Roles.GetAllRoles();
            else return RoleConstants.UnitRoles.ToArray();
        }

        public void updateReportRoles(int reportid, string[] rolename)
        {
            new ReportRoleBusiness().DeleteRolesfromReport(reportid);
            if (rolename.Length > 0)
                new ReportRoleBusiness().AddRolestoReport(reportid, rolename);
        }

        protected void AddDataSourceMultipleItemReport(LocalReport report, object source)
        {
            if (!(source is IList))
            {
                ArrayList tmp = new ArrayList();
                tmp.Add(source);
                ReportDataSource item =
                    new ReportDataSource(GetTypeString(source.GetType()), tmp);


                report.DataSources.Add(item);
            }
            else
            {
                ReportDataSource item =
                    new ReportDataSource(GetTypeString(source.GetType()), source);
                report.DataSources.Add(item);
            }
        }

        public void BindDataSources(LocalReport report, object[] dataSources, RiskReportParameterCollection parameters, Stream ReportStream)
        {
            try
            {
                report.DataSources.Clear();
                if (ReportStream == null)
                    report.ReportPath = currentReport.ReportPath;
                else
                    report.LoadReportDefinition(ReportStream);
                report.DataSources.Clear();

                if (dataSources != null)
                    for (int i = 0; i < dataSources.Length; i++)
                    {
                        if (dataSources[i] != null)
                        {
                            if (dataSources[i] is RiskReportDataSourceCollection)
                            {
                                RiskReportDataSourceCollection col = dataSources[i] as RiskReportDataSourceCollection;
                                foreach (RiskReportDataSource item in col)
                                {
                                    ReportDataSource ds = new ReportDataSource(item.Name, item.Value);
                                    report.DataSources.Add(ds);

                                }
                            }
                            else
                                AddDataSourceMultipleItemReport(report, dataSources[i]);
                        }
                    }

                if (parameters != null)
                {
                    ArrayList paramList = new ArrayList();
                    foreach (RiskReportParameter param in parameters)
                    {
                        paramList.Add(new ReportParameter(param.Name, param.Value));
                    }
                    report.SetParameters((ReportParameter[])paramList.ToArray(typeof(ReportParameter)));
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }


        protected string GetTypeString(Type type)
        {
            string typeStrWithDot = type.FullName;

            if (typeof(IList).IsAssignableFrom(type))
            {
                if (type.IsGenericType)
                {
                    typeStrWithDot = type.GetGenericArguments()[0].FullName;
                }
            }
            return typeStrWithDot.Replace(".", "_");
        }

        public virtual void ResponseReport(LocalReport localReport, ReportResponse responseType, object source, RiskReportParameterCollection parameters, Stream ReportStream)
        {
            localReport.EnableExternalImages = true;
            if (responseType == ReportResponse.Excel || responseType == ReportResponse.PDF)
            {
                BindDataSources(localReport, new object[] { source }, parameters, ReportStream);
                string mimeType = "";
                Warning[] warns = null;
                string fnExt = "";
                string[] streams = null;
                string encoding = "";
                byte[] res = null;

                localReport.Refresh();

                if (responseType == ReportResponse.PDF)
                    res = localReport.Render("PDF", "", out mimeType, out encoding, out fnExt, out streams, out warns);
                else if (responseType == ReportResponse.Excel)
                    res = localReport.Render("Excel", "", out mimeType, out encoding, out fnExt, out streams, out warns);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();

                HttpContext.Current.Response.Expires = 0;

                if (responseType == ReportResponse.PDF)
                {
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Report.pdf");
                }
                else if (responseType == ReportResponse.Excel)
                {
                    HttpContext.Current.Response.ContentType = "application/excel";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
                }

                HttpContext.Current.Response.BinaryWrite(res);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                HttpContext.Current.Response.Close();
            }
            else if (responseType == ReportResponse.ReportViewer)
            {
                //Viewer.LocalReport.DataSources.Clear();
                //Viewer.LocalReport.ReportPath = ReportPath;
                BindDataSources(localReport, new object[] { source }, parameters, ReportStream);
                //Viewer.Visible = true;
                localReport.Refresh();
            }

        }

        public RaporTanim Retrieve(string reportKey)
        {
            return GetQueryable().Single(p => p.ReportKey == reportKey);
        }
    }
}
