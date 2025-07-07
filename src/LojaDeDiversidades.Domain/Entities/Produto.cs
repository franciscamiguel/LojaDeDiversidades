namespace LojaDeDiversidades.Domain.Entities;

public class Produto
{
    public Produto(string nome, string descricao, decimal preco, int quantidadeEstoque)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
    }

    public int Id { get; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public string ImagemUrl { get; private set; } = string.Empty;

    // Relacionamentos
    public ICollection<VendaItem> VendaItems { get; set; } = new List<VendaItem>();

    public void Atualizar(string nome, string descricao, decimal preco, int quantidadeEstoque)
    {
        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
    }

    public void BaixarEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser positiva.");
        if (QuantidadeEstoque < quantidade)
            throw new InvalidOperationException("Estoque insuficiente.");
        QuantidadeEstoque -= quantidade;
    }

    public void ReporEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("Quantidade deve ser positiva.");
        QuantidadeEstoque += quantidade;
    }
}