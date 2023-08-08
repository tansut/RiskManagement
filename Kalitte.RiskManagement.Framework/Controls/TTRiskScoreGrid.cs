using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using System.Web.UI.WebControls;
using Kalitte.RiskManagement.Framework.Business.Common;
using Kalitte.RiskManagement.Framework.Core;
using System.Collections;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTRiskScoreStore : TTStore
    {
        public TTRiskScoreStore()
        {
            UseServerSidePaging = false;
        }

        protected override void OnInit(EventArgs e)
        {

            RiskMatrisBusiness bll = new RiskMatrisBusiness();
            var matrix = bll.GetMatrix();
            //int rowCount = matrix.GetUpperBound(0);
            int colCount = matrix.GetUpperBound(0) + 1;
            JsonReader reader = new JsonReader();
            reader.IDProperty = "Score";
            //reader.Fields.Add(new RecordField("Score"));
            //reader.Fields.Add(new RecordField("Display"));

            //foreach (var prop in typeof(RiskMatrisEntity).GetProperties())
            //{
            //    RecordField f = new RecordField(string.Format("{0}", prop));
            //    reader.Fields.Add(f);
            //}

            for (int i = 0; i < colCount; i++)
            {
                RecordField f = new RecordField(string.Format("col{0}", i));
                reader.Fields.Add(f);
            }
            Reader.Add(reader);
            base.OnInit(e);

        }

        public DynamicEntityList GetRows()
        {
            DynamicEntityList list = new DynamicEntityList();
            RiskMatrisBusiness bll = new RiskMatrisBusiness();
            var matrix = bll.GetMatrix();
            int rowCount = matrix.GetUpperBound(1) + 1;
            int colCount = matrix.GetUpperBound(0) + 1;

            Dictionary<string, Type> columns = new Dictionary<string, Type>();
            for (int i = 0; i < colCount; i++)
            {
                columns.Add(string.Format("col{0}", i), typeof(string));
            }

            for (int i = rowCount - 1; i >= 0; i--)
            {
                ArrayList values = new ArrayList();
                for (int j = 0; j < colCount; j++)
                {
                    values.Add(matrix[i, j]);
                }
                var de = new DynamicEntity(columns, values.ToArray());
                list.Add(de);
            }

            return list;
        }
    }

    public class TTRiskScoreGrid : TTGrid
    {
        TTRiskScoreStore store = null;

        public TTRiskScoreGrid()
        {
            HideHeaders = true;
            AllowPaging = false;
            DefaultSelectionModel = SelectionModelType.None;
            FilterType = GridFilterType.None;
            ColumnLines = false;
            StripeRows = false;
            CellSize = 65;
            Border = false;
            BodyBorder = false;
            Width = 500;
        }

        private void CreateStore()
        {
            if (store == null)
            {
                store = new TTRiskScoreStore();
                store.ID = this.ID + "store";
                Store.Add(store);

            }
        }

        public int CellSize { get; set; }

        protected override void OnInit(EventArgs e)
        {
            CreateStore();
            var view = new Ext.Net.GridView() { ForceFit = true };
            view.GetRowClass.Fn = "function(record, index) { return 'riskMatrixRow'}";
            
            View.Add(view);
            if (!Ext.Net.X.IsAjaxRequest)
            {
                this.CustomConfig.Add(new ConfigItem("cellSize", this.CellSize.ToString()));
                RiskMatrisBusiness bll = new RiskMatrisBusiness();
                var matrix = bll.GetMatrix();
                int colCount = matrix.GetUpperBound(0) + 1;
                for (int i = 0; i < colCount; i++)
                {
                    TTColumn column = new TTColumn();
                    column.Fixed = true;
                    column.Width = new Unit(CellSize);
                    column.DataIndex = string.Format("col{0}", i.ToString());
                    
                    column.AutoFilter = false;
                    column.Sortable = false;
                    column.Renderer = new Renderer() { Fn = "renderMatrixCell" };
                    ColumnModel.Columns.Add(column);
                }
            }
            base.OnInit(e);
        }
    }
}
