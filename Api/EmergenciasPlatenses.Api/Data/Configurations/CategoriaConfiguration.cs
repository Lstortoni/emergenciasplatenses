using EmergenciasPlatenses.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergenciasPlatenses.Api.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(categoria => categoria.Id);

        builder.Property(categoria => categoria.Id)
            .ValueGeneratedOnAdd();

        builder.Property(categoria => categoria.Nombre)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(categoria => categoria.Descripcion)
            .HasMaxLength(300);

        builder.Property(categoria => categoria.Icono)
            .HasMaxLength(100);

        builder.Property(categoria => categoria.Activa)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(categoria => categoria.Nombre)
            .IsUnique();
    }
}
