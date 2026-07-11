using EmergenciasPlatenses.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergenciasPlatenses.Api.Data.Configurations;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("Ciudades");

        builder.HasKey(ciudad => ciudad.Id);

        builder.Property(ciudad => ciudad.Id)
            .ValueGeneratedOnAdd();

        builder.Property(ciudad => ciudad.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ciudad => ciudad.Provincia)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ciudad => ciudad.Pais)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ciudad => ciudad.Activa)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(ciudad => new
            {
                ciudad.Nombre,
                ciudad.Provincia,
                ciudad.Pais
            })
            .IsUnique();
    }
}
