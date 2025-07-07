using LojaDeDiversidades.Application.DTOs;

namespace LojaDeDiversidades.Application.Interfaces;

public interface IProdutoService
{
    Task<List<ProdutoDto>> ListarTodosAsync();
    Task<ProdutoDto?> ObterPorIdAsync(int id);
    Task<ProdutoDto> CriarAsync(ProdutoDto dto);
    Task AtualizarAsync(ProdutoDto dto);
    Task RemoverAsync(int id);
}