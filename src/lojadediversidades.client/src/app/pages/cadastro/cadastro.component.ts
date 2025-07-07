import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsuarioService } from '../../services/usuario.service';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})
export class CadastroComponent {
  cadastroForm: FormGroup;
  mensagem: string = '';

  constructor(
    private fb: FormBuilder,
    private usuarioService: UsuarioService,
    private router: Router
  ) {
    this.cadastroForm = this.fb.group({
      nome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      telefone: [''],
      dataNascimento: ['', Validators.required],
      perfil: ['Cliente'] // fixo, cliente sempre cadastra como Cliente
    });
  }

  cadastrar() {
    if (this.cadastroForm.invalid) return;

    this.usuarioService.cadastrar(this.cadastroForm.value).subscribe({
      next: () => {
        this.mensagem = 'Cadastro realizado! Faça login para continuar.';
        setTimeout(() => this.router.navigate(['/login']), 1500);
      },
      error: err => {
        this.mensagem = err.error?.message || 'Erro ao cadastrar. Tente novamente!';
      }
    });
  }
}
