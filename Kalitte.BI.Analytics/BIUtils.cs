using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.BI.Shared;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using Kalitte.BI.Shared.ServiceManagement;
using RadarSoft.WinForms.Desktop;
using System.Threading;
using System.Windows.Forms;
using Kalitte.BI.Analytics.UserInterface;
using RadarSoft.WinForms.Grid;
using RadarSoft.WinForms.GridChart;


namespace Kalitte.BI.Analytics
{
    public static class BIUtils
    {

        public enum FormType
        {
            PersonelAnaliz, PersonelIzinAnaliz, PersonelMaasAnaliz, PersonelSicilAnaliz
        }

        public static Kalitte.BI.Shared.FilterSelectData GetFilterData(Kalitte.BI.Shared.AnalyticsService.FilterSelectData source)
        {            
            Kalitte.BI.Shared.FilterSelectData result = new FilterSelectData();
            result.Username = source.Username;
            result.Filters = source.Filters;
            result.ID = source.ID;
            result.IDList = source.IDList;
            result.Password = source.Password;
            result.SelectType = (DataSelectType)Enum.Parse(typeof(DataSelectType), source.SelectType.ToString()); ;
            result.Key = source.Key;
            result.FilterType = source.FilterType;
            return result;

        }

        public static string GetFilterHtml(Kalitte.BI.Shared.AnalyticsService.FilterSelectData data,string relativeUrl)
        {
            string html;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Kalitte.BI.Analytics.Resources.postback.htm"))
            {
                StreamReader reader = new StreamReader(stream);
                html = reader.ReadToEnd();
                stream.Close();
            }
            BinaryFormatter fmt = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            fmt.Serialize(ms, GetFilterData(data));
            byte[] fAsByte = ms.ToArray();


            html = html.Replace("#FILTERDATA#", Convert.ToBase64String(fAsByte));
            html = html.Replace("#URL#", ServerServices.Params.PortalUrl + relativeUrl);
            return html;
        }

        public static string GetFilterHtmlFile(Kalitte.BI.Shared.AnalyticsService.FilterSelectData data, string relativeUrl)
        {
            string fileName = Path.GetTempFileName() + ".htm";
            File.WriteAllText(fileName, GetFilterHtml(data, relativeUrl));
            return fileName;
        }

        public static void GoFilter(Kalitte.BI.Shared.AnalyticsService.FilterSelectData data, string relativeUrl)
        {
            string fileName = GetFilterHtmlFile(data, relativeUrl);
            Process.Start(fileName);
        }

        public static void SaveOlapCubeData(object OLAPObject, string reportName,FormType type,bool saveAsTemplate)
        {
            Kalitte.BI.Shared.AnalyticsService.AnalyticsServiceClient client = new Shared.AnalyticsService.AnalyticsServiceClient();
            client.ClientCredentials.UserName.UserName = ServerServices.UserName;
            client.ClientCredentials.UserName.Password = ServerServices.Password;
            MemoryStream ms = new MemoryStream();
            string reportElement;
            if (OLAPObject is TOLAPGrid)
            {                
                ((TOLAPGrid)OLAPObject).SaveCompressed(ms, RadarSoft.Common.TStreamContent.GridState);
                reportElement = "Grid";
            }
            else
            {
                ((TOLAPChart)OLAPObject).SaveCompressed(ms, RadarSoft.Common.TStreamContent.GridState);
                reportElement = "Grafik";
            }            
            byte[] data = ms.ToArray();
            client.SaveUserReport(ServerServices.UserName, reportName, type.ToString("g"), data, reportElement,saveAsTemplate);
        }

        public static MemoryStream ShowSavedReports(string formtype,string repelement)
        {
            Kalitte.BI.Shared.AnalyticsService.AnalyticsServiceClient client = new Shared.AnalyticsService.AnalyticsServiceClient();
            client.ClientCredentials.UserName.UserName = ServerServices.UserName;
            client.ClientCredentials.UserName.Password = ServerServices.Password;
            string username = ServerServices.UserName;
            string reportelement = repelement;
            Shared.AnalyticsService.UserReport[] data = client.GetUserReports(username, formtype, reportelement);
            if (data.Length == 0)
            {
                MessageBox.Show("Kayıtlı rapor bulunamadı");
                return null;
            }
            else
            {
                UserReportsForm form = new UserReportsForm();
                return form.Execute(data);
            }
        }

        public static MemoryStream GetReportById(int id)
        {
            try
            {
                Kalitte.BI.Shared.AnalyticsService.AnalyticsServiceClient client = new Shared.AnalyticsService.AnalyticsServiceClient();
                client.ClientCredentials.UserName.UserName = ServerServices.UserName;
                client.ClientCredentials.UserName.Password = ServerServices.Password;
                byte[] data = client.GetReportDataByID(id);
                MemoryStream ms = new MemoryStream(data);
                return ms;
            }
            catch
            {
                return null;
            }

        }

        public static bool DeleteReportById(int id)
        {
            try
            {
                Kalitte.BI.Shared.AnalyticsService.AnalyticsServiceClient client = new Shared.AnalyticsService.AnalyticsServiceClient();
                client.ClientCredentials.UserName.UserName = ServerServices.UserName;
                client.ClientCredentials.UserName.Password = ServerServices.Password;
                client.DeleteReportDataByID(id);
                return true;
                        
            }
            catch
            {
                return false;
            }

        }
    }
}
