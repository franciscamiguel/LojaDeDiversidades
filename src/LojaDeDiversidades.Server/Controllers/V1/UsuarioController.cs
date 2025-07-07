using Asp.Versioning;
using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeDiversidades.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuarios")]
public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<List<UsuarioDto>>> ListarTodos()
    {
        var usuarios = await usuarioService.ListarTodosAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrador,Cliente")]
    public async Task<ActionResult<UsuarioDto>> ObterPorId(int id)
    {
        var usuario = await usuarioService.ObterPorIdAsync(id);
        if (usuario == null)
            return NotFound();
        return Ok(usuario);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioDto>> Criar([FromBody] CriarUsuarioDto dto)
    {
        var novo = await usuarioService.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novo.Id }, novo);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador,Cliente")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] UsuarioDto dto)
    {
        if (id != dto.Id) return BadRequest("Id não bate.");
        await usuarioService.AtualizarAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Remover([FromRoute] int id)
    {
        await usuarioService.RemoverAsync(id);
        return NoContent();
    }
}