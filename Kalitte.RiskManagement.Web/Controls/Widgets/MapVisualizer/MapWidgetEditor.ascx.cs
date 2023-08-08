using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;
using Kalitte.Dashboard.Framework;
using MapEntities;
using System.Xml.Serialization;
using System.IO;

namespace Kalitte.WidgetLibrary.MapVisualizer
{
    public partial class MapWidgetEditor : System.Web.UI.UserControl, IWidgetEditor
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public void Edit(object instanceKey)
        {
            WidgetInstance instance = DashboardFramework.GetWidgetInstance(instanceKey);
            ViewState["instance"] = instanceKey;
            if (instance.SerializedData == null)
            {
                var entity = MapDataRequest.CreateEmpty();
                entity.Id = (Guid)instance.InstanceKey;
                ctlMapEditor.Edit(entity);
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MapDataRequest));
                StringReader reader = new StringReader((string)instance.SerializedData);

                MapDataRequest editedRequest = (MapDataRequest)serializer.Deserialize(reader);
                ctlMapEditor.Edit(editedRequest);
            }
        }

        public bool EndEdit(Dictionary<string, object> arguments)
        {
            MapDataRequest editedRequest = ctlMapEditor.EndEdit();
            if (editedRequest == null)
                return false;
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MapDataRequest));
                StringWriter writer = new StringWriter();

                serializer.Serialize(writer, editedRequest);

                WidgetInstance instance = DashboardFramework.GetWidgetInstance(ViewState["instance"]);

                instance.SerializedData = writer.ToString();
                DashboardFramework.UpdateWidget(instance);
                return true;
            }


        }
    }
}