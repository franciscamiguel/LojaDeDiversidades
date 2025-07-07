namespace LojaDeDiversidades.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(int usuarioId, string nome, string email, string role);
}
