using Infraestructure.DataManager.Exceptions;
using System;
using System.Data;
using System.Reflection;

namespace Infraestructure.DataManager
{
    internal class DefaultReaderEntityMapper<TEntity> : IDataReaderEntityMapper<TEntity> where TEntity : new()
    {
        public DefaultReaderEntityMapper()
        {
        }

        public TEntity MapToEntity(IDataReader reader)
        {
            var recordEntity = new TEntity();
            var entityType = typeof(TEntity);

            var properties = entityType.GetProperties();
            foreach( var property in properties)
            {
                var attribute = property.GetCustomAttribute<DataFieldMappingAttribute>();
                object originalValue = null;
                try
                {
                    if(attribute != null)
                    {
                        originalValue = reader[attribute.MappedField];
                        var realValue = ChangeType(originalValue, property.PropertyType);
                        property.SetValue(recordEntity, realValue, null);
                    }
                }
                catch
                {
                    throw new DatabaseMappingException(originalValue?.GetType().ToString(), attribute.MappedField, typeof(TEntity).Name, property.GetType().Name, property.PropertyType.Name);
                }
            }
            return recordEntity;
        }

        private static object ChangeType(object value, Type conversion)
        {
            if(!conversion.IsGenericType || conversion.GetGenericTypeDefinition() != typeof(Nullable<>))
            {
                return Convert.ChangeType(value, conversion);
            }
            if (value is DBNull)
                return null;

            conversion = Nullable.GetUnderlyingType(conversion);
            return Convert.ChangeType(value, conversion);
        }
    }
}