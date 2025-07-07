namespace LojaDeDiversidades.Application.DTOs;

public class VendaDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTime DataVenda { get; set; }
    public List<ItemVendaDto> Itens { get; set; } = new();
    public decimal Total => Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
}