using LojaDeDiversidades.Application.DTOs;

namespace LojaDeDiversidades.Application.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioDto>> ListarTodosAsync();
    Task<UsuarioDto> ObterPorIdAsync(int id);
    Task<UsuarioDto> ObterPorEmailAsync(string email);
    Task<UsuarioDto> CriarAsync(CriarUsuarioDto dto);
    Task AtualizarAsync(UsuarioDto dto);
    Task RemoverAsync(int id);
}