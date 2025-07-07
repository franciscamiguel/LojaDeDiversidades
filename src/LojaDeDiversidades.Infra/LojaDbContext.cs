using Microsoft.EntityFrameworkCore;

namespace LojaDeDiversidades.Infra;

public class LojaDbContext(DbContextOptions<LojaDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LojaDbContext).Assembly);
    }
}