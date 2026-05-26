using Kumi.API.Apis;
using Kumi.API.Application.Configs;
using Kumi.API.Application.Configs.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Kumi.API.Apis
{
    public class ConfigController(ConfigService configService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ConfigDto>> GetSystemConfig(string search)
        {
            return HandleResult(await configService.GetConfigFor(search));
        }

        [HttpPost]
        public async Task<ActionResult<ConfigDto>> CreateOrUpdateConfig([FromBody] ConfigDto configDto)
        {
            return HandleResult(await configService.CreateOrUpdateConfig(configDto));
        }

    }
}