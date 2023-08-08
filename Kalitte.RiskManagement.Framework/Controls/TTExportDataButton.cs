using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using System.Web.UI;
using System.Web;
using System.Collections;
using System.Net.Mime;
using Kalitte.RiskManagement.Framework.Utility;
using Kalitte.RiskManagement.Framework.Core;

namespace Kalitte.RiskManagement.Framework.Controls
{

 
    public class TTExportDataButton : TTCmdButon
    {        
                
        public TTExportDataButton()
            : base()
        {
            Text = "Verileri Excel'e Aktar";
            Icon = Ext.Net.Icon.PageExcel;
            ToolTip = "Verileri Excel'e aktar";            
            this.KnownCommand = Security.KnownCommand.ExportData;
        
            ForceGridSelection = false;

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            DirectEvents.Click.ExtraParams.Add(new Parameter("state", string.Format("getGridState('{0}')", OwnerGrid.ClientID), ParameterMode.Raw));

            DirectEvents.Click.IsUpload = true;

        }

        public virtual void ExportData(List<EntityMetadata> metadata, IList data)
        {

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ciktidosya.xls");
                    new EntityListExporter(metadata, data, HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();


        }


        public virtual bool ProcessCommand(object sender, CommandInfo cmd)
        {
            
            var parameters = new ListingParameters();
            var gridState = GridState.FromJson(cmd.Parameters["state"].ToString());
            if (gridState != null)
            {
                if (gridState.Sort != null)
                {
                    parameters.SortField = gridState.Sort.field;
                    parameters.Dir = gridState.Sort.GetDirection();
                }
                if (gridState.Filters != null)
                {
                    parameters.LoadFilter(gridState.Filters);
                }
            }
            parameters.Units = UnitFilterManager.GetActiveUnits();
            var exportData = CommandSource.ControllerObject.RetreiveItems(parameters);                                             
            var metaData = cmd.Parameters["metadata"] as List<EntityMetadata>;
            ExportData(metaData, exportData);
            return true;
        }

  
    }
}
