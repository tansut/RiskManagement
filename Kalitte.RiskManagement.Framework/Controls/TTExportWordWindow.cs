using Kalitte.RiskManagement.Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Controls
{
   public class TTExportWordWindow:ExportWindow
    {
       private TTExportWordButton exportButton;
       public TTExportWordWindow()
           : base()
       {
    //       exportButton = exportWordButton;
           Icon = Ext.Net.Icon.PageWord;
           SetGridProps();
       }

       public override void SetButtonProps()
       {
           //base.SetButtonProps();
       }

       public override void Bind(Type t)
       {
           var metadata = EntityMetadata.FromTypeUsingGrid(t, DataGrid).OrderBy(p => p.PropertyName).ToList();
           grid.Store.Primary.DataSource = metadata;
           grid.Store.Primary.DataBind();
           ViewState["type"] = t.FullName;
       }
    }
}
