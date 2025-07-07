import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private api = 'https://localhost:7110/api/v1/usuarios';

  constructor(private http: HttpClient) {}

  cadastrar(usuario: any) {
    return this.http.post(this.api, usuario);
  }
}
