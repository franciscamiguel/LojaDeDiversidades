using Asp.Versioning;
using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaDeDiversidades.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/vendas")]
public class VendaController(IVendaService vendaService) : ControllerBase
{
    /// <summary>
    ///     Realiza uma venda.
    /// </summary>
    /// <param name="vendaInput"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Cliente")]
    public async Task<ActionResult<VendaDto>> RealizarVenda([FromBody] RealizarVendaDto vendaInput)
    {
        var venda = await vendaService.RealizarVendaAsync(vendaInput);
        return Ok(venda);
    }

    /// <summary>
    ///     Devolução completa de uma venda.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/devolucao-completa")]
    [Authorize(Roles = "Cliente")]
    public async Task<ActionResult<DevolucaoVendaDto>> DevolverVendaCompleta([FromRoute] int id)
    {
        var input = new DevolverVendaDto { VendaId = id };
        var devolucao = await vendaService.DevolverVendaAsync(input);
        return Ok(devolucao);
    }

    /// <summary>
    ///     Devolução parcial de itens de uma venda.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/devolucao-parcial")]
    [Authorize(Roles = "Cliente")]
    public async Task<ActionResult<DevolucaoParcialDto>> DevolverItensVenda([FromRoute] int id,
        [FromBody] DevolverItensVendaDto input)
    {
        if (input.VendaId != id)
            return BadRequest("VendaId do body e da rota não coincidem.");

        var devolucao = await vendaService.DevolverItensVendaAsync(input);
        return Ok(devolucao);
    }
}