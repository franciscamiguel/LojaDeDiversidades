namespace LojaDeDiversidades.Domain.Entities;

public class VendaItem(int vendaId, int produtoId, int quantidade, decimal precoUnitario)
{
    public int Id { get; }
    public int VendaId { get; private set; } = vendaId;
    public int ProdutoId { get; private set; } = produtoId;
    public int Quantidade { get; } = quantidade;
    public decimal PrecoUnitario { get; private set; } = precoUnitario;
    public int QuantidadeDevolvida { get; private set; }

    public void Devolver(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade de devolução deve ser maior que zero.");

        if (quantidade > Quantidade - QuantidadeDevolvida)
            throw new InvalidOperationException("Quantidade de devolução excede a quantidade comprada.");

        QuantidadeDevolvida += quantidade;
    }
}