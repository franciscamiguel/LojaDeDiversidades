namespace LojaDeDiversidades.Application.DTOs;

public class DevolucaoVendaDto
{
    public int VendaId { get; set; }
    public DateTime DataDevolucao { get; set; }
    public List<ItemDevolucaoDto> Itens { get; set; } = new();
    public string Status { get; set; } = "";
}