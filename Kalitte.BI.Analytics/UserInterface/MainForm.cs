using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Kalitte.BI.Shared.AnalyticsService;
using Kalitte.BI.Shared;
using Kalitte.BI.Shared.ServiceManagement;
using System.Diagnostics;
using System.IO;
using RadarSoft.WinForms;
using RadarSoft.WinForms.Grid;

namespace Kalitte.BI.Analytics.UserInterface
{


    public partial class MainForm : Kalitte.BI.Analytics.UserInterface.BaseForm
    {
        internal class AnaliticsApp : IAnaliticsApp
        {
            //public void ExecutePublishDialog(RadarSoft.WinForms.TOLAPControlGeneral control)
            //{
            //    FmPublishToDashboard f = new FmPublishToDashboard();
            //    if (f.Execute(control is TOLAPGrid ? ContentType.Html: ContentType.Resim))
            //    {
            //        using (MemoryStream stream = new MemoryStream())
            //        {

            //            TConvertType radarConvertType = TConvertType.ctPDF;

            //            switch (f.ContentTip)
            //            {
            //                case ContentType.Html:
            //                    {
            //                        radarConvertType = TConvertType.ctHTML;
            //                        break;
            //                    }
            //                case ContentType.MSExcel:
            //                    {
            //                        radarConvertType = TConvertType.ctXLS;
            //                        break;
            //                    }
            //                case ContentType.AdobePdf:
            //                    {
            //                        radarConvertType = TConvertType.ctPDF;
            //                        break;
            //                    }
            //                case ContentType.Resim:
            //                    {
            //                        radarConvertType = TConvertType.ctJPG;
            //                        break;
            //                    }
            //            }

            //            control.ExportTo(stream, radarConvertType);

            //            ServerServices.AnalyticsService.CreateNewWidget(f.SelectedDashboard, f.ContentTip, f.Title, stream.ToArray());
            //            stream.Close();
            //        }
            //        MessageBox.Show("Gösterge panelinize içerik yerleştirildi.");
            //    }
            //}
        }


        private ModuleInfo[] modules;
        private AnaliticsApp application = new AnaliticsApp();

        public MainForm()
        {
            InitializeComponent();
        }

        private ListViewGroup GetGroup(string title)
        {
            foreach (ListViewGroup g in modulesList.Groups)
                if (g.Header == title)
                    return g;

            return modulesList.Groups.Add(title, title);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lvBigIcons.Tag = View.LargeIcon;
            LvDetails.Tag = View.Details;
            LvList.Tag = View.List;
            LvSmallIcons.Tag = View.SmallIcon;
            LvTiles.Tag = View.Tile;
            modules = ServerServices.AnalyticsService.GetModules();
            //miniBrowser.Url = new Uri(string.Format(ServerServices.Params.DecisionSupportBrowserWinUrl + "&R=" + new Random().Next(1500).ToString(), ServerServices.UserName)); ;
            foreach (ModuleInfo m in modules)
            {
                ListViewGroup group = GetGroup(m.GroupName);
                ListViewItem item = new ListViewItem(new string[] { m.ModuleName, m.Description }, m.ImageIndex);
                modulesList.Items.Add(item);
                item.Group = group;
                item.Tag = m;
            }
            if (modulesList.Items.Count > 0)
                modulesList.Items[0].Selected = true;
        }



        private void executeSelectedModule()
        {
            ListView.SelectedListViewItemCollection col = modulesList.SelectedItems;
            if (col.Count == 0)
                return;
            ModuleInfo m = col[0].Tag as ModuleInfo;

            Type t = Type.GetType(m.ActivatorInfo);
            (Activator.CreateInstance(t) as BaseAnalyticModule).Execute(m, application);
        }

        private void modulesList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            executeSelectedModule();
        }



        private void LvViewclick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            modulesList.View = (View)item.Tag;
            lvBigIcons.Checked = false;
            LvDetails.Checked = false;
            LvList.Checked = false;
            LvSmallIcons.Checked = false;
            LvTiles.Checked = false;
            item.Checked = true;
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm f = new AboutForm();
            f.ShowDialog();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void yardımKonularıToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openModule_Click(object sender, EventArgs e)
        {
            executeSelectedModule();
        }


        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(ServerServices.Params.PortalUrl + "/help.aspx?dest=client");
        }

        private void executeCurrentMenuItem_Click(object sender, EventArgs e)
        {
            executeSelectedModule();
        }
    }
}
