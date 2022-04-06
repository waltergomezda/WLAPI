using System;

namespace Infraestructure.DataManager
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataFieldMappingAttribute : Attribute
    {
        public string MappedField { get; private set; }
        public DataFieldMappingAttribute(string fieldName)
        {
            MappedField = fieldName;
        }
    }
}