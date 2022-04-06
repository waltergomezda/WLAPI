using System;
using System.Runtime.Serialization;

namespace Infraestructure.DataManager.Exceptions
{
    public class DatabaseMappingException : Exception
    {
        public string DatabaseDataType { get; set; }
        public string ColumnName { get; set; }
        public string ObjectClassName { get; set; }
        public string ObjectDataType { get; set; }
        public string ObjectPropertyName { get; set; }
        public string StoredProcedureName { get; set; }

        public DatabaseMappingException(string message) 
            : base(message)
        {
        }

        public DatabaseMappingException(DatabaseMappingException parameterException, string storedProcedureName)
            :base(AddStoredProcedureNameToMessage(parameterException.Message, storedProcedureName))
        {
            DatabaseDataType = parameterException.DatabaseDataType;
            ColumnName = parameterException.ColumnName;
            ObjectClassName = parameterException.ObjectClassName;
            ObjectDataType = parameterException.ObjectDataType;
            ObjectPropertyName = parameterException.ObjectPropertyName;
            StoredProcedureName = storedProcedureName;
        }

        public DatabaseMappingException(string databaseDataType, string columnName, string objectClassName, string objectDataType, string objectPropertyName)
            :this(databaseDataType, columnName, objectClassName, objectDataType, objectPropertyName, null)
        { 
        }
        public DatabaseMappingException(string databaseDataType, string columnName, string objectClassName, string objectDataType, string objectPropertyName, string storedProcedure)
            :base(GetMessage(databaseDataType, columnName, objectClassName, objectDataType, objectPropertyName, storedProcedure))
        {
            
            DatabaseDataType = databaseDataType;
            ColumnName = columnName;
            ObjectClassName = objectClassName;
            ObjectDataType = objectDataType;
            ObjectPropertyName = objectPropertyName;
        }

        private static string AddStoredProcedureNameToMessage(string message, string storedProcedureName)
        {
            return message.Replace("##STOREDPROCEDURE##", storedProcedureName);
        }

        private static string GetMessage(string databaseDataType, string columnName, string objectClassName, string objectDataType, string objectPropertyName, string storedProcedure)
        {
            return $"Mapping error calling the stored procedure: '{storedProcedure ?? "##STOREDPROCEDURE##"}' mapping column '{columnName}' with data type '{databaseDataType}' to a class object '{objectClassName}' to the property '{objectPropertyName}' with data type '{objectDataType}'"; 
        }
    }
}