using Kumi.Domain.Configs;

namespace Kumi.Core.Configs.Interfaces;

public interface IConfigQueryActions
{
    public Task<Config?> FindToolFor(string user);
}
