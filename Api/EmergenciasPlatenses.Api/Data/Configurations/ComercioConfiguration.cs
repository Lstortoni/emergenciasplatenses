using EmergenciasPlatenses.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergenciasPlatenses.Api.Data.Configurations;

public class ComercioConfiguration : IEntityTypeConfiguration<Comercio>
{
    public void Configure(EntityTypeBuilder<Comercio> builder)
    {
        builder.ToTable("Comercios");

        builder.HasKey(comercio => comercio.Id);

        builder.Property(comercio => comercio.Id)
            .ValueGeneratedOnAdd();

        builder.Property(comercio => comercio.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(comercio => comercio.Descripcion)
            .HasMaxLength(500);

        builder.Property(comercio => comercio.Telefono)
            .HasMaxLength(30);

        builder.Property(comercio => comercio.WhatsApp)
            .HasMaxLength(30);

        builder.Property(comercio => comercio.Horario)
            .HasMaxLength(200);

        builder.Property(comercio => comercio.Atiende24Horas)
            .IsRequired();

        builder.Property(comercio => comercio.Activo)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasOne(comercio => comercio.Categoria)
            .WithMany()
            .HasForeignKey(comercio => comercio.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Navigation(comercio => comercio.Direccion)
            .IsRequired();

        builder.HasIndex(comercio => comercio.CategoriaId);

        builder.HasIndex(comercio => new
        {
            comercio.Activo,
            comercio.Atiende24Horas
        });
    }
}
