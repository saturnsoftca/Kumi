using System;

namespace Kumi.API.Application.Tools.Dtos;

public class ToolDto
{
    public Guid ToolId { get; set; }
    public required string Url { get; set; }
    public required string Method { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Dictionary<string, ParameterDto> Parameters { get; set; }
}
