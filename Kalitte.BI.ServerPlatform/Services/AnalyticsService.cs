using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Kalitte.BI.ServerPlatform.Business.RepositoryManagement;
using System.Data;
using Kalitte.BI.ServerPlatform.Business.Objects;
using Kalitte.BI.ServerPlatform.Security;
using Kalitte.BI.Shared.Exceptions;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Web.Security;
using Kalitte.BI.Shared;
using System.Globalization;
using Kalitte.BI.ServerPlatform.Entities;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace Kalitte.BI.ServerPlatform.Services
{
    [ServiceBehavior(Namespace = "http://kalitte.biServer", InstanceContextMode = InstanceContextMode.PerCall,
    IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AnalyticsService : IAnalyticsService
    {

    

        public byte[] GetModuleDataAsArray(object moduleId)
        {
            ModuleBusiness bll = new ModuleBusiness();
            DataSet data = bll.GetModuleData(moduleId);
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            f.Serialize(ms, data);
            return ms.ToArray();
        }

        public DataSet GetModuleData(object moduleId)
        {
            ModuleBusiness bll = new ModuleBusiness();
            DataSet data = bll.GetModuleData(moduleId);
            data.RemotingFormat = SerializationFormat.Binary;
            return data;
        }

        public DataSet GetFilterData(FilterSelectData filter)
        {
            DataSet set = BIDBUtil.GetFilterData(filter);
            set.RemotingFormat = SerializationFormat.Binary;
            return set;
        }


        public void ValidateUsernamePassword(string userName, string password)
        {
            if (!SecurityManager.ValidateUser(userName, password))
                throw new BusinessException("Geçersiz kullanıcı adı / şifre kombinasyonu");
            SecurityManager.LoginUser(userName, password);
            if (!Thread.CurrentPrincipal.IsInRole("Birim Süreç Sorumlusu") || !Thread.CurrentPrincipal.IsInRole("Global Süreç Sorumlusu"))
                throw new BusinessException("Yetkisiz giriş");
        }



        //public void CreateNewWidget(object DashboardID, Kalitte.BI.ServerPlatform.Web.Controls.ContentType contentType, string title, byte[] contents)
        //{
        //    new RepositoryBusiness().CreateContentWidget(DashboardID, contentType, title, contents);
        //}




        public void SaveUserReport(string userName, string reportName, string reportType, byte[] reportData, string reportElement, bool saveAsTemplate)
        {
            ReportBussiness rpt = new ReportBussiness();
            UserReportsBI exrec = rpt.RepositoryContext.UserReportsBIs.SingleOrDefault(p => p.UserName == userName && p.ReportName == reportName && p.ReportType == reportType && p.ReportElement == reportElement);
            if (exrec == null)
            {
                UserReportsBI rec = new UserReportsBI();
                rec.UserName = userName;
                rec.ReportData = reportData;
                rec.ReportName = reportName;
                rec.ReportType = reportType;
                rec.ReportElement = reportElement;
                rec.CreationDate = DateTime.Now;
                rec.Template = saveAsTemplate;
                rpt.RepositoryContext.UserReportsBIs.InsertOnSubmit(rec);
            }
            else
            {
                exrec.ReportData = reportData;
                exrec.Template = saveAsTemplate;
            }
            rpt.RepositoryContext.SubmitChanges();
        }

        public List<UserReport> GetUserReports(string userName, string reportType,string reportElement)
        {

            ReportBussiness rpt = new ReportBussiness();                        
            return      rpt.RepositoryContext.UserReportsBIs.Where(u => (u.UserName == userName || u.Template == true) && u.ReportType == reportType && u.ReportElement == reportElement).OrderBy(p => p.CreationDate).Select(z => new UserReport(z.Id, z.ReportName, z.ReportType, z.CreationDate,z.UserName)).ToList();
        }


        public byte[] GetReportDataByID(int reportid)
        {
            ReportBussiness rpt = new ReportBussiness();
            return rpt.RepositoryContext.UserReportsBIs.Where(p => p.Id == reportid).Single().ReportData.ToArray();
        }

        public void DeleteReportDataByID(int reportid)
        {
            ReportBussiness rpt = new ReportBussiness();
            rpt.RepositoryContext.UserReportsBIs.DeleteAllOnSubmit(rpt.RepositoryContext.UserReportsBIs.Where(p => p.Id == reportid));
            rpt.RepositoryContext.SubmitChanges();
        }


        #region IAnalyicsService Members

        List<Entities.ModuleInfo> IAnalyticsService.GetModules()
        {
            return (from p in RepositoryManager.GetModules() select new ModuleInfo(p)).ToList();
        }

        List<Entities.DashboardInfo> IAnalyticsService.GetUserDashboards()
        {
            return new List<DashboardInfo>();
        }

        #endregion
    }

    [ServiceBehavior(Namespace = "http://kalitte.biServer", InstanceContextMode = InstanceContextMode.PerCall,
IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ParametersService : IParametersService
    {
        public SystemParameterInfo GetParameters()
        {
            SystemParameter parameter =  (from p in (new RepositoryBusiness().RepositoryContext.SystemParameters) select p).First();

            return new SystemParameterInfo(parameter);
        }

        
    }
}
