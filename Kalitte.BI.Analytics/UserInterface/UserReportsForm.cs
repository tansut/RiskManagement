using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kalitte.BI.Shared.AnalyticsService;
using RadarSoft.WinForms.Desktop;
using Kalitte.BI.Analytics.RiskAnalizModule;
using System.IO;
using Kalitte.BI.Shared.ServiceManagement;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class UserReportsForm : Form
    {

        List<UserReport> Data;
        public MemoryStream StreamData;
        public UserReportsForm()
        {
            InitializeComponent();
        }

        public MemoryStream Execute(UserReport[] data)
        {
            Data = new List<UserReport>(data);
            ctlDataGrid.AutoGenerateColumns = false;
            ctlDataGrid.DataSource = Data;
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK) return StreamData; else return null;
        }

        private void ctlClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ctlOpenSelectedReport_Click(object sender, EventArgs e)
        {
            if (ctlDataGrid.SelectedRows.Count > 0)
            {
                int id = (int)ctlDataGrid.SelectedRows[0].Cells["ctlId"].Value;
                MemoryStream ms = BIUtils.GetReportById(id);
                StreamData = ms;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void ctlDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ctlOpenSelectedReport_Click(sender, new EventArgs());
        }

        private void ctlDeleteReport_Click(object sender, EventArgs e)
        {
            if (ctlDataGrid.SelectedRows.Count > 0)
            {
                int id = (int)ctlDataGrid.SelectedRows[0].Cells["ctlId"].Value;
                UserReport rpt = Data.Single(p => p.ReportIdk__BackingField == id);
                if (rpt.UserNamek__BackingField == ServerServices.UserName)
                {
                    if (BIUtils.DeleteReportById(id))
                    {
                        Data.Remove(Data.Single(p => p.ReportIdk__BackingField == id));
                        ctlDataGrid.DataSource = Data.ToArray();
                    }
                    else
                    {
                        MessageBox.Show("Hata Oluştu");
                    }
                }
                else
                {
                    MessageBox.Show("Seçilen rapor genel bir rapordur.\nSadece raporu kaydeden kullanıcı tarafından silinebilir");
                }
            }
        }
    }
}
