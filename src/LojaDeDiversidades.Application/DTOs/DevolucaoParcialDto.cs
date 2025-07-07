namespace LojaDeDiversidades.Application.DTOs;

public class DevolucaoParcialDto
{
    public int VendaId { get; set; }
    public DateTime DataDevolucao { get; set; }
    public List<ItemDevolucaoDto> ItensDevolvidos { get; set; } = [];
    public string Status { get; set; } = "";
}