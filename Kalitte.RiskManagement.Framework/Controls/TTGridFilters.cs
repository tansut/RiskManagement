using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTGridFilters : GridFilters
    {
        public TTGridFilters()
            : base()
        {
            Local = false;
        }

        private GridFilter createFilterBasedOnField(RecordField field)
        {
            switch (field.Type)
            {
                case RecordFieldType.Auto:
                    return new StringFilter();
                case RecordFieldType.Boolean:
                    return new BooleanFilter();
                case RecordFieldType.Date:
                    return new DateFilter();
                case RecordFieldType.Float:
                    return new NumericFilter();
                case RecordFieldType.Int:
                    return new NumericFilter();
                case RecordFieldType.String:
                    return new StringFilter();
                default:
                    return new StringFilter();
            }
        }

        internal void SetFilters(List<TTColumn> columns, Store store)
        {
            foreach (var column in columns)
            {
                if (column.AutoFilter && !string.IsNullOrEmpty(column.DataIndex))
                {
                    var fieldFound = store.Reader.Reader.Fields.SingleOrDefault(p => p.Name == column.DataIndex);
                    bool alreadyAdded = Filters.Any(p => p.DataIndex == column.DataIndex);
                    if (fieldFound != null && !alreadyAdded)
                    {
                        var filter = createFilterBasedOnField(fieldFound);
                        filter.DataIndex = fieldFound.Name;
                        Filters.Add(filter);
                    }
                }
            }
        }
    }
}
