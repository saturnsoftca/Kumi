using Kumi.Domain.Configs;

namespace Kumi.Core.Configs.Interfaces;

public interface IConfigCommandActions
{
    public Task<Config> AddConfig(Config config);
    public Task<Config> UpdateConfig(Config config);
}
