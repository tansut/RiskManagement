using Kalitte.RiskManagement.Framework.Business;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Core;
using Kalitte.RiskManagement.Framework.Model;
using Kalitte.RiskManagement.Framework.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Kalitte.RiskManagement.Framework.Controls
{
    public class TTExportWordButton : TTExportDataButton
    {
        private int EntityID;
        public TTExportWordButton()
        {
            this.KnownCommand = Security.KnownCommand.ExportData;
            Text = "Verileri Word'e Aktar";
            Icon = Ext.Net.Icon.PageWord;
            ToolTip = "Verileri Word'e aktar";
            ForceGridSelection = true;
        }

        public override bool ProcessCommand(object sender, CommandInfo cmd)
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

            if (CommandSource.ControllerObject.EntityType == typeof(Surec))
            {

                var exportData = new WorkflowBusiness().Retrieve(cmd.RecordID);
                var metaData = cmd.Parameters["metadata"] as List<EntityMetadata>;
                ExportWordData(metaData, exportData);
                return true;
            }
            throw new Exception("Bu nesne için Word çıktısı alınamaz.");
        }

        public virtual void ExportWordData(List<Utility.EntityMetadata> metadata, object entity)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=ciktidosya.doc");
            var htmlBase = @"<!DOCTYPE html>
            <html xmlns='http://www.w3.org/1999/xhtml'>
            <head>        
                <title></title>
                <style type='text/css'>
                    .auto-style1 {{
                        width: 172px;
                        vertical-align:top;
                    }}
                </style>
            </head>
            <body>
                <table>
                       {0}                
                </table>
            </body>
            </html>";
            var rowTemplate = "<tr><td class='auto-style1'>{0}: </td><td>{1}</td></tr>";
            StringBuilder sb = new StringBuilder();

            foreach (var m in metadata)
            {
                if (true)
                {
                    PropertyInfo property = entity.GetType().GetProperty(m.PropertyName);
                    if (property != null)
                    {
                        object value;
                        try
                        {
                            value = property.GetValue(entity, null);
                        }
                        catch (Exception exc)
                        {
                            value = exc.Message;
                            if (exc.InnerException != null)
                                value += exc.InnerException.Message;
                        }
                        if (value == null)
                        {
                            sb.Append(string.Format(rowTemplate, m.Description, string.Empty));
                        }
                        else if (value.GetType() == typeof(DateTime))
                        {
                            sb.Append(string.Format(rowTemplate, m.Description, ((DateTime)value).ToString("dd.MM.yyyy")));
                        }
                        else if (value.GetType() == typeof(bool))
                        {
                            sb.Append(string.Format(rowTemplate, m.Description, ((bool)value) ? "Evet" : "Hayır"));
                        }
                        else
                        {
                            sb.Append(string.Format(rowTemplate, m.Description, value.ToString().Replace(Environment.NewLine,"<br>")));
                        }
                    }
                }
            }
            var finalHTML = refineHTML(string.Format(htmlBase, sb.ToString()));

            WriteStream(finalHTML, HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }


        private string refineHTML(string html)
        {
            return html.Replace("İ", "&#304;").Replace("ı", "&#305;").Replace("Ö", "&#214;").Replace("ö", "&#246;").Replace("Ü", "&#220;").Replace("ü", "&#252;").Replace("Ç", "&#199;").Replace("ç", "&#231;").Replace("Ğ", "&#286;").Replace("ğ", "&#287;").Replace("Ş", "&#350;").Replace("ş", "&#351;");            
        }

        private void WriteStream(string str, Stream stream)
        {
            byte[] temp = Encoding.UTF8.GetBytes(str);
            stream.Write(temp, 0, temp.Length);
        }



    }
}
