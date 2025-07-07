export interface ItemVendaVendaDto {
  produtoId: number;
  quantidade: number;
}

export interface RealizarVendaDto {
  clienteId: number;
  itens: ItemVendaVendaDto[];
}
