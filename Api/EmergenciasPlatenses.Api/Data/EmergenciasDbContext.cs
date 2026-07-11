using EmergenciasPlatenses.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmergenciasPlatenses.Api.Data;

public class EmergenciasDbContext(
    DbContextOptions<EmergenciasDbContext> options) : DbContext(options)
{
    public DbSet<Ciudad> Ciudades => Set<Ciudad>();

    public DbSet<Categoria> Categorias => Set<Categoria>();

    public DbSet<Comercio> Comercios => Set<Comercio>();

    public DbSet<Direccion> Direcciones => Set<Direccion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(EmergenciasDbContext).Assembly);
    }
}
