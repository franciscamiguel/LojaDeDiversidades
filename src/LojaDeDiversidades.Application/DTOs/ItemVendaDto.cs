namespace LojaDeDiversidades.Application.DTOs;

public class ItemVendaDto
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = "";
    public int Quantidade { get; init; }
    public decimal PrecoUnitario { get; init; }
}