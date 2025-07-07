import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProdutoService } from '../../services/produto.service';

export interface Produto {
  id?: number;
  nome: string;
  descricao?: string;
  preco: number;
  quantidadeEstoque: number;
}

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrls: ['./produtos.component.scss']
})
export class ProdutosComponent implements OnInit {
  produtos: Produto[] = [];
  formProduto: FormGroup;
  editandoProdutoId?: number;
  carregando = false;
  modalNovoProduto: any;

  constructor(
    private produtoService: ProdutoService,
    private fb: FormBuilder
  ) {
    // Inicializa o formulário reativo
    this.formProduto = this.fb.group({
      id: [null],
      nome: ['', Validators.required],
      descricao: [''],
      preco: [0, [Validators.required, Validators.min(0)]],
      quantidadeEstoque: [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.listarProdutos();
  }

  listarProdutos(): void {
    this.carregando = true;
    this.produtoService.getAll().subscribe({
      next: (res: Produto[]) => {
        this.produtos = res;
        this.carregando = false;
      },
      error: () => {
        alert('Erro ao carregar produtos!');
        this.carregando = false;
      }
    });
  }

  salvarProduto(): void {
    if (this.formProduto.invalid) return;

    const produto: Produto = this.formProduto.value;

    if (this.editandoProdutoId) {
      // Atualizar produto existente
      this.produtoService.update(this.editandoProdutoId, produto).subscribe({
        next: () => {
          this.listarProdutos();
          this.cancelarEdicao();
        },
        error: () => alert('Erro ao atualizar produto!')
      });
    } else {
      // Criar novo produto
      this.produtoService.create(
        {
          nome: produto.nome,
          Descricao: produto.descricao,
          preco: produto.preco,
          quantidadeEstoque: produto.quantidadeEstoque
        }
        ).subscribe({
        next: () => {
          this.listarProdutos();
          this.formProduto.reset({ preco: 0, quantidadeEstoque: 0 });
        },
        error: () => alert('Erro ao criar produto!')
      });
    }
  }

  editarProduto(produto: Produto): void {
    this.editandoProdutoId = produto.id;
    this.formProduto.setValue({
      id: produto.id,
      nome: produto.nome,
      descricao: produto.descricao || '',
      preco: produto.preco,
      quantidadeEstoque: produto.quantidadeEstoque
    });
  }

  cancelarEdicao(): void {
    this.editandoProdutoId = undefined;
    this.formProduto.reset({ preco: 0, quantidadeEstoque: 0 });
  }

  excluirProduto(id?: number): void {
    if (!id) return;
    if (confirm('Deseja realmente excluir este produto?')) {
      this.produtoService.delete(id).subscribe({
        next: () => this.listarProdutos(),
        error: () => alert('Erro ao excluir produto!')
      });
    }
  }
}
