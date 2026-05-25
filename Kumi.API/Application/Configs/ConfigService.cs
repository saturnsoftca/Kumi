using Kumi.API.Application.Configs.Dtos;
using Kumi.API.Application.Configs.Mappings;
using Kumi.Core.Configs.Interfaces;
using Kumi.Domain.Configs;

namespace Kumi.API.Application.Configs
{
    public class ConfigService(IConfigQueryActions configQueryActions,
                               IConfigCommandActions configCommandActions,
                               ConfigMapper configMapper)
    {
        public async Task<Result<ConfigDto?>> GetConfigFor(string search)
        {
            Config? config = await configQueryActions.FindConfigFor(search); 
            if (config != null)
            {
                return Result<ConfigDto>.Success(configMapper.ToDto(config))!;
            }
            return Result<ConfigDto>.Failure(404)!;
        }

        public async Task<Result<ConfigDto>> CreateOrUpdateConfig(ConfigDto configDto)
        {
            Config? config = await configQueryActions.FindConfigFor(configDto.For!); 
            
            if (config != null)
            {
                config = await configCommandActions.UpdateConfig(configMapper.ToEntity(configDto));
                return Result<ConfigDto>.Success(configMapper.ToDto(config));
            }
        
            config = await configCommandActions.AddConfig(configMapper.ToEntity(configDto));
            return Result<ConfigDto>.Success(configMapper.ToDto(config));

        }

    }
}