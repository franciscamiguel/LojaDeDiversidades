using Asp.Versioning;
using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeDiversidades.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/produtos")]
public class ProdutoController(IProdutoService produtoService) : ControllerBase
{
    /// <summary>
    ///     Lista todos os produtos disponíveis.
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Cliente,Administrador")]
    public async Task<ActionResult<List<ProdutoDto>>> ListarTodos()
    {
        var produtos = await produtoService.ListarTodosAsync();
        return Ok(produtos);
    }

    /// <summary>
    ///     Consulta um produto pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Cliente,Administrador")]
    public async Task<ActionResult<ProdutoDto>> ObterPorId([FromRoute] int id)
    {
        var produto = await produtoService.ObterPorIdAsync(id);
        if (produto == null)
            return NotFound();
        return Ok(produto);
    }

    /// <summary>
    ///     Cadastra um novo produto (somente Administrador).
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<ActionResult<ProdutoDto>> Criar([FromBody] ProdutoDto dto)
    {
        var novoProduto = await produtoService.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoProduto.Id }, novoProduto);
    }

    /// <summary>
    ///     Atualiza um produto (somente Administrador).
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Atualizar([FromRoute] int id, [FromBody] ProdutoDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Id do produto na rota e no corpo não coincidem.");
        await produtoService.AtualizarAsync(dto);
        return NoContent();
    }

    /// <summary>
    ///     Remove um produto (somente Administrador).
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Remover([FromRoute] int id)
    {
        await produtoService.RemoverAsync(id);
        return NoContent();
    }
}