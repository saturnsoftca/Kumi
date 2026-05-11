namespace Kumi.Domain.Configs.Interfaces;

public interface IConfigRepository
{
    Task<Config?> FindFor(string user);
    Task<Config> AddAsync(Config config);
    Task<Config> UpdateAsync(Config config);
}
