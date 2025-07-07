import { Component, Input } from '@angular/core';
import { VendaService } from '../../services/venda.service';
import { DevolverItensVendaDto, ItemDevolucaoParcialDto } from '../../models/devolver-itens-venda.dto';

@Component({
  selector: 'app-devolucao-parcial',
  templateUrl: './devolucao-parcial.component.html'
})
export class DevolucaoParcialComponent {
  @Input() vendaId!: number;
  @Input() itensVenda!: { produtoId: number; nome: string; quantidade: number; }[];

  devolucaoItens: ItemDevolucaoParcialDto[] = [];
  mensagem = '';
  erro = '';
  quantidadesDevolucao: { [produtoId: number]: number } = {};

  constructor(private vendaService: VendaService) {}

  adicionarItemDevolucao(produtoId: number, quantidade: number) {
    // Remove se já existe (não permitir duplicado)
    this.devolucaoItens = this.devolucaoItens.filter(i => i.produtoId !== produtoId);

    if (quantidade > 0) {
      this.devolucaoItens.push({ produtoId, quantidade });
    }
  }

  devolverParcial() {
    if (this.devolucaoItens.length === 0) {
      this.erro = 'Selecione pelo menos um item e quantidade para devolver.';
      return;
    }
    this.erro = '';
    this.mensagem = '';

    const dto: DevolverItensVendaDto = {
      vendaId: this.vendaId,
      itens: this.devolucaoItens
    };

    this.vendaService.devolverVendaParcial(this.vendaId, dto).subscribe({
      next: () => {
        this.mensagem = 'Devolução parcial realizada com sucesso!';
        this.devolucaoItens = [];
      },
      error: (e) => {
        this.erro = e?.error || 'Erro ao processar devolução parcial.';
      }
    });
  }
}
