namespace Kumi.API.Application.Dtos;

public class ConfigDto
{
    public required string Type { get; set; }
    public required string Model { get; set; }
    public string? ApiKey { get; set; }
}
