using LojaDeDiversidades.Application.DTOs;

namespace LojaDeDiversidades.Application.Interfaces;

public interface IVendaService
{
    Task<VendaDto> RealizarVendaAsync(RealizarVendaDto vendaInput);
    Task<DevolucaoVendaDto> DevolverVendaAsync(DevolverVendaDto devolucaoInput);
    Task<DevolucaoParcialDto> DevolverItensVendaAsync(DevolverItensVendaDto devolucaoParcialInput);
}