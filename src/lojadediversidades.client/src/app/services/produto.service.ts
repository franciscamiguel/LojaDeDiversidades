import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class ProdutoService {
  private api = 'https://localhost:7110/api/v1/produtos';

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<any[]>(this.api);
  }
  create(dto: any) {
    return this.http.post(this.api, dto);
  }
  update(id: number, dto: any) {
    return this.http.put(`${this.api}/${id}`, dto);
  }
  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
