import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }

    // Decodifica payload do JWT para extrair perfil/role
    const payload = this.parseJwt(token);

    const requiredRole = next.data['role'];
    if (requiredRole && payload?.role !== requiredRole) {
      this.router.navigate(['/']);
      return false;
    }

    return true;
  }

  // Função simples para decodificar o payload do JWT
  private parseJwt(token: string): any {
    try {
      const base64Payload = token.split('.')[1];
      const payload = atob(base64Payload.replace(/-/g, '+').replace(/_/g, '/'));
      return JSON.parse(decodeURIComponent(escape(payload)));
    } catch (e) {
      return null;
    }
  }
}
