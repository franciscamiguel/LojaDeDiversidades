using LojaDeDiversidades.Domain.Enums;
using LojaDeDiversidades.Domain.ValueObjects;

namespace LojaDeDiversidades.Domain.Entities;

public class Usuario
{
    public Usuario(string nome, Email email, string senhaHash, string telefone, DateTime dataNascimento,
        PerfilUsuario perfil)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Telefone = telefone;
        DataNascimento = dataNascimento;
        Perfil = perfil;
        Ativo = true;
    }

    // Para o EF Core
    protected Usuario()
    {
    }

    public int Id { get; }
    public string Nome { get; private set; } = string.Empty;
    public Email Email { get; private set; }
    public string SenhaHash { get; private set; }
    public string Telefone { get; private set; } = string.Empty;
    public DateTime DataNascimento { get; private set; } = DateTime.MinValue;
    public bool Ativo { get; private set; } = true;

    // Relacionamentos
    public PerfilUsuario Perfil { get; set; }
    public ICollection<Venda> Vendas { get; set; } = new List<Venda>();

    public void Inativar()
    {
        Ativo = false;
    }

    public void Atualizar(string nome, Email email, string telefone, DateTime dataNascimento, PerfilUsuario perfil)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
        DataNascimento = dataNascimento;
        Perfil = perfil;
    }
}