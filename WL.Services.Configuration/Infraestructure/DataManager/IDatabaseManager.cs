using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.DataManager
{
    public interface IDatabaseManager : IDisposable
    {
        IDatabaseManager OpenConnection();
        void ExecuteNonQuery(string procedureName, IEnumerable<DbParameter> parameters);
        Task<object> Execute(string commandString, ExecutionType executionType, IEnumerable<DbParameter> parameters);
        Task<IEnumerable<TEntity>> ExecuteList<TEntity>(string procedureName, IEnumerable<DbParameter> parameters) where TEntity : new();
    }
}
