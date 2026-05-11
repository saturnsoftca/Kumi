using Kumi.Domain.Tools;
using Kumi.Domain.Configs;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Kumi.Persistence;

public class KumiDbContext : DbContext
{

    public DbSet<Tool> Tools { get; set; }
    public DbSet<Config> Configs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: change this
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(
            "Host=localhost;Port=8973;Database=kumi;Username=kumi;Password=KumiDb123"
        );

        dataSourceBuilder.EnableDynamicJson();

        var dataSource = dataSourceBuilder.Build();

        optionsBuilder.UseNpgsql(dataSource);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tool>()
            .Property(x => x.Parameters)
            .HasColumnType("jsonb");
        
        modelBuilder.Entity<Tool>()
            .Property(x => x.Method)
            .HasConversion<string>();

        modelBuilder.Entity<Config>()
            .HasKey(x => x.For);

        modelBuilder.Entity<Config>()
            .Property(x => x.Type)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }

}
