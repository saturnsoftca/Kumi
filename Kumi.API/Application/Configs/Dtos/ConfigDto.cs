namespace Kumi.API.Application.Configs.Dtos;

public class ConfigDto
{
    public required string Type { get; set; }
    public required string Model { get; set; }
    public string? ApiKey { get; set; }
}
