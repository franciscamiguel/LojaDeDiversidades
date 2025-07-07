export interface ItemDevolucaoParcialDto {
  produtoId: number;
  quantidade: number;
}

export interface DevolverItensVendaDto {
  vendaId: number;
  itens: ItemDevolucaoParcialDto[];
}
