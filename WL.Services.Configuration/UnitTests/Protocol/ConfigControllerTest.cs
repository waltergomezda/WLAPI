using ApplicationServices.DataTransferObjects.OutputModel;
using ApplicationServices.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Protocol.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace UnitTests.Protocol
{
    public class ConfigControllerTest
    {
        [Fact]
        public async Task GetConfig_ShouldReturn200_WhenConfigExist()
        {
            var key1 = "key1";
            var system1 = "system1";
            var value1 = "value1";
            var expectedResult = new ConfigOutputModel { Key = key1, SystemCode = system1, Value = value1 };

            IConfigService configServiceMock = Substitute.For<IConfigService>();
            configServiceMock.GetConfigByKey(key1, system1).Returns(Task.FromResult(expectedResult));
            
            var subjectUnderTest = new ConfigController(configServiceMock);
            var result = await subjectUnderTest.GetConfig(key1, system1) as OkObjectResult;
            Assert.IsType<ConfigOutputModel>(result.Value);
            Assert.Equal(200, result.StatusCode);
            var config = result.Value as ConfigOutputModel;
            Assert.Equal(value1, config.Value );
        }

        [Fact]
        public async Task GetConfig_ShouldReturn404_WhenConfigExist()
        {
            IConfigService configService = Substitute.For<IConfigService>();
            var key1 = "key1";
            var system1 = "system1";
            var value1 = "value1";
            var key2 = "key2";
            var expectedResult = new ConfigOutputModel { Key = key1, SystemCode = system1, Value = value1 };
            configService.GetConfigByKey(key2, system1)
                .Returns(Task.FromResult(expectedResult));

            var subjectUnderTest = new ConfigController(configService);
            var result = await subjectUnderTest.GetConfig(key1, system1) as NotFoundResult;
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetConfigs_ShouldReturn200_WhenConfigsExist()
        {
            IConfigService configService = Substitute.For<IConfigService>();
            IEnumerable<ConfigOutputModel> expectedResult = new List<ConfigOutputModel>{ new ConfigOutputModel { Key = "key1", SystemCode = "system1", Value = "value1" },
                                                              new ConfigOutputModel { Key = "key2", SystemCode = "system2", Value = "value2" }};
            configService.GetAllConfigs().Returns(Task.FromResult(expectedResult));

            var subjectUnderTest = new ConfigController(configService);
            var result = await subjectUnderTest.GetConfigs() as OkObjectResult;
            Assert.IsType<List<ConfigOutputModel>>(result.Value) ;
            Assert.Equal(200, result.StatusCode);
            var configList =  result.Value as List<ConfigOutputModel> ;
            Assert.Equal(2, configList.Count);
        }

    }
}
