namespace LojaDeDiversidades.Application.DTOs;

public class DevolverItensVendaDto
{
    public int VendaId { get; set; }
    public List<ItemDevolucaoParcialDto> Itens { get; set; } = [];
}