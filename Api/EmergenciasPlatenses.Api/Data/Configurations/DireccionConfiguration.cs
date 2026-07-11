using EmergenciasPlatenses.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmergenciasPlatenses.Api.Data.Configurations;

public class DireccionConfiguration : IEntityTypeConfiguration<Direccion>
{
    public void Configure(EntityTypeBuilder<Direccion> builder)
    {
        builder.ToTable("Direcciones");

        builder.HasKey(direccion => direccion.Id);

        builder.Property(direccion => direccion.Id)
            .ValueGeneratedOnAdd();

        builder.Property(direccion => direccion.Calle)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(direccion => direccion.Numero)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(direccion => direccion.Piso)
            .HasMaxLength(20);

        builder.Property(direccion => direccion.Departamento)
            .HasMaxLength(20);

        builder.Property(direccion => direccion.CodigoPostal)
            .HasMaxLength(20);

        builder.Property(direccion => direccion.Referencia)
            .HasMaxLength(300);

        builder.Property(direccion => direccion.Latitud)
            .HasPrecision(9, 6);

        builder.Property(direccion => direccion.Longitud)
            .HasPrecision(9, 6);

        builder.HasOne(direccion => direccion.Ciudad)
            .WithMany()
            .HasForeignKey(direccion => direccion.CiudadId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne<Comercio>()
            .WithOne(comercio => comercio.Direccion)
            .HasForeignKey<Direccion>(direccion => direccion.ComercioId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasIndex(direccion => direccion.CiudadId);

        builder.HasIndex(direccion => direccion.ComercioId)
            .IsUnique();
    }
}
