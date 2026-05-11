using Kumi.Domain.Configs.Interfaces;
using Kumi.Domain.Configs;
using Microsoft.EntityFrameworkCore;

namespace Kumi.Persistence.Configs;

public class ConfigRepository(KumiDbContext kumiDbContext) : IConfigRepository
{
    public async Task<Config?> FindFor(string user)
    {
        return await kumiDbContext.Set<Config>()
            .FirstOrDefaultAsync(config => config.For == user);
    }

    public async Task<Config> AddAsync(Config config)
    {
        kumiDbContext.Set<Config>().Add(config);
        await kumiDbContext.SaveChangesAsync();
        return config;
    }

    public async Task<Config> UpdateAsync(Config config)
    {
        kumiDbContext.Update(config);
        await kumiDbContext.SaveChangesAsync();
        return config;
    }
}
