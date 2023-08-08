using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerPlatform.DataProvider;
using MapEntities;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Kalitte.RiskManagement.Framework.Providers
{
    public class Il
    {
        public int Kod { get; set; }
        public string Ad { get; set; }
        public List<ShapeSegment> Segments { get; set; }
    }

    public class TurkeyMapXMLProvider : BaseDataProvider
    {
        public static List<Il> iller = null;

        public static List<Il> GetIller()
        {
            if (iller == null)
            {
                lock (typeof(TurkeyMapXMLProvider))
                {
                    if (iller == null)
                    {
                        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Kalitte.RiskManagement.Framework.Resource.iller.xml"))
                        {
                            XmlSerializer ser = new XmlSerializer(typeof(List<Il>));
                            iller = (List<Il>)ser.Deserialize(stream);
                            stream.Close();
                        }
                    }
                    return iller;
                }
            }
            else return iller;
        }

  
        public override void Connect(string connectionString)
        {

        }

        public override DALEntities.ViewSchemaList GetSchema(DALEntities.SchemaRequest request)
        {
            DALEntities.ViewSchemaList result = new DALEntities.ViewSchemaList();

            DALEntities.ViewSchema iller = new DALEntities.ViewSchema() { Name = "İller" };
            iller.Columns.Add(new DALEntities.Column() { Name = "Kod", Type = DALEntities.DataType.Integer });
            iller.Columns.Add(new DALEntities.Column() { Name = "Ad", Type = DALEntities.DataType.String });
            result.Add(iller);

            return result;
        }

        public override MapEntities.Shape GetShape(object data, MapEntities.ShapeItemBindingRule rule)
        {
            Shape result = new Shape();

            Il il = data as Il;
            result.Segments = il.Segments;
            return result;
        }

        public override DALEntities.ViewResult GetViewResult(DALEntities.ViewRequest request)
        {           
            DALEntities.ViewResult result = new DALEntities.ViewResult();
            result.Data = GetIller();
            result.RequestID = request.ID;
            return result;
        }

        public override List<DALEntities.ViewSchema> GetViews()
        {
            return null;
        }

        public override DALEntities.ProviderSource Source
        {
            get { return DALEntities.ProviderSource.Other; }
        }
    }
}
