using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IConfigDataRepository
    {
        public Task<Config> GetConfigByKey(string key, string systemCode);
        public Task<IEnumerable<Config>> GetAllConfigs();
    }
}
