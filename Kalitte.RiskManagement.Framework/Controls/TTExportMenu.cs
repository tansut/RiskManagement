using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kalitte.RiskManagement.Framework.Core;
using Ext.Net;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Security;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class MetadataGrid: TTGrid
    {
        public MetadataGrid()
            : base()
        {
            TTStore store = new TTStore();
            store.AutoLoad = false;
            store.UseServerSidePaging = false;
            JsonReader reader = new JsonReader();
            reader.IDProperty = "PropertyName";
            reader.Fields.Add(new RecordField("PropertyName"));
            reader.Fields.Add(new RecordField("Description"));
            store.Reader.Add(reader);
            AllowPaging = false;
            DefaultSelectionModel = SelectionModelType.CheckBox;
            Store.Add(store);

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            View.Add(new GridView() { ForceFit = true });
            TTColumn nameCol = new TTColumn() { DataIndex = "Description", Header = "Sütun" };
            ColumnModel.Columns.Add(nameCol);
        }
    }

    public class ExportWindow : TTWindow, ICommandHandler
    {
        public MetadataGrid grid;
        public TTExportDataButton exportBtn;
        public TTExportWordButton exportWordBtn;

        public ExportWindow()
        {
            Title = "Dışa Aktarım Seçenekleri";
            Width = 300;
            InitCenter = true;
            Height = 300;
            Maximizable = false;
            ButtonAlign = Ext.Net.Alignment.Center;
            Icon = Ext.Net.Icon.PageExcel;
            Layout = "fit";
            BodyBorder = false;
        }
        public virtual void SetButtonProps()
        {
            exportBtn = new TTExportDataButton();
            exportBtn.ID = this.ID + "exp";
            Buttons.Add(exportBtn);

            exportWordBtn = new TTExportWordButton();            
            exportWordBtn.ID = this.ID + "expW";
            Buttons.Add(exportWordBtn);

        }

        public virtual void SetGridProps()
        {
            grid = new MetadataGrid();
            grid.ID = this.ID + "grd";
            Items.Add(grid);

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SetButtonProps();
            SetGridProps();
        }

        public virtual void Bind(Type t)
        {
            var metadata = EntityMetadata.FromTypeUsingGrid(t, DataGrid).OrderBy(p=>p.PropertyName).ToList();
            
            grid.Store.Primary.DataSource = metadata;
            grid.Store.Primary.DataBind();
            ViewState["type"] = t.FullName;
            if (grid.CheckBoxSelection.SelectedRows.Count == 0)
            {
                foreach (var col in DataGrid.ColumnModel.Columns)
                {
                    grid.CheckBoxSelection.SelectedRows.Add(new SelectedRow(col.DataIndex));
                }
                grid.CheckBoxSelection.UpdateSelection();
            }
        }



        #region ICommandHandler Members

        public bool ProcessCommand(object sender, CommandInfo cmd)
        {
            if (cmd.KnownCommand == Security.KnownCommand.ExportData)
            {
                var entityType = Type.GetType((string)ViewState["type"]);
                var metadata = EntityMetadata.FromTypeUsingGrid(entityType, DataGrid);
                var filteredList = new List<EntityMetadata>(metadata.Count);
                foreach (var row in grid.CheckBoxSelection.SelectedRows)
                {
                    var item = metadata.SingleOrDefault(p => p.PropertyName == row.RecordID);
                    filteredList.Add(item);
                }

                if (filteredList.Count == 0)
                    throw new BusinessException("Lütfen en az bir adet sütun seçiniz");
                cmd.Parameters["metadata"] = filteredList;
                ((TTExportDataButton)sender).ProcessCommand(sender, cmd);
                //exportBtn.ProcessCommand(sender, cmd);

                return true;
            }
            else return false;
        }

        #endregion

        public GridPanel DataGrid { get; set; }
    }

    public class TTExportMenu: TTCmdButon, ICommandHandler
    {
        private ExportWindow window = null;
        public ExportWindow Window
        {
            get { return window; }
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            window = new ExportWindow();
            window.ID = this.ID + "wnd";
            window.DataGrid = this.OwnerGrid;
            Controls.Add(window);
        }

        public TTExportMenu()
            : base()
        {
            KnownCommand = Security.KnownCommand.ExportData;
            Text = "Dışa Aktar";
            Icon = Ext.Net.Icon.PageExcel;
            ToolTip = "Verileri Excel'e aktar";
            ForceGridSelection = false;
        }

        #region ICommandHandler Members

        public bool ProcessCommand(object sender, CommandInfo cmd)
        {
            var bllType = CommandSource.ControllerObject.EntityType;
            window.Bind(bllType);
            window.Show();
            return true;
        }

        #endregion
    }
}
