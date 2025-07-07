namespace LojaDeDiversidades.Application.DTOs;

public class RealizarVendaDto
{
    public int ClienteId { get; set; }
    public List<ItemVendaVendaDto> Itens { get; set; } = [];
}