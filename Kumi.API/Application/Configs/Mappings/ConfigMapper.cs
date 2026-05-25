using Kumi.API.Application.Configs.Dtos;
using Kumi.Domain.Configs;

namespace Kumi.API.Application.Configs.Mappings;

public class ConfigMapper
{
    public ConfigDto ToDto(Config config)
    {
        Enum.TryParse(config.Type.ToString(), out LanguageModelProviderTypeDto typeDto);
        return new ConfigDto
        {
            For = config.For,
            Type = typeDto,
            Model = config.Model,
            ApiKey = config.ApiKey 
        };
    }

    public Config ToEntity(ConfigDto configDto)
    {
        Enum.TryParse(configDto.Type.ToString(), out LanguageModelProviderType type);
        return new Config
        {
            Type = type,
            Model = configDto.Model,
            ApiKey = configDto.ApiKey
        };
    }

}