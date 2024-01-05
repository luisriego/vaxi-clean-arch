using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Comentarios;
using CleanArchitecture.Domain.Usuarios;
using CleanArchitecture.Domain.Vehiculos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Cofigurations;

internal sealed class ComentariosConfiguracion : IEntityTypeConfiguration<Comentario>
{
    public void Configure(EntityTypeBuilder<Comentario> builder)
    {
        builder.ToTable("comentarios");
        builder.HasKey(comentario => comentario.Id);

        builder.Property(comentario => comentario.Clasificacion)
            .HasConversion(clasificacion => clasificacion.Value, value => Clasificacion.Create(value).Value);

        builder.Property(comentario => comentario.ComentarioTexto)
            .HasMaxLength(200);

        builder.HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(comentario => comentario.VehiculoId);

        builder.HasOne<Alquiler>()
            .WithMany()
            .HasForeignKey(comentario => comentario.AlquilerId);

        builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(comentario => comentario.UserId);
    }
}
