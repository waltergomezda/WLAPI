using Infraestructure.DataManager.Exceptions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructure.DataManager
{
    public class DatabaseManagerSqlServer : IDatabaseManager
    {
        private readonly string _connectionString;
        private SqlCommand _command;
        private SqlConnection _connection;
        private const int COMMAND_TIMEOUT = 30; 

        public DatabaseManagerSqlServer(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public IDatabaseManager OpenConnection()
        {
            this._connection = new SqlConnection(_connectionString);
            return this;
        }
        public ICollection<DbParameter> OutputParameters { get; set; }
        public async Task<object> Execute(string commandString, ExecutionType executionType, IEnumerable<DbParameter> parameters)
        {
            return await Execute(commandString, executionType, parameters, COMMAND_TIMEOUT);
        }

        private async Task<object> Execute(string commandString, ExecutionType executionType, IEnumerable<DbParameter> parameters, int timeout)
        {
            object returnObject = null;

            if (_connection == null) return null;
            _command = new SqlCommand(commandString, _connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = timeout
            };

            var dbParameters = parameters as DbParameter[] ?? parameters.ToArray();

            if (dbParameters.Any())
            {
                _command.Parameters.Clear();

                foreach (var dbParameter in dbParameters)
                {
                    _command.Parameters.AddWithValue(dbParameter.Name, dbParameter.Value);
                    _command.Parameters[dbParameter.Name].Direction = dbParameter.Direction;

                    if (dbParameter.Size > 0)
                    {
                        _command.Parameters[dbParameter.Name].Size = dbParameter.Size;
                    }
                }
            }
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            try
            {
                switch (executionType)
                {
                    case ExecutionType.Procedure:
                        returnObject = await _command.ExecuteReaderAsync();
                        break;
                    case ExecutionType.Query:
                        _command.CommandType = CommandType.Text;
                        returnObject = await _command.ExecuteReaderAsync();
                        break;
                    case ExecutionType.NonQuery:
                        returnObject = await _command.ExecuteNonQueryAsync();
                        break;
                    case ExecutionType.Scalar:
                        _command.CommandType = CommandType.Text;
                        returnObject = await _command.ExecuteScalarAsync();
                        break;
                }
            }
            catch(SqlException ex)
            {
                throw new DatabaseExecutionException(commandString, ex);
            }
            return returnObject;
        }

        public async Task<IEnumerable<TEntity>> ExecuteList<TEntity>(string procedureName, IEnumerable<DbParameter> parameters) where TEntity : new()
        {
            try
            {
                return await ExecuteList(procedureName, parameters, new DefaultReaderEntityMapper<TEntity>());
            }
            catch(DatabaseMappingException ex)
            {
                throw new DatabaseMappingException(ex, procedureName);
            }
        }

        private async Task<IEnumerable<TEntity>> ExecuteList<TEntity>(string procedureName, IEnumerable<DbParameter> parameters, DefaultReaderEntityMapper<TEntity> dataReaderMapper) where TEntity : new()
        {
            return await ExecuteList(procedureName, ExecutionType.Procedure, parameters, dataReaderMapper);
        }

        private async Task<IEnumerable<TEntity>> ExecuteList<TEntity>(string commandString, ExecutionType executionType, IEnumerable<DbParameter> parameters, DefaultReaderEntityMapper<TEntity> dataReaderMapper) where TEntity : new()
        {
            var reportDataItems = new List<TEntity>();
            var reader = (IDataReader)(await Execute(commandString, executionType, parameters));
            while (reader.Read())
            {
                var itemReport = dataReaderMapper.MapToEntity(reader);
                reportDataItems.Add(itemReport);
            }
            reader.Close();
            UpdateOutputParameters();
            return reportDataItems;
        }

        public async void ExecuteNonQuery(string procedureName, IEnumerable<DbParameter> parameters)
        {
            await Execute(procedureName, ExecutionType.NonQuery, parameters);
            UpdateOutputParameters();
        }
        private void UpdateOutputParameters()
        {
            if (_command.Parameters.Count <= 0) return;
            OutputParameters = new List<DbParameter>();

            for (var i = 0; i < _command.Parameters.Count; i++)
            {
                var parameter = _command.Parameters[i];
                if ( parameter.Direction == ParameterDirection.Output 
                    || parameter.Direction == ParameterDirection.InputOutput 
                    || parameter.Direction == ParameterDirection.ReturnValue)
                {
                    OutputParameters.Add(new DbParameter(parameter.ParameterName, parameter.Direction, parameter.Value));
                }
            }
        }

        public void Dispose()
        {
            if(_command != null)
            {
                _command.Dispose();
                _command = null;
            }
            if(_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
