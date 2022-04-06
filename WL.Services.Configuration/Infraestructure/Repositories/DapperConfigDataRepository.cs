using AutoMapper;
using Dapper;
using Domain;
using Domain.Contracts;
using Infraestructure.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class DapperConfigDataRepository : IConfigDataRepository
    {
        const string QUERY_ALL_CONFIG = @"select [key], systemcode, [value] from [dbo].[ConfigData]";
        const string QUERY_CONFIG_BY_KEY = @"select [key], systemcode, [value] from [dbo].[ConfigData] where SystemCode = @systemCode and [key] = @key";
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        public DapperConfigDataRepository(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString("ConfigConnectionString");
            _mapper = mapper;
        }
        public async Task<IEnumerable<Config>> GetAllConfigs()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var configs = await connection.QueryAsync<ConfigDataDTO>(QUERY_ALL_CONFIG, null);
                return _mapper.Map<IEnumerable<ConfigDataDTO>, IEnumerable<Config>>(configs);
            }
        }

        public async Task<Config> GetConfigByKey(string key, string systemCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var configs = await connection.QueryAsync<ConfigDataDTO>(QUERY_CONFIG_BY_KEY, new { SystemCode = systemCode, Key = key });
                return _mapper.Map<ConfigDataDTO, Config>(configs.FirstOrDefault());
            }
        }
    }
}
