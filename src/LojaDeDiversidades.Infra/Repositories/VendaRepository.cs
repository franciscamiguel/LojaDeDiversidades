using LojaDeDiversidades.Domain.Entities;
using LojaDeDiversidades.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaDeDiversidades.Infra.Repositories;

public class VendaRepository(LojaDbContext context) : IVendaRepository
{
    private readonly DbSet<Venda> _vendaContext = context.Set<Venda>();

    public async Task<Venda> ObterPorIdAsync(int id)
    {
        // Inclui os itens da venda (usado para devolução, etc.)
        return await _vendaContext
            .Include(v => v.Itens)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Venda>> ObterTodosAsync()
    {
        return await _vendaContext.Include(v => v.Itens)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AdicionarAsync(Venda venda)
    {
        await _vendaContext.AddAsync(venda);
        await context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Venda venda)
    {
        _vendaContext.Update(venda);
        await context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Venda produto)
    {
        _vendaContext.Remove(produto);
        await context.SaveChangesAsync();
    }
}