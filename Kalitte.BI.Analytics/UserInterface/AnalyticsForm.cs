using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RadarSoft.WinForms.Desktop;
using RadarSoft.WinForms.Grid;
using Kalitte.BI.Shared.AnalyticsService;
using Kalitte.BI.Shared;
using RadarSoft.WinForms.GridChart;
using RadarSoft.WinForms;
using RadarSoft.Common;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Kalitte.BI.Shared.ServiceManagement;
using Kalitte.BI.Analytics.RiskAnalizModule;

namespace Kalitte.BI.Analytics.UserInterface
{
    public partial class AnalyticsForm : Kalitte.BI.Analytics.UserInterface.BaseForm
    {

        protected DataSet UnderlyingData;

        protected DataSet GetAsDataSet(byte[] buffer)
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(buffer);
            DataSet set = (DataSet)f.Deserialize(ms);
            return set;
        }

        protected virtual DataSet GetData()
        {
            return null;
        }

        public AnalyticsForm()
        {
            InitializeComponent();
        }

        public enum activeView
        {
            Grid,
            Chart
        }

        private activeView _activeView;

        public virtual bool AllowWebDrillThroughRequest
        {
            get
            {
                return false;
            }
        }

        protected virtual activeView ActiveView
        {
            set
            {
                //if (value == activeView.Grid)
                //    olapToolBox.Grid = OLAPGrid;
                //else olapToolBox.Grid = OLAPChart;
                _activeView = value;
            }
            get
            {
                return _activeView;
            }
        }

        protected ModuleInfo Module { get; set; }
        protected IAnaliticsApp AnaliticsApplication { get; set; }

        public AnalyticsForm(ModuleInfo moduleInfo, IAnaliticsApp application)
            : this()
        {
            this.Module = moduleInfo;
            this.AnaliticsApplication = application;
        }


        public virtual TOLAPCube OLAPCube
        {
            get
            {
                return null;
            }
        }
        public virtual TOLAPGrid OLAPGrid
        {
            get
            {
                return null;
            }
        }


        public virtual TOLAPChart OLAPChart
        {
            get
            {
                return null;
            }
        }

        public virtual void Execute()
        {
            if (OLAPCube != null && !DesignMode)
            {
                try
                {
                    FmWait.Execute();
                    if (!OLAPCube.Active)
                    {
                        if (UnderlyingData == null)
                        {
                            FmWait.Update("Sunucu ile haberleşme devam ediyor ...");
                            UnderlyingData = GetData();
                        }
                        FmWait.Update("Veriler konsolide ediliyor ...");
                        FillData(UnderlyingData);
                    }
                    FmWait.Update("Küpler açılıyor ...");
                    OLAPCube.Active = true;
                    CubeReady();
                }
                finally
                {
                    FmWait.Done();
                }
            }
            Show();
        }

        protected virtual void CubeReady()
        {

        }


        public virtual void FillData(DataSet dataset) { }

        protected virtual void NewInstance()
        {
            Type t = this.GetType();
            AnalyticsForm f = Activator.CreateInstance(t, Module, AnaliticsApplication) as AnalyticsForm;
            f.UnderlyingData = this.UnderlyingData;
            f.Execute();
        }

        private void AnalyticsForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                OLAPGrid.OLAPExport.OnGetXlsFormat += excelExport;
                OLAPGrid.OLAPExport.OnGetPreferredWidth += new TEventGetPreferredWidth(OLAPExport_OnGetPreferredWidth);
                OLAPGrid.OLAPExport.OnEventExportCell += new TEventExportCell(OLAPExport_OnEventExportCell);
                OLAPGrid.OnGetPopupMenu += new TEventPopupMenu(OLAPGrid_OnGetPopupMenu);
                //OLAPChart.OnGetPopupMenu += new TEventPopupMenu(OLAPChart_OnGetPopupMenu);
            }
        }

        void OLAPChart_OnGetPopupMenu(object Sender, TEventPopupMenuArgs e)
        {
            if (!AllowDrillThrough)
                return;


            ICell Cell = OLAPChart.CellSet.Cells(e.Col, e.Row);
            if (Cell.CellType == TCellType.ctData)
            {
                DrillthroughDataCell = new Point(e.Col, e.Row);

                e.Popup.Items.Add(new ToolStripSeparator());

                M = new ToolStripMenuItem();
                M.Text = "İlgili Kayıtlar ...";
                M.Click += new EventHandler(M_Click);
                e.Popup.Items.Add(M);
            }
        }

        protected virtual bool AllowDrillThrough
        {
            get
            {
                return false;
            }
        }



        protected Point DrillthroughDataCell;
        protected IDataCell SelectedCell;
        ToolStripMenuItem M;

        void OLAPGrid_OnGetPopupMenu(object Sender, TEventPopupMenuArgs e)
        {
            if (!AllowDrillThrough)
                return;
            ICell Cell = OLAPGrid.CellSet.Cells(e.Col, e.Row);
            if (Cell.CellType == TCellType.ctData)
            {
                DrillthroughDataCell = new Point(e.Col, e.Row);
                SelectedCell = (IDataCell)OLAPGrid.CellSet.Cells(DrillthroughDataCell.X, DrillthroughDataCell.Y);

                e.Popup.Items.Add(new ToolStripSeparator());

                M = new ToolStripMenuItem();
                M.Text = "İlgili Kayıtlar ...";
                M.Click += new EventHandler(M_Click);
                e.Popup.Items.Add(M);
            }
        }

        protected virtual void SetDrillthroughData(Kalitte.BI.Shared.AnalyticsService.FilterSelectData filter)
        {
        }

        public virtual bool SendUsernamePasswordWithFilterData
        {
            get
            {
                return true;
            }
        }

        protected virtual int LookupRowIndex
        {
            get
            {
                return 0;
            }
        }

        protected void DoDrillThrough(DataTable table)
        {
            StringBuilder sb = new StringBuilder(table.Rows.Count * 3);

            Kalitte.BI.Shared.AnalyticsService.FilterSelectData f = new Kalitte.BI.Shared.AnalyticsService.FilterSelectData();
            f.Key = Guid.NewGuid();
            if (SendUsernamePasswordWithFilterData)
            {
                f.Username = ServerServices.UserName;
                f.Password = ServerServices.Password;
            }
            f.SelectType = Shared.AnalyticsService.DataSelectType.IDList;
            f.Filters = new Dictionary<string, string>();


            List<int> idList = new List<int>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
                idList.Add(Convert.ToInt32(row[LookupRowIndex]));
            f.IDList = idList.ToArray();

            for (int i = 0; i < SelectedCell.Address.LevelsCount; i++)
            {
                f.Filters.Add(SelectedCell.Address.Levels(i).DisplayName, SelectedCell.Address.Members(i).DisplayName);
            }

            SetDrillthroughData(f);

            ShowDrillTh(f);



        }

        protected virtual void ShowDrillTh(Kalitte.BI.Shared.AnalyticsService.FilterSelectData f)
        {
            DrillThForm fm = new DrillThForm();
            fm.ShowWebRequestToolbar(AllowWebDrillThroughRequest);
            fm.Execute(f);
        }

        void M_Click(object sender, EventArgs e)
        {
            //TfmDrillthrough F = new TfmDrillthrough();
            System.Data.DataTable DataTable = new DataTable("Drillthrough");
            try
            {
                SelectedCell = (IDataCell)OLAPGrid.CellSet.Cells(DrillthroughDataCell.X, DrillthroughDataCell.Y);
                SelectedCell.Drillthrough(DataTable, 0, TDrillThroughMethod.PureFactTable);
                DoDrillThrough(DataTable);
                //F.DataSource = DataTable;
                //F.ShowDialog();
            }
            finally
            {
                DataTable.Dispose();
                //F.Dispose();
            }
        }


        void OLAPExport_OnGetPreferredWidth(object sender, TEventGetPreferredWidthArgs e)
        {

        }

        void OLAPExport_OnEventExportCell(object sender, TEventExportCellArgs e)
        {
            if (e.Cell.CellType == TCellType.ctData)
            {
                e.AlignText = StringAlignment.Far;
            }
        }

        void excelExport(object sender, TEventGetXlsFormatArgs e)
        {
            if (e.Cell == null)
            {
                return;
            }

            switch (e.Cell.CellType)
            {
                case TCellType.ctData:
                    if (e.Value is long)
                    {
                        //e.CellStyle.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                        //e.CellStyle.NumberFormat = "0";
                    }

                    if (e.Value is double)
                    {
                        //e.CellStyle.HorizontalAlignment = HorizontalAlignmentStyle.Right;
                        //e.CellStyle.NumberFormat = "0.00";
                    }
                    break;
                case TCellType.ctMember:
                    break;
            }

            //e.CellStyle.WrapText = false;
        }


        protected virtual void SendToUserDahsboardBtn_Click(object sender, EventArgs e)
        {
            //if (_activeView == activeView.Chart)
            //    AnaliticsApplication.ExecutePublishDialog(OLAPChart);
            //else AnaliticsApplication.ExecutePublishDialog(OLAPGrid);
        }

        private void clearGrid_Click(object sender, EventArgs e)
        {
            //OLAPGrid.AxesLayout.Clear();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AnalyticsForm_Activated(object sender, EventArgs e)
        {
            OLAPGrid.Focus();
        }


        private void ctlNewWindow_Click_1(object sender, EventArgs e)
        {
            NewInstance();
        }

        private void ctlSaveReport_Click(object sender, EventArgs e)
        {
            InputForm form = new InputForm("Lütfen rapor adını giriniz");
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    object olapObject;
                    if (ActiveView == activeView.Grid) olapObject = OLAPGrid; else olapObject = OLAPChart;
                    FmWait.Execute("Rapor Kaydediliyor...");
                    try
                    {
                        BIUtils.SaveOlapCubeData(olapObject, form.Input, GetFormType(), form.ctlSaveAsTemplate.Checked);
                    }
                    finally
                    {
                        FmWait.Done();
                    }
                    MessageBox.Show("Rapor Başarıyla Kaydedildi");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message.ToString(), "Hata");
                }
            }
        }

        private BIUtils.FormType GetFormType()
        {
            Type formtype = this.GetType();
            switch (formtype.Name)
            {

                case "PersonelAnalizForm":
                    {
                        return BIUtils.FormType.PersonelAnaliz;
                    }
                case "PersonelIzinAnalizForm":
                    {
                        return BIUtils.FormType.PersonelIzinAnaliz;
                    }
                case "PersonelMaasAnaliz":
                    {
                        return BIUtils.FormType.PersonelMaasAnaliz;
                    }
                case "PersonelSicilAnalizForm":
                    {
                        return BIUtils.FormType.PersonelSicilAnaliz;
                    }
                default:
                    return BIUtils.FormType.PersonelAnaliz;
            }
        }

        private void ctlShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = BIUtils.ShowSavedReports(GetFormType().ToString("g"), ActiveView == activeView.Grid ? "Grid" : "Grafik");
                if (ms != null)
                {
                    FmWait.Execute("Açılıyor ...");
                    try
                    {
                        if (ActiveView == activeView.Grid) OLAPGrid.Load(ms); else OLAPChart.Load(ms);
                    }
                    finally
                    {
                        FmWait.Done();
                    }

                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message.ToString(), "Hata");
            }
        }

        private void ctlExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Excel Dosyası|*.xls";
            sf.DefaultExt = "Excel Dosyası|*.xls";
            sf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sf.FileName = DateTime.Now.ToString("dd.MM.yyyy") + " Tarihli Rapor";
            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (ActiveView == activeView.Grid)
                {
                    OLAPGrid.ExportTo(sf.FileName, TConvertType.ctXLS);
                }
                MessageBox.Show("Excel'e Başarıyla Aktarıldı");
            }
        }







    }
}
