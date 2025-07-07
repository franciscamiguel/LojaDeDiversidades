using LojaDeDiversidades.Domain.Entities;

namespace LojaDeDiversidades.Domain.Interfaces;

public interface IVendaRepository
{
    Task<Venda?> ObterPorIdAsync(int id);
    Task<List<Venda>> ObterTodosAsync();
    Task AdicionarAsync(Venda produto);
    Task AtualizarAsync(Venda produto);
    Task RemoverAsync(Venda produto);
}