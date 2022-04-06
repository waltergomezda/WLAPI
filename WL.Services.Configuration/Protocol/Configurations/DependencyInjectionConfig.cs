using ApplicationServices.Services;
using Domain.Contracts;
using Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IConfigDataRepository, DapperConfigDataRepository>();
        }
    }
}
