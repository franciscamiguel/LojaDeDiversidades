﻿namespace LojaDeDiversidades.Application.DTOs;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Perfil { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
}