namespace LojaDeDiversidades.Domain.Entities;

public class Venda(int clienteId)
{
    public int Id { get; }
    public DateTime DataVenda { get; private set; } = DateTime.UtcNow;
    public string Status { get; set; } = string.Empty;
    public bool Devolvida { get; private set; }
    public decimal Total => Itens.Sum(i => i.PrecoUnitario * i.Quantidade);

    // Relacionamentos
    public int ClienteId { get; private set; } = clienteId;
    public Usuario Usuario { get; set; } = null!;
    public ICollection<VendaItem> Itens { get; set; } = new List<VendaItem>();

    public void AdicionarItem(int produtoId, int quantidade, decimal precoUnitario)
    {
        var item = new VendaItem(Id, produtoId, quantidade, precoUnitario);
        Itens.Add(item);
    }

    public void MarcarComoDevolvida()
    {
        Devolvida = true;
    }
}