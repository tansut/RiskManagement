using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalitte.RiskManagement.Framework.Utility
{
    public class EntityMetadata
    {
        public string PropertyName { get; set; }
        public string Description { get; set; }

        public EntityMetadata(string propertyName, string description)
        {
            this.PropertyName = propertyName;
            this.Description = description;
        }

        internal static List<EntityMetadata> FromTypeUsingGrid(Type type, Ext.Net.GridPanel grid)
        {
            var result = new List<EntityMetadata>();
            var properties = type.GetProperties().Where(p=>p.PropertyType.IsArray == false);
            foreach (var prop in properties)
            {
                if (!canExport(prop.PropertyType))
                    continue;
                var entityMetadata = new EntityMetadata(prop.Name, prop.Name);
                if (grid != null)
                {
                    var column = grid.ColumnModel.Columns.FirstOrDefault(p => p.DataIndex == prop.Name);
                    if (column != null)
                        entityMetadata.Description = column.Header;
                    result.Add(entityMetadata);
                }else
                    result.Add(entityMetadata);
            }
            return result;
        }

        private static bool canExport(Type type)
        {
            if (type == typeof(string))
                return true;
            else return !type.IsClass && type.Name != "EntityState";
        }
    }
}
