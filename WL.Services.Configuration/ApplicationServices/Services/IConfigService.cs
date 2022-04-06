using ApplicationServices.DataTransferObjects.InputModel;
using ApplicationServices.DataTransferObjects.OutputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Services
{
    public interface IConfigService
    {
        Task<IEnumerable<ConfigOutputModel>> GetAllConfigs();
        Task<ConfigOutputModel> GetConfigByKey(string key, string systemCode);
    }
}
