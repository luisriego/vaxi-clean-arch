using CleanArchitecture.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Cofigurations;

internal sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(usuario => usuario.Id);

        builder.Property(usuario => usuario.Nombre)
            .HasMaxLength(200)
            .HasConversion(nombre => nombre!.Value, value => new Nombre(value));

        builder.Property(usuario => usuario.Apellido)
            .HasMaxLength(200)
            .HasConversion(apellido => apellido!.Value, value => new Apellido(value));

        builder.Property(usuario => usuario.Email)
            .HasMaxLength(500)
            .HasConversion(email => email!.Value, value => new Domain.Usuarios.Email(value));

        builder.HasIndex(usuario => usuario.Email)
            .IsUnique();
    }
}
