using LojaDeDiversidades.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaDeDiversidades.Infra.Configurations.Mappings;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Perfil)
            .IsRequired();

        builder.Property(u => u.Telefone);

        builder.Property(u => u.DataNascimento)
            .HasColumnType("datetime2");

        builder.Property(u => u.Ativo)
            .IsRequired()
            .HasDefaultValue(false);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Endereco)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(150);
        });
    }
}