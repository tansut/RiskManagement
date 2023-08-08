using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTCommandColumn : Ext.Net.CommandColumn
    {
        public TTCommandColumn()
            : base()
        {
            this.Fixed = true;
        }

        private int mergeColumnIndex = -1;

        public int MergeColumnIndex 
        {
            get
            {
               return mergeColumnIndex;
            }
            set
            {
                mergeColumnIndex = value;
            }
        }
    }
}
