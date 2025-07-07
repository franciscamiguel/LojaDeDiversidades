using System.Net.Mail;

namespace LojaDeDiversidades.Domain.ValueObjects;

public class Email
{
    protected Email() { }

    public Email(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("O endereço de e-mail não pode ser vazio.", nameof(endereco));

        if (!IsValidEmail(endereco))
            throw new ArgumentException("Endereço de e-mail inválido.", nameof(endereco));

        Endereco = endereco;
    }

    public string Endereco { get; }
    public int UsuarioId { get; set; }

    public override string ToString()
    {
        return Endereco;
    }

    public override bool Equals(object obj)
    {
        return obj is Email e && Endereco == e.Endereco;
    }

    public override int GetHashCode()
    {
        return Endereco.GetHashCode();
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}