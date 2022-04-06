using ApplicationServices.DataTransferObjects.InputModel;
using ApplicationServices.DataTransferObjects.OutputModel;
using ApplicationServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;
        public ConfigController(IConfigService configService)
        {
            this._configService = configService;
        }

        /// <summary>
        /// Get a Hello World Message.
        /// </summary>
        /// <returns>Returns Hello World Message</returns>
        /// <response code="200">Greetings returned</response>
        [HttpGet("api/greetings")]
        public async Task<ActionResult> GetGreetings()
        {
            return Ok("Hello World");
        }

        /// <summary>
        /// Get a list of registered configs.
        /// </summary>
        /// <returns>Returns a List of config items</returns>
        /// <response code="200">Configs returned</response>
        [HttpGet("api/config")]
        public async Task<ActionResult> GetConfigs()
        {
            var response = await _configService.GetAllConfigs();
            return Ok(response);
        }


        /// <summary>
        /// Gets a config item filter by key and systemCode.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="systemCode"></param>
        /// <returns>A list of configs</returns>
        /// <response code="200">Config item returned</response>
        /// <response code="404">Config item not found</response>
        [HttpGet("api/config/{systemCode}/{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetConfig(string key, string systemCode)
        {
            var response = await _configService.GetConfigByKey(key, systemCode);
            if (response == null) return NotFound();
            return Ok(response);
        }

    }
}
