// src/app/services/venda.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DevolverItensVendaDto } from '../models/devolver-itens-venda.dto';
import { RealizarVendaDto } from '../models/realizar-venda.dto';

@Injectable({ providedIn: 'root' })
export class VendaService {
  private api = 'https://localhost:7110/api/v1/vendas';

  constructor(private http: HttpClient) {}

  /**
   * Cria uma nova venda.
   * @param vendaDto Objeto contendo os detalhes da venda a ser criada.
   */
  criarVenda(vendaDto: RealizarVendaDto) {
    return this.http.post(this.api, vendaDto);
  }

  /**
   * Devolve uma venda completa, cancelando todos os itens.
   * @param vendaId ID da venda a ser devolvida.
   */
  devolverVendaCompleta(vendaId: number) {
    return this.http.post(`/api/v1/vendas/${vendaId}/devolucao-completa`, {});
  }

  /**
   * Devolve uma venda parcial, permitindo especificar quais itens devolver.
   * @param vendaId ID da venda a ser devolvida.
   * @param devolucao itens a serem devolvidos, cada item deve conter o ID do produto e a quantidade.
   */
  devolverVendaParcial(vendaId: number, devolucao: DevolverItensVendaDto) {
    return this.http.post(`${this.api}/${vendaId}/devolucao-parcial`, devolucao);
  }

  getVendas() {
    return this.http.get<any[]>(this.api);
  }

  getVendaById(id: number) {
    return this.http.get(`${this.api}/${id}`);
  }

  atualizarVenda(id: number, vendaDto: any) {
    return this.http.put(`${this.api}/${id}`, vendaDto);
  }

  deletarVenda(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }

  getVendasByCliente(clienteId: number) {
    return this.http.get<any[]>(`${this.api}/cliente/${clienteId}`);
  }

  getVendasByProduto(produtoId: number) {
    return this.http.get<any[]>(`${this.api}/produto/${produtoId}`);
  }
  
}
