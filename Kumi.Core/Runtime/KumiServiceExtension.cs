using Kumi.Core.Tools;
using Kumi.Core.Tools.Interfaces;
using Kumi.Core.Chats;
using Kumi.Domain.Tools.Interfaces;
using Kumi.LLM.Integrations.Ollama;
using Kumi.LLM.Integrations.Gemini;
using Kumi.LLM.Interfaces;
using Kumi.Persistence;
using Kumi.Persistence.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Kumi.Core;

public static class KumiServiceExtension
{
    public static IServiceCollection AddKumiRuntime(this IServiceCollection services)
    {
        services.AddDbContext<KumiDbContext>();
        services.AddHostedService<KumiRuntime>();

        services.AddScoped<IToolRepository, ToolRepository>();
        //services.AddScoped<ILanguageModel>(model => new Ollama("gemma4:26b"));
        //services.AddScoped<ILanguageModel>(model => new Ollama("qwen3.5"));
        services.AddScoped<ILanguageModel,Gemini>();

        services.AddScoped<IToolQueryActions, ToolActions>();
        services.AddScoped<IToolCommandActions, ToolActions>();

        services.AddScoped<Chat>();
        //services.AddScoped<MessageHistory>();

        return services;
    }
}
