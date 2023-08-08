using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Kalitte.Dashboard.Framework.Types;
using System.Xml.Serialization;
using MapEntities;
using System.IO;
using Kalitte.Dashboard.Framework;
using System.Threading;

namespace Kalitte.WidgetLibrary.MapVisualizer
{
    public partial class MapWidget : System.Web.UI.UserControl, IWidgetControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region IWidgetControl Members

        public void Bind(WidgetInstance instance)
        {
            if (instance.SerializedData != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MapDataRequest));
                StringReader reader = new StringReader((string)instance.SerializedData);

                MapDataRequest editedRequest = (MapDataRequest)serializer.Deserialize(reader);


                ctlMap.Bind(editedRequest);
            }
        }

        public UpdatePanel[] Command(WidgetInstance instance, Kalitte.Dashboard.Framework.WidgetCommandInfo commandData, ref UpdateMode updateMode)
        {
            switch (commandData.CommandType)
            {
                case WidgetCommandType.Refresh:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { UpdatePanel1 };
                    }
                case WidgetCommandType.SettingsChanged:
                    {
                        Bind(instance);
                        return new UpdatePanel[] { UpdatePanel1 };
                    }
                default: return null;
            }
            
        }

        public void InitControl(Kalitte.Dashboard.Framework.WidgetInitParameters parameters)
        {

        }

        #endregion
    }
}