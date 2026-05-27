using Kumi.API.Application.Configs.Dtos;
using Kumi.Domain.Configs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    public Config ToExistingEntity(Config config, ConfigDto configDto)
    {

        Enum.TryParse(configDto.Type.ToString(), out LanguageModelProviderType type);
        config.For = configDto.For!;
        config.Type = type;
        config.Model = configDto.Model;
        config.ApiKey = configDto.ApiKey is null ? "" : configDto.ApiKey;
        return config;
    }

}