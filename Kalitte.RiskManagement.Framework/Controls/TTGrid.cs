﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

using Ext.Net.Utilities;
using Kalitte.RiskManagement.Framework.Security;
using Kalitte.RiskManagement.Framework.UI;
using Kalitte.RiskManagement.Framework.Core;
using System.Web.UI;
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class GridRowCommandEventArgs : EventArgs
    {
        public string ID { get; private set; }
        public int RowIndex { get; private set; }
        public string Command { get; private set; }
        public DirectEventArgs E { get; private set; }

        public int RecordID
        {
            get
            {
                return int.Parse(ID);
            }
        }

        internal GridRowCommandEventArgs(DirectEventArgs e)
        {
            ID = e.ExtraParams["id"];
            RowIndex = int.Parse(e.ExtraParams["rowIndex"]);
            Command = e.ExtraParams["command"];
            E = e;
        }
    }

    public delegate void GridRowCommandHandler(object sender, GridRowCommandEventArgs e);

    public enum SelectionModelType
    {
        None,
        Row,
        CheckBox
    }

    public enum GridFilterType
    {
        None,
        Remote,
        Local
    }

    public class TTGrid : GridPanel
    {
        public string KnownCommandPrefix { get; set; }
        public bool IsEditable { get; set; }
        public bool AutoGenerateDeleteCommand { get; set; }
        public bool AllowPaging { get; set; }
        public SelectionModelType DefaultSelectionModel { get; set; }
        public GridFilterType FilterType { get; set; }

        private ICommandHandler mvcControl = null;
        private ICommandSource commandSource = null;

        public void SelectRow(int index)
        {
            if (RowSelection != null)
            {
                RowSelection.ClearSelections();
                RowSelection.SelectedRows.Add(new SelectedRow(index));
                RowSelection.UpdateSelection();
                RowSelection.FireEvent("rowselect");
            }
        }

       

        public event GridRowCommandHandler GridRowCommand;

        public TTGrid()
            : base()
        {
            StripeRows = true;
            LoadMask.ShowMask = true;
            AutoGenerateDeleteCommand = false;
            DefaultSelectionModel = SelectionModelType.Row;
            AllowPaging = true;
            FilterType = GridFilterType.Remote;
        }

        public TTGridFilters Filters
        {
            get
            {
                return Plugins.Where(p=>p.GetType() == typeof(TTGridFilters)).FirstOrDefault() as TTGridFilters;
            }
        }

        public CheckboxSelectionModel CheckBoxSelection
        {
            get
            {
                return SelectionModel.Primary as CheckboxSelectionModel;
            }
        }

        public RowSelectionModel RowSelection
        {
            get
            {
                return SelectionModel.Primary as RowSelectionModel;
            }
        }

        protected ICommandHandler MvcControl
        {
            get
            {
                if (mvcControl == null)
                    mvcControl = GetCommandHandler(true);
                return mvcControl;
            }
        }

        protected ICommandSource CommandSource
        {
            get
            {
                if (commandSource == null)
                    commandSource = GetCommandSource();
                return commandSource;
            }
        }

        protected ICommandHandler GetCommandHandler(bool useThis)
        {
            if (useThis && this is ICommandHandler)
            {
                return (ICommandHandler)this;
            }
            else
            {
                Control t = this;
                while ((t = t.Parent) != null)
                {
                    if (t is ICommandHandler)
                    {
                        return (ICommandHandler)t;

                    }
                }
            }
            return null;
        }

        protected ICommandSource GetCommandSource()
        {

            Control t = this;
            while ((t = t.Parent) != null)
            {
                if (t is ICommandSource)
                {
                    return (ICommandSource)t;

                }
            }

            return null;
        }

        public CheckboxSelectionModel CheckboxSelection
        {
            get
            {
                return this.SelectionModel.Primary as CheckboxSelectionModel;
            }
        }

        public void ClearSelections()
        {
            if (RowSelection != null)
            {
                RowSelection.ClearSelections();
                RowSelection.SelectedRecordID = "";
                RowSelection.UpdateSelection();
            }
            else if (CheckboxSelection != null)
            {
                CheckboxSelection.ClearSelections();
                CheckboxSelection.SelectedRecordID = "";
                CheckboxSelection.UpdateSelection();
            }
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            DirectEvents.Command.ExtraParams.Add(new Parameter("command", "command", ParameterMode.Raw));
            DirectEvents.Command.ExtraParams.Add(new Parameter("id", "record.id", ParameterMode.Raw));
            DirectEvents.Command.ExtraParams.Add(new Parameter("rowIndex", "rowIndex", ParameterMode.Raw));
            DirectEvents.Command.EventMask.ShowMask = true;
            DirectEvents.Command.EventMask.Target = MaskTarget.Parent;
            DirectEvents.Command.EventMask.Msg = "Yükleniyor ...";
            Listeners.Command.Handler = "return GridCommandHandler(command,record,this);";
            Listeners.RowDblClick.Fn = "gridRowDblClick";

            if (AllowPaging)
            {
                TTPagingToolbar pagingToolbar = new TTPagingToolbar();
                pagingToolbar.ID = this.ID + "pt";
                pagingToolbar.StoreID = this.StoreID;
                BottomBar.Add(pagingToolbar);
            }

            if (DefaultSelectionModel == SelectionModelType.Row)
            {
                RowSelectionModel rowSelectionModel = new RowSelectionModel();
                rowSelectionModel.ID = this.ID + "rsm";
                rowSelectionModel.SingleSelect = true;
                this.SelectionModel.Add(rowSelectionModel);
            }
            else if (DefaultSelectionModel == SelectionModelType.CheckBox)
            {
                CheckboxSelectionModel model = new CheckboxSelectionModel();
                model.ID = this.ID + "chksm";
                this.SelectionModel.Add(model);
            }

            if (FilterType != GridFilterType.None)
            {
                var filters = Plugins.Where(p=>p.GetType() == typeof(TTGridFilters)).FirstOrDefault() as TTGridFilters;
                if (filters == null)
                {
                    filters = new TTGridFilters();
                    filters.Local = FilterType == GridFilterType.Local;
                    filters.ID = this.ID + "flt";
                }
                List<TTColumn> columns = this.ColumnModel.Columns.Where(p=>p is TTColumn).Select(p => (TTColumn)p).ToList();
                Plugins.Add(filters);
                var store = GetStore();
                filters.SetFilters(columns, store);
            }

            if (!Ext.Net.X.IsAjaxRequest)
            {
                var ttcommandcols = ColumnModel.Columns.Where(p => p is TTCommandColumn).ToList();
                foreach (TTCommandColumn item in ttcommandcols)
                {
                    if (item.MergeColumnIndex != -1)
                    {
                        if (ColumnModel.Columns[item.MergeColumnIndex] is TTCommandColumn)
                        {
                            TTCommandColumn col = (TTCommandColumn)ColumnModel.Columns[item.MergeColumnIndex];
                            col.Commands.AddRange(item.Commands);
                            ColumnModel.Columns.Remove(item);
                            col.Width = new System.Web.UI.WebControls.Unit(col.Commands.Count * 24);
                        }
                    }
                }
            }
            DirectEvents.Command.Event += new ComponentDirectEvent.DirectEventHandler(Command_Event);
            foreach (TTCmdButon btn in ControlUtils.FindControls<TTCmdButon>(this))
                btn.ConnectedGrid = this.ID;
        }



        void Command_Event(object sender, DirectEventArgs e)
        {
            var args = new GridRowCommandEventArgs(e);
            CommandInfo cmd = new CommandInfo(args.Command, e);
            cmd.Source = CommandSource;
            var ttcommandcols = ColumnModel.Columns.Where(p => p is TTCommandColumn).Select(p=>(TTCommandColumn)p).ToList();
            foreach (var col in ttcommandcols)
            {
                var targetCommand = col.Commands.FirstOrDefault(o => ((TTGridCommand)o).CommandName == cmd.CommandName);
                if (targetCommand != null)
                    PermissionBusiness.ValidatePermission(targetCommand);
            }
            
            if (GridRowCommand != null)
                GridRowCommand(this, args);
            else if (MvcControl != null)
                MvcControl.ProcessCommand(sender, cmd);
        }



        public void SelectIfNotSelected(int index)
        {
            if (RowSelection != null && RowSelection.SelectedRows.Count == 0)
                SelectRow(index);
        }

        public void SelectById(object id)
        {
            if (RowSelection != null)
            {
                RowSelection.ClearSelections();
                RowSelection.SelectedRows.Add(new SelectedRow(id.ToString()));
                RowSelection.SelectedRecordID = id.ToString();
                RowSelection.UpdateSelection();
                RowSelection.FireEvent("rowselect");
            }
            else if (CheckBoxSelection != null)
            {
                CheckBoxSelection.ClearSelections();
                CheckBoxSelection.SelectedRows.Add(new SelectedRow(id.ToString()));
                CheckBoxSelection.SelectedRecordID = id.ToString();
                CheckBoxSelection.UpdateSelection();
                CheckBoxSelection.FireEvent("rowselect");
            }

        }

        public void SelectNext()
        {
            if (RowSelection != null)
            {
                RowSelection.SelectNext();
                RowSelection.FireEvent("rowselect");
            }
        }

        public void SelectPrevious()
        {
            if (RowSelection != null)
            {
                RowSelection.SelectPrevious();
                RowSelection.FireEvent("rowselect");
            }
        }
    }
}
