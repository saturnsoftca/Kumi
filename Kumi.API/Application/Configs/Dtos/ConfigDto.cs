using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kumi.API.Application.Configs.Dtos;

public class ConfigDto
{
    public string? For { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required LanguageModelProviderTypeDto Type { get; set; }
    public required string Model { get; set; }
    public string? ApiKey { get; set; }
}
