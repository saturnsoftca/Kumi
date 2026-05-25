namespace Kumi.API.Application.Configs.Dtos;

public class ConfigDto
{
    public string? For { get; set; }
    public required LanguageModelProviderTypeDto Type { get; set; }
    public required string Model { get; set; }
    public string? ApiKey { get; set; }
}
