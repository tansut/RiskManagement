using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ext.Net;
using System.Collections;
using System.IO;
using Kalitte.RiskManagement.Framework.Controls;
using System.Reflection;
using System.Web;

namespace Kalitte.RiskManagement.Framework.Utility
{
    public class EntityListExporter
    {
        Stream BaseStream;

        private void WriteStream(string str)
        {
            byte[] temp = Encoding.UTF8.GetBytes(str);
            BaseStream.Write(temp, 0, temp.Length);
        }


        public EntityListExporter(List<EntityMetadata> metadata, IList data, Stream stream)
        {
            BaseStream = stream;
            WriteStream("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            WriteStream("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:html=\"http://www.w3.org/TR/REC-html40\" xmlns:msxsl=\"urn:schemas-microsoft-com:xslt\" xmlns:user=\"urn:my-scripts\">");
            WriteStream("<Worksheet ss:Name=\"Çıktı\">");
            WriteStream("<Table x:FullColumns=\"1\" x:FullRows=\"1\">");
            WriteStream("<Row>");
            foreach (var m in metadata)
            {

                WriteStream("<Cell>");
                WriteStream("<Data ss:Type=\"String\">" + m.Description + "</Data>");
                WriteStream("</Cell>");

            }
            WriteStream("</Row>");
            foreach (object entity in data)
            {
                WriteStream("<Row>");
                foreach (var m in metadata)
                {
                    if (true)
                    {
                        WriteStream("<Cell>");
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
                                WriteStream("<Data ss:Type=\"String\">NULL</Data>");
                            }
                            else if (value.GetType() == typeof(int))
                            {
                                WriteStream("<Data ss:Type=\"Number\">" + Convert.ToInt32(value) + "</Data>");
                            }
                            else if (value.GetType() == typeof(DateTime))
                            {
                                WriteStream("<Data ss:Type=\"String\">" + Convert.ToDateTime(value).ToString("dd.MM.yyyy") + "</Data>");
                            }
                            else if (value.GetType() == typeof(bool))
                            {
                                WriteStream("<Data ss:Type=\"String\">" + HttpUtility.HtmlEncode(value.ToString()) + "</Data>");
                            }
                            else if (value.GetType() == typeof(double))
                            {
                                WriteStream("<Data ss:Type=\"String\">" + Convert.ToDouble(value) + "</Data>");
                            }
                            else if (value.GetType() == typeof(float))
                            {
                                WriteStream("<Data ss:Type=\"String\">" + Convert.ToDouble(value) + "</Data>");
                            }
                            else
                            {
                                WriteStream("<Data ss:Type=\"String\">" + value.ToString() + "</Data>");
                                //.EscapeXml()
                            }
                        }
                        WriteStream("</Cell>");
                    }
                }
                WriteStream("</Row>");
            }
            WriteStream("</Table></Worksheet></Workbook>");
        }
    }
}
