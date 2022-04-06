using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Infraestructure.DataManager.Exceptions
{ 
    public class DatabaseExecutionException : DbException
    {
        public string StoredProcedureName { get; set; }

        public DatabaseExecutionException(string storedProcedureName, Exception innerException)
            : base($"Error trying to execute the stored procedure '{storedProcedureName}'", innerException)
        {
            StoredProcedureName = storedProcedureName;
        }
    }
}