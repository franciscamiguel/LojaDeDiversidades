using LojaDeDiversidades.Domain.Entities;

namespace LojaDeDiversidades.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<Produto> ObterPorIdAsync(int id);
    Task<List<Produto>> ObterTodosAsync();
    Task AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task RemoverAsync(Produto produto);
}