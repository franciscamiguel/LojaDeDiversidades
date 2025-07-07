import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly api = 'https://localhost:7110/api/v1/auth';

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, senha: string) {
    return this.http.post<{ token: string, nome: string, email: string, perfil: string }>(
      `${this.api}/login`,
      { email, senha }
    ).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('perfil', res.perfil);
        localStorage.setItem('email', res.email);
        localStorage.setItem('nome', res.nome);
      })
    );
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  getPerfil(): string | null {
    return localStorage.getItem('perfil');
  }

  getDecodedToken(): any {
    const token = this.getToken();
    if (!token) return null;
    try {
      const base64Payload = token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/');
      const payload = atob(base64Payload);
      return JSON.parse(decodeURIComponent(escape(payload)));
    } catch (e) {
      return null;
    }
  }

  getUserId(): number | null {
    const decoded = this.getDecodedToken();
    // Id pode estar em "sub" (caso padrão JWT) ou "nameid" ou outro claim.
    // Veja como você gerou o token no back: new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString())
    return decoded?.sub ? +decoded.sub : null;
  }

  getUserEmail(): string | null {
    return localStorage.getItem('email');
  }

  getUserName(): string | null {
    return localStorage.getItem('nome');
  }
}
