using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using LojaDeDiversidades.Domain.Entities;
using LojaDeDiversidades.Domain.Interfaces;

namespace LojaDeDiversidades.Application.Services;

public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
{
    public async Task<List<ProdutoDto>> ListarTodosAsync()
    {
        var produtos = await produtoRepository.ObterTodosAsync();
        return produtos.Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            QuantidadeEstoque = p.QuantidadeEstoque
        }).ToList();
    }

    public async Task<ProdutoDto> ObterPorIdAsync(int id)
    {
        var p = await produtoRepository.ObterPorIdAsync(id);
        if (p is null) return null;
        return new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Descricao = p.Descricao,
            Preco = p.Preco,
            QuantidadeEstoque = p.QuantidadeEstoque
        };
    }

    public async Task<ProdutoDto> CriarAsync(ProdutoDto dto)
    {
        var produto = new Produto(dto.Nome, dto.Descricao, dto.Preco, dto.QuantidadeEstoque);
        await produtoRepository.AdicionarAsync(produto);
        dto.Id = produto.Id;
        return dto;
    }

    public async Task AtualizarAsync(ProdutoDto dto)
    {
        var produto = await produtoRepository.ObterPorIdAsync(dto.Id);
        if (produto is null)
            throw new Exception("Produto não encontrado.");

        produto.Atualizar(dto.Nome, dto.Descricao, dto.Preco, dto.QuantidadeEstoque);
        await produtoRepository.AtualizarAsync(produto);
    }

    public async Task RemoverAsync(int id)
    {
        var produto = await produtoRepository.ObterPorIdAsync(id);
        if (produto is null)
            throw new Exception("Produto não encontrado.");
        await produtoRepository.RemoverAsync(produto);
    }
}