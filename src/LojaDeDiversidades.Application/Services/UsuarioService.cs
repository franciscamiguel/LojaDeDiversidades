using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using LojaDeDiversidades.Domain.Entities;
using LojaDeDiversidades.Domain.Enums;
using LojaDeDiversidades.Domain.Interfaces;
using LojaDeDiversidades.Domain.ValueObjects;

namespace LojaDeDiversidades.Application.Services;

public class UsuarioService(
    IUsuarioRepository usuarioRepository) : IUsuarioService
{
    public async Task<List<UsuarioDto>> ListarTodosAsync()
    {
        var usuarios = await usuarioRepository.ObterTodosAsync();
        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email.ToString(),
            Perfil = u.Perfil.ToString()
        }).ToList();
    }

    public async Task<UsuarioDto> ObterPorIdAsync(int id)
    {
        var u = await usuarioRepository.ObterPorIdAsync(id);
        if (u == null) throw new Exception("Usuário não encontrado");
        return new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email.ToString(),
            Perfil = u.Perfil.ToString()
        };
    }

    public async Task<UsuarioDto> ObterPorEmailAsync(string email)
    {
        var u = await usuarioRepository.ObterPorEmailAsync(email);
        if (u == null) throw new Exception("Usuário não encontrado");
        return new UsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email.ToString(),
            Perfil = u.Perfil.ToString()
        };
    }

    public async Task<UsuarioDto> CriarAsync(CriarUsuarioDto dto)
    {
        // Hash de senha simplificado para exemplo (substitua por um hasher real)
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        var emailExistente = await usuarioRepository.ObterPorEmailAsync(dto.Email);
        if (emailExistente != null) throw new Exception("Email já cadastrado");

        var usuario = new Usuario(dto.Nome, new Email(dto.Email), senhaHash, dto.Telefone, dto.DataNascimento,
            PerfilUsuario.Cliente);
        await usuarioRepository.AdicionarAsync(usuario);

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email.Endereco,
            Perfil = usuario.Perfil.ToString()
        };
    }

    public async Task AtualizarAsync(UsuarioDto dto)
    {
        var usuario = await usuarioRepository.ObterPorIdAsync(dto.Id);
        if (usuario == null) throw new Exception("Usuário não encontrado");

        usuario.Atualizar(
            dto.Nome,
            new Email(dto.Email),
            dto.Telefone,
            dto.DataNascimento,
            Enum.Parse<PerfilUsuario>(dto.Perfil)
        );
        await usuarioRepository.AtualizarAsync(usuario);
    }

    public async Task RemoverAsync(int id)
    {
        var usuario = await usuarioRepository.ObterPorIdAsync(id);
        if (usuario == null) throw new Exception("Usuário não encontrado");
        await usuarioRepository.RemoverAsync(usuario);
    }
}