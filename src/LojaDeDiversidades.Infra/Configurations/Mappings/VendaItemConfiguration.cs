using LojaDeDiversidades.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaDeDiversidades.Infra.Configurations.Mappings;

public class VendaItemConfiguration : IEntityTypeConfiguration<VendaItem>
{
    public void Configure(EntityTypeBuilder<VendaItem> builder)
    {
        builder.ToTable("VendaItens");
        builder.HasKey(vi => vi.Id);

        builder.Property(vi => vi.VendaId).IsRequired();
        builder.Property(vi => vi.ProdutoId).IsRequired();
        builder.Property(vi => vi.Quantidade).IsRequired();
        builder.Property(vi => vi.PrecoUnitario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(vi => vi.QuantidadeDevolvida);
    }
}