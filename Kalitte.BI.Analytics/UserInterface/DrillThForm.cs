using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kalitte.BI.Shared;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Kalitte.BI.Shared.ServiceManagement;
using Kalitte.BI.Analytics.Utility;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class DrillThForm : Kalitte.BI.Analytics.UserInterface.BaseForm
    {
        public DrillThForm()
        {
            InitializeComponent();
        }
        private Kalitte.BI.Shared.AnalyticsService.FilterSelectData filterData;
        private int[] selectedList;

        private void DrillThForm_Load(object sender, EventArgs e)
        {

        }

        internal void Execute(Kalitte.BI.Shared.AnalyticsService.FilterSelectData data)
        {
            filterData = data;
            selectedList = data.IDList;

            DataTable set = ServerServices.AnalyticsService.GetFilterData(data).Tables[0];
            grid.DataSource = set;

            ShowDialog();
        }



        private void ctlShowDetails_Click(object sender, EventArgs e)
        {
            filterData.SelectType = Shared.AnalyticsService.DataSelectType.ID;
            filterData.ID = (int)grid.CurrentRow.Cells[0].Value;
            filterData.IDList = null;
            Helpers.OpenSpecifigFilterPage(filterData);
        }

        private void ctlShowAll_Click(object sender, EventArgs e)
        {

            filterData.IDList = selectedList;
            filterData.SelectType = Shared.AnalyticsService.DataSelectType.IDList;

            Helpers.OpenSpecifigFilterPage(filterData);

            //BIUtils.GoFilter(filterData);
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            ctlShowDetails_Click(sender, EventArgs.Empty);
        }

        internal void ShowWebRequestToolbar(bool AllowWebDrillThroughRequest)
        {
            toolStrip1.Visible = AllowWebDrillThroughRequest;
        }
    }
}
