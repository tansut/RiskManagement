using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Kalitte.RiskManagement.Framework.Core
{
    public class DynamicEntityList : List<DynamicEntity>, ITypedList
    {
        #region ITypedList Members

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            PropertyDescriptorCollection list = null;
            foreach (var e in this)
            {
                PropertyDescriptorCollection col = TypeDescriptor.GetProperties(e);
                list = col;
                break;
            }
            return list;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return "DynamicEntityList";
        }

        #endregion
    }
}
