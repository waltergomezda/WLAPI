using ApplicationServices.DataTransferObjects.InputModel;
using ApplicationServices.DataTransferObjects.OutputModel;
using AutoMapper;
using Domain;
using Domain.Contracts;
using Infraestructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IMapper _mapper;
        private readonly IConfigDataRepository _configDataRepository;
        public ConfigService(IConfigDataRepository configDataRepository, IMapper mapper)
        {
            this._configDataRepository = configDataRepository;
            this._mapper = mapper;
        }
        public async Task<ConfigOutputModel> GetConfigByKey(string key, string systemCode)
        {
            var config = await _configDataRepository.GetConfigByKey(key, systemCode);
            return _mapper.Map<Config, ConfigOutputModel>(config);
        }
        public async Task<IEnumerable<ConfigOutputModel>> GetAllConfigs()
        {
            var configs = await _configDataRepository.GetAllConfigs();
            return _mapper.Map<IEnumerable<Config>, IEnumerable<ConfigOutputModel>>(configs);
        }
    }
} 
