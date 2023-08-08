using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerPlatform.DataProvider;
using DALEntities;
using Kalitte.RiskManagement.Framework.Business.Surec;
using Kalitte.RiskManagement.Framework.Business.Common;

namespace Kalitte.RiskManagement.Framework.Providers
{
    public class IllerData
    {
        public int Kod { get; set; }
        public string Ad { get; set; }
        public int RiskGecmisSayisi { get; set; }
    }

    public class TurkeyMapDataProvider : BaseDataProvider
    {


        public static List<IllerData> GetIllerData(string viewName)
        {

            List<IllerData> illerData = null;
                lock (typeof(TurkeyMapDataProvider))
                {
                    if (illerData == null)
                    {
                        illerData = new RiskGecmisBusiness().GetGecmisRiskCountBySkorTanim(viewName);
                    }
                    return illerData;
                }
           
        }

        public override void Connect(string connectionString)
        {

        }

        public TurkeyMapDataProvider()
        {

        }

        public override DALEntities.ViewSchemaList GetSchema(DALEntities.SchemaRequest request)
        {

            DALEntities.ViewSchemaList result = new DALEntities.ViewSchemaList();
            var list = new RiskMatrisBusiness().GetGrupDeger();
            foreach (var item in list)
            {
                DALEntities.ViewSchema vs = new ViewSchema();
                vs.Columns.Add(new DALEntities.Column() { Name = "Kod", Type = DALEntities.DataType.Integer });
                vs.Columns.Add(new DALEntities.Column() { Name = "Ad", Type = DALEntities.DataType.String });
                vs.Columns.Add(new DALEntities.Column() { Name = "RiskGecmisSayisi", Type = DALEntities.DataType.Integer });
                vs.Name = item;
                vs.Description = item;
                result.Add(vs);
            }
            return result;
        }

        public override MapEntities.Shape GetShape(object data, MapEntities.ShapeItemBindingRule rule)
        {
            return null;
        }

        public override DALEntities.ViewResult GetViewResult(DALEntities.ViewRequest request)
        {
            DALEntities.ViewResult result = new DALEntities.ViewResult();
            result.Data = GetIllerData(request.ViewName);
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
