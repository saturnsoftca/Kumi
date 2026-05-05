using System;
using Kumi.API.Application.Dtos;
using Kumi.Domain.Tools;

namespace Kumi.API.Application.Mappings;

public class ToolMapper(ParameterMapper parameterMapper)
{
  
    public ToolDto ToDto(Tool entity) 
    {
        return new ToolDto 
        {
            ToolId = entity.ToolId,
            Url = entity.Url,
            Method = entity.Method.ToString(),
            Name = entity.Name,
            Description = entity.Description,
            Parameters = entity.Parameters.ToDictionary(
                x => x.Key,
                x => parameterMapper.ToDto(x.Value)
            )
        };
    }

    public List<ToolDto> ToDtoList(List<Tool> entities) 
    {
        return entities
            .Select(entity => ToDto(entity))
            .ToList();
    }

    public Tool ToEntity(ToolDto dto)
    {
        return Tool.NewInstance(
            dto.Url,
            Enum.Parse<Method>(dto.Method),
            dto.Name,
            dto.Description,
            dto.Parameters.ToDictionary(
                x => x.Key,
                x => parameterMapper.ToEntity(x.Value)
            )
        );
    }


}
