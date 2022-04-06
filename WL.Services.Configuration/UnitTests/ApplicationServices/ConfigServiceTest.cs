using ApplicationServices.DataTransferObjects.OutputModel;
using ApplicationServices.Mapping;
using ApplicationServices.Services;
using AutoMapper;
using Domain;
using Domain.Contracts;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationServices
{
    public class ConfigServiceTest
    {
        public IMapper GetMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewMappingProfile());
            });
            return mockMapper.CreateMapper();
        }
        [Fact]
        public async Task GetConfigByKey_ShouldReturnOneConfiguration_WhenConfigurationExist()
        {
            var configDataRepositoryMock = Substitute.For<IConfigDataRepository>();
            var responseRepository = new Config { Key = "key1", SystemCode = "system1", Value = "value1" };
            var expectedResult = new ConfigOutputModel { Key = "key1", SystemCode = "system1", Value = "value1" };
            configDataRepositoryMock.GetConfigByKey("key1", "system1").Returns(Task.FromResult(responseRepository));
            
            var subjectUnderTest = new ConfigService(configDataRepositoryMock, GetMapper());

            var result = await subjectUnderTest.GetConfigByKey("key1", "system1");
            Assert.IsType<ConfigOutputModel>(result);
            Assert.Equal(expectedResult.Value, result.Value);
        }

        [Fact]
        public async Task GetAllConfigs_ShouldReturnConfigurationList_WhenThereAreConfigs()
        {
            var configDataRepositoryMock = Substitute.For<IConfigDataRepository>();
            IEnumerable<Config> responseRepository = new List<Config> { new Config { Key = "key1", SystemCode = "system1", Value = "value1" },
                                                                        new Config { Key = "key2", SystemCode = "system2", Value = "value2" }};
            configDataRepositoryMock.GetAllConfigs().Returns(Task.FromResult(responseRepository));

            var subjectUnderTest = new ConfigService(configDataRepositoryMock, GetMapper());

            var result = await subjectUnderTest.GetAllConfigs();
            Assert.IsType<List<ConfigOutputModel>>(result);
            var configs = result as List<ConfigOutputModel>;
            Assert.Equal(2, configs.Count );
        }

    }
}
