import { Component, OnInit } from '@angular/core';
import { ProdutoService } from '../../services/produto.service';
import { VendaService } from '../../services/venda.service';
import { RealizarVendaDto } from '../../models/realizar-venda.dto';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-vendas',
  templateUrl: './vendas.component.html',
  styleUrls: ['./vendas.component.scss']
})
export class VendasComponent implements OnInit {
  produtos: any[] = [];
  carrinho: any[] = [];
  totalCarrinho = 0;

  constructor(
    private produtoService: ProdutoService,
    private vendaService: VendaService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.produtoService.getAll().subscribe(res => {
      this.produtos = res.filter((p: any) => p.quantidadeEstoque > 0);
    });
  }

  adicionarAoCarrinho(produto: any) {
    const item = this.carrinho.find(i => i.produtoId === produto.id);
    if (item && item.quantidade < produto.quantidadeEstoque) {
      item.quantidade++;
    } else {
      this.carrinho.push({
        produtoId: produto.id,
        nome: produto.nome,
        preco: produto.preco,
        quantidade: 1
      });
    }
    this.calcularTotal();
  }

  removerDoCarrinho(item: any) {
    this.carrinho = this.carrinho.filter(i => i.produtoId !== item.produtoId);
    this.calcularTotal();
  }

  alterarQuantidade(item: any, qtd: number) {
    if (qtd < 1) return;
    const produto = this.produtos.find(p => p.id === item.produtoId);
    if (produto && qtd <= produto.quantidadeEstoque) {
      item.quantidade = qtd;
      this.calcularTotal();
    }
  }

  calcularTotal() {
    this.totalCarrinho = this.carrinho.reduce((soma, item) => soma + item.preco * item.quantidade, 0);
  }

  finalizarVenda() {
    if (this.carrinho.length === 0) return;

    const userId = this.authService.getUserId();
    if (userId == null) {
      alert('Usuário não autenticado.');
      return;
    }
    const vendaDto: RealizarVendaDto = {
      clienteId: userId,
      itens: this.carrinho
    };

    this.vendaService.criarVenda(vendaDto).subscribe({
      next: () => {
        alert('Venda realizada com sucesso!');
        this.carrinho = [];
        this.totalCarrinho = 0;
      },
      error: () => {
        alert('Erro ao finalizar venda.');
      }
    });
  }
}
