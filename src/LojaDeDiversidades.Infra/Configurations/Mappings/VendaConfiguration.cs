using LojaDeDiversidades.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaDeDiversidades.Infra.Configurations.Mappings;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.ClienteId).IsRequired();

        builder.Property(v => v.DataVenda)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(v => v.Devolvida).IsRequired();

        builder.HasMany(v => v.Itens)
            .WithOne()
            .HasForeignKey(vi => vi.VendaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}