import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  form: FormGroup;
  loading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', Validators.required]
    });
  }

  submit() {
    if (this.form.invalid) return;

    this.loading = true;
    this.error = null;
    const { email, senha } = this.form.value;

    this.auth.login(email, senha).subscribe({
      next: () => {
        this.loading = false;
        // Redireciona conforme o perfil
        const perfil = this.auth.getPerfil();
        if (perfil === 'Administrador') {
          this.router.navigate(['/produtos']);
        } else {
          this.router.navigate(['/vendas']);
        }
      },
      error: err => {
        this.loading = false;
        this.error = err.error || 'Usuário ou senha inválidos!';
      }
    });
  }
}
