using System;
using Kumi.API.Application.Dtos;
using Kumi.Domain.Tools;

namespace Kumi.API.Application.Mappings;

public class ParameterMapper
{
    public ParameterDto ToDto(Parameter entity)
    {
       return new ParameterDto
       {
            Type = entity.Type,
            Description = entity.Description,
            Required = entity.Required
       };
    }

    public Parameter ToEntity(ParameterDto dto)
    {
        return new Parameter
        {
            Type = dto.Type,
            Description = dto.Description,
            Required = dto.Required
        };
    }

}
