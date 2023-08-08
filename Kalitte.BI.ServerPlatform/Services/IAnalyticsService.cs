using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using Kalitte.BI.Shared;
using Kalitte.BI.ServerPlatform.DataAccess.Repository;
using Kalitte.BI.ServerPlatform.Entities;


namespace Kalitte.BI.ServerPlatform.Services
{
    [ServiceContract]
    public interface IAnalyticsService
    {
        [OperationContract]
        List<ModuleInfo> GetModules();

        [OperationContract]
        byte [] GetModuleDataAsArray(object moduleId);

        [OperationContract]
        DataSet GetModuleData(object moduleId);

        [OperationContract]
        void ValidateUsernamePassword(string userName, string password);

        [OperationContract]
        List<DashboardInfo> GetUserDashboards();

        [OperationContract]
        DataSet GetFilterData(FilterSelectData filter);

        [OperationContract]
        void SaveUserReport(string userName, string reportName, string reportype, byte[] reportData, string reportElement,bool saveAsTemplate);

        [OperationContract]
        List<UserReport> GetUserReports(string userName, string reportType, string reportElement);

        [OperationContract]
        byte[] GetReportDataByID(int reportid);

        [OperationContract]
        void DeleteReportDataByID(int reportid);

        //[OperationContract]
        //void CreateNewWidget(object DashboardID, ContentType contentType, string title, byte [] contents);

    }


}
