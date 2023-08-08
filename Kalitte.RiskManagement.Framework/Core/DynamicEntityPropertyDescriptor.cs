using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kalitte.RiskManagement.Framework.Core
{
    public class DynamicEntityPropertyDescriptor: PropertyDescriptor
    {
        private string propertyName;
        private Type propertyType;

        public DynamicEntityPropertyDescriptor(string propName, Type type): base(propName, new Attribute [] {})
        {
            this.propertyType = type;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(DynamicEntity); }
        }

        public override object GetValue(object component)
        {
            DynamicEntity entity = component as DynamicEntity;
            return entity.GetValue(Name);
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override Type PropertyType
        {
            get { return this.propertyType; }
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
