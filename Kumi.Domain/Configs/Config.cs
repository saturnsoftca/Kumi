namespace Kumi.Domain.Configs;

public class Config
{
    public string For { get; set; } = "SYSTEM";
    public required LanguageModelProviderType Type { get; set; }
    public required string Model { get; set; }
    public string ApiKey { get; set; } = null!;
}
