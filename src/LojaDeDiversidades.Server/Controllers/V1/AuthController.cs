using Asp.Versioning;
using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using LojaDeDiversidades.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeDiversidades.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
    {
        var usuario = await usuarioRepository.ObterPorEmailAsync(dto.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return Unauthorized("Usuário ou senha inválidos");

        var token = tokenService.GerarToken(
            usuario.Id,
            usuario.Nome,
            usuario.Email.ToString(),
            usuario.Perfil.ToString()
        );

        var response = new LoginResponseDto
        {
            Nome = usuario.Nome,
            Email = usuario.Email.ToString(),
            Perfil = usuario.Perfil.ToString(),
            Token = token
        };

        return Ok(response);
    }
}