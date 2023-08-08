using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace Kalitte.RiskManagement.Framework.Core
{
    [Serializable]
    public class DynamicEntity : ICustomTypeDescriptor
    {

        private Dictionary<string, Type> columns;
        private Dictionary<string, object> values;

        public DynamicEntity(object obj)
        {
            Dictionary<string, Type> columns = new Dictionary<string, Type>();
            ArrayList array = new ArrayList();
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                columns.Add(prop.Name, prop.PropertyType);
                array.Add(prop.GetValue(obj, null));
            }
            constructEntity(columns, array.ToArray());
        }

        private void constructEntity(Dictionary<string, Type> columns, params object[] values)
        {
            this.columns = columns;
            this.values = new Dictionary<string, object>();
            int i = 0;
            foreach (var item in columns)
            {
                this.values.Add(item.Key, values[i++]);
            }
        }

        public DynamicEntity(Dictionary<string, Type> columns, params object [] values)
        {
            constructEntity(columns, values);
        }

        #region ICustomTypeDescriptor Members

        public AttributeCollection GetAttributes()
        {
            return AttributeCollection.Empty;
        }

        public string GetClassName()
        {
            return this.GetType().Name;
        }

        public string GetComponentName()
        {
            return string.Empty;
        }

        public TypeConverter GetConverter()
        {
            return new TypeConverter();
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return EventDescriptorCollection.Empty;
        }

        public EventDescriptorCollection GetEvents()
        {
            return EventDescriptorCollection.Empty;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
           return this.GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            List<PropertyDescriptor> properties = new List<PropertyDescriptor>();

            foreach (var col in columns)
            {
                PropertyDescriptor pdesc = new DynamicEntityPropertyDescriptor(col.Key, col.Value);
                properties.Add(pdesc);
            }

            return new PropertyDescriptorCollection(properties.ToArray());
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        internal object GetValue(string name)
        {
            return values[name];
        }
    }
}
