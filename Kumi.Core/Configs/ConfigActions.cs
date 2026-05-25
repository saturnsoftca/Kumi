using Kumi.Core.Configs.Interfaces;
using Kumi.Domain.Configs.Interfaces;
using Kumi.Domain.Configs;

namespace Kumi.Core.Configs;

public class ConfigActions(IConfigRepository repository) : IConfigQueryActions, IConfigCommandActions
{
    public async Task<Config?> FindConfigFor(string user)
    {
        return await repository.FindFor(user); 
    }

    public async Task<Config> AddConfig(Config config)
    {
        return await repository.AddAsync(config);
    }

    public async Task<Config> UpdateConfig(Config config)
    {
        return await repository.UpdateAsync(config);
    }
}
