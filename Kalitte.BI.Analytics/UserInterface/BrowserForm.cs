using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Kalitte.BI.Shared.ServiceManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Threading;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class BrowserForm : Kalitte.BI.Analytics.UserInterface.BaseForm
    {
        public BrowserForm()
        {
            InitializeComponent();
        }

        private void BrowserForm_Load(object sender, EventArgs e)
        {

        }

        public void Execute(string html)
        {
            string file = Path.GetTempFileName() + ".htm";
            File.WriteAllText(file, html);
            webBrowser1.Navigate(file);
            ShowDialog();
        }

        internal void Execute(Shared.AnalyticsService.FilterSelectData filterData)
        {
            string url = ServerServices.Params.PortalUrl + "/default.aspx?action=filterData";
            BinaryFormatter fmt = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            fmt.Serialize(ms, BIUtils.GetFilterData(filterData));

            byte[] fAsByte = ms.ToArray();

            string filter = Convert.ToBase64String(fAsByte);

            string poststring = "filterData=" + HttpUtility.UrlEncode(filter);

            return;
            byte[] postData = Encoding.UTF8.GetBytes(poststring);

            webBrowser1.Navigate(url, "", postData, "Content-Type: application/x-www-form-urlencoded\r\n");

            ShowDialog();

        }
    }
}
