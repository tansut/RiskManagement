using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServerPlatform.Configuration;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.Dashboard.Framework;
using System.Xml.Serialization;
using System.IO;
using MapEntities;
using System.Threading;

namespace Kalitte.RiskManagement.Framework.Providers
{
    public class DashboardMapDataProvider: IMapDataProvider
    {
        #region IMapDataProvider Members

        public MapEntities.MapDataRequest GetMapData(Guid requestID, string uri)
        {
            WidgetInstance instance = DashboardFramework.GetWidgetInstance(requestID);
            if (instance == null)
                return null;
            if (instance.SerializedData != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MapDataRequest));
                StringReader reader = new StringReader((string)instance.SerializedData);
                MapDataRequest editedRequest = (MapDataRequest)serializer.Deserialize(reader);

                //if (!Thread.CurrentPrincipal.Identity.IsAuthenticated)
                //{
                //    foreach (var item in editedRequest.ShapeBindingRuleDataList)
                //    {
                //        item.Behaviour.LeftMouseClick.RedirectAddress = "";
                //    }
                //    foreach (var item in editedRequest.SymbolBindingRuleDataList)
                //    {
                //        item.Behaviour.LeftMouseClick.RedirectAddress = "";
                //    }
                //}
                return editedRequest;
            }
            else return null;
        }

        #endregion
    }
}
