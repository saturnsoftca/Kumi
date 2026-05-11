using Kumi.Core.Tools;
using Kumi.Core.Tools.Interfaces;
using Kumi.Core.Chats;
using Kumi.Core.Configs.Interfaces;
using Kumi.Core.Configs;
using Kumi.Domain.Tools.Interfaces;
using Kumi.Domain.Configs.Interfaces;
using Kumi.LLM.Integrations.Ollama;
using Kumi.LLM.Integrations.Gemini;
using Kumi.LLM.Interfaces;
using Kumi.Persistence;
using Kumi.Persistence.Tools;
using Kumi.Persistence.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace Kumi.Core;

public static class KumiServiceExtension
{
    public static IServiceCollection AddKumiRuntime(this IServiceCollection services)
    {
        services.AddDbContext<KumiDbContext>();
        services.AddHostedService<KumiRuntime>();

        //services.AddScoped<ILanguageModel>(model => new Ollama("gemma4:26b"));
        //services.AddScoped<ILanguageModel>(model => new Ollama("qwen3.5"));
        //services.AddScoped<ILanguageModel>(model => new Gemini("gemini-2.5-flash", "AIzaSyDRkAPAikvOeGN0ybV_EEKtwcxMRVX6pUo"));

        services.AddScoped<IToolRepository, ToolRepository>();
        services.AddScoped<IConfigRepository, ConfigRepository>();

        services.AddScoped<IToolQueryActions, ToolActions>();
        services.AddScoped<IToolCommandActions, ToolActions>();

        services.AddScoped<IConfigQueryActions, ConfigActions>();
        services.AddScoped<IConfigCommandActions, ConfigActions>();

        services.AddScoped<Chat>();
        //services.AddScoped<MessageHistory>();

        return services;
    }
}
