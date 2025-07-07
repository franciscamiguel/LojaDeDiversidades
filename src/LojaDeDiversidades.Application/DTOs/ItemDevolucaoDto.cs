namespace LojaDeDiversidades.Application.DTOs;

public class ItemDevolucaoDto
{
    public int ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = "";
    public int QuantidadeDevolvida { get; set; }
}