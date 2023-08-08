using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTPagingToolbar : PagingToolbar
    {
        public TTPagingToolbar()
            : base()
        {
            PageSize = 25;
            DisplayInfo = true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var exportBtn = new TTExportMenu();
            exportBtn.ID = this.ID + "exp";
            Items.Add(new ToolbarSeparator());
            Items.Add(exportBtn);
        }


    }
}
