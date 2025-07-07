using LojaDeDiversidades.Domain.Entities;
using LojaDeDiversidades.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LojaDeDiversidades.Infra.Repositories;

public class UsuarioRepository(LojaDbContext context) : IUsuarioRepository
{
    private readonly DbSet<Usuario> _usuarioContext = context.Set<Usuario>();

    public async Task<List<Usuario>> ObterTodosAsync()
    {
        return await _usuarioContext.AsNoTracking().ToListAsync();
    }

    public async Task<Usuario> ObterPorIdAsync(int id)
    {
        return await _usuarioContext.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> ObterPorEmailAsync(string email)
    {
        return await _usuarioContext.FirstOrDefaultAsync(u => u.Email.Endereco == email);
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        await _usuarioContext.AddAsync(usuario);
        await context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Usuario usuario)
    {
        _usuarioContext.Update(usuario);
        await context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Usuario usuario)
    {
        _usuarioContext.Remove(usuario);
        await context.SaveChangesAsync();
    }
}