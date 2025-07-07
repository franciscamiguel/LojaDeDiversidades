using LojaDeDiversidades.Domain.Entities;

namespace LojaDeDiversidades.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> ObterPorIdAsync(int id);
    Task<Usuario> ObterPorEmailAsync(string email);
    Task<List<Usuario>> ObterTodosAsync();
    Task AdicionarAsync(Usuario produto);
    Task AtualizarAsync(Usuario produto);
    Task RemoverAsync(Usuario produto);
}