using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;

namespace Kalitte.RiskManagement.Framework.Controls
{

    public class TTRiskScoreTableRow : TableRow, IDataItemContainer
    {
        private object data;
        private int _itemIndex;

        public TTRiskScoreTableRow(int itemIndex, object o)
        {
            data = o;
            _itemIndex = itemIndex;
        }

        public virtual object Data
        {
            get
            {
                return data;
            }
        }
        object IDataItemContainer.DataItem
        {
            get
            {
                return Data;
            }
        }
        int IDataItemContainer.DataItemIndex
        {
            get
            {
                return _itemIndex;
            }
        }
        int IDataItemContainer.DisplayIndex
        {
            get
            {
                return _itemIndex;
            }
        }
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (Data != null)
            {
                //if (Data is System.Data.Common.DbDataRecord)
                //{
                //    DbDataRecord temp = (DbDataRecord)Data;
                //    for (int i = 0; i < temp.FieldCount; ++i)
                //    {
                //        writer.Write("<TD>");
                //        writer.Write(temp.GetValue(i).ToString());
                //        writer.Write("</TD>");
                //    }
                //}
                //else
                    writer.Write("<TD>" + Data.ToString() + "</TD>");
            }

            else
                writer.Write("<TD>This is a test</TD>");
        }
    }

    public class TTRiskScoreTable: System.Web.UI.WebControls.CompositeDataBoundControl
    {
        protected Table table = new Table();

        public virtual TableRowCollection Rows
        {
            get
            {
                return table.Rows;
            }
        }


        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            int count = 0;
            // If dataSource is not null, iterate through it and
            // extract each element from it as a row, then
            // create a SimpleSpreadsheetRow and add it to the
            // rows collection.
            if (dataSource != null)
            {

                TTRiskScoreTableRow row;
                IEnumerator e = dataSource.GetEnumerator();

                while (e.MoveNext())
                {
                    object datarow = e.Current;
                    row = new TTRiskScoreTableRow(count, datarow);
                    this.Rows.Add(row);
                    ++count;
                }

                Controls.Add(table);
            }
            return count;
        }
    }
}
