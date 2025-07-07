using LojaDeDiversidades.Domain.Entities;
using LojaDeDiversidades.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaDeDiversidades.Infra.Repositories;

public class ProdutoRepository(LojaDbContext context) : IProdutoRepository
{
    private readonly DbSet<Produto> _produtoContext = context.Set<Produto>();

    public async Task<List<Produto>> ObterTodosAsync()
    {
        return await _produtoContext.AsNoTracking().ToListAsync();
    }

    public async Task<Produto> ObterPorIdAsync(int id)
    {
        return await _produtoContext.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AdicionarAsync(Produto produto)
    {
        await _produtoContext.AddAsync(produto);
        await context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Produto produto)
    {
        _produtoContext.Update(produto);
        await context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Produto produto)
    {
        _produtoContext.Remove(produto);
        await context.SaveChangesAsync();
    }
}