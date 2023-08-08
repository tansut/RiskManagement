using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kalitte.BI.Shared.AnalyticsService;
using Kalitte.BI.Shared.ServiceManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RadarSoft.WinForms;
using RadarSoft.WinForms.Desktop;

namespace Kalitte.BI.Analytics.RiskAnalizModule
{
    public partial class RiskAnalizForm : Kalitte.BI.Analytics.UserInterface.AnalyticsForm
    {
        public RiskAnalizForm()
        {
            InitializeComponent();
        }

        public RiskAnalizForm(ModuleInfo moduleInfo, IAnaliticsApp application)
            : base(moduleInfo, application)
        {
            InitializeComponent();

        }

        public override bool AllowWebDrillThroughRequest
        {
            get
            {
                return true;
            }
        }

        public override void FillData(DataSet dataset)
        {
            OLAPCube.DataSet = dataset;
        }

        protected override DataSet GetData()
        {
            return ServerServices.AnalyticsService.GetModuleData(Module.ID);
        }

        public override RadarSoft.WinForms.GridChart.TOLAPChart OLAPChart
        {
            get
            {
                return chart;
            }
        }

        protected override void CubeReady()
        {
            grid.BeginUpdate();
            TMeasure m = grid.Measures.FindByDisplayName("Toplam Risk Sayısı");
            m.Visible = true;

            THierarchy h = grid.Dimensions.FindHierarchyByDisplayName("Skor");
            grid.Pivoting(h, RadarSoft.Common.TLayoutArea.laRow, 0);

            //h = grid.Dimensions.FindHierarchyByDisplayName("Ayrılma Nedeni");
            //TMember tm = h.FindMemberByName("ÇALIŞAN PERSONEL");

            grid.EndUpdate();

        }

        protected override bool AllowDrillThrough
        {
            get
            {
                return true;
            }
        }


        public override RadarSoft.WinForms.Desktop.TOLAPCube OLAPCube
        {
            get
            {
                return ctlCube;
            }            
        }

        public override RadarSoft.WinForms.Grid.TOLAPGrid OLAPGrid
        {
            get
            {
                return grid;
            }
        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                if (tab.SelectedTab == tabGrid)
                    ActiveView = activeView.Grid;
                else ActiveView = activeView.Chart;
            }
        }

        private void PersonelAnalizForm_Load(object sender, EventArgs e)
        {

        }

        protected override int LookupRowIndex
        {
            get
            {
                return 4;
            }
        }

        public override bool SendUsernamePasswordWithFilterData
        {
            get
            {
                return false;
            }
        }

        private void chart_OnDrillthrough(object sender, DrillthroughActionArgs e)
        {
            DoDrillThrough(e.DrillthroughData.Tables[0]);
        }

        protected override void SetDrillthroughData(Kalitte.BI.Shared.AnalyticsService.FilterSelectData filter)
        {
            base.SetDrillthroughData(filter);
            filter.FilterType = "Risk";            
        }
    }
}
