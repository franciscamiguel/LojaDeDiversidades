using LojaDeDiversidades.Application.DTOs;
using LojaDeDiversidades.Application.Interfaces;
using LojaDeDiversidades.Domain.Entities;
using LojaDeDiversidades.Domain.Interfaces;

namespace LojaDeDiversidades.Application.Services;

public class VendaService(
    IVendaRepository vendaRepository,
    IProdutoRepository produtoRepository,
    IUsuarioRepository usuarioRepository)
    : IVendaService
{
    public async Task<VendaDto> RealizarVendaAsync(RealizarVendaDto vendaInput)
    {
        // Validar cliente
        var cliente = await usuarioRepository.ObterPorIdAsync(vendaInput.ClienteId);
        if (cliente is null)
            throw new Exception("Cliente não encontrado.");

        // Criar venda
        var venda = new Venda(vendaInput.ClienteId);

        var itensVendaDto = new List<ItemVendaDto>();

        foreach (var itemInput in vendaInput.Itens)
        {
            // Validar produto
            var produto = await produtoRepository.ObterPorIdAsync(itemInput.ProdutoId);
            if (produto is null)
                throw new Exception($"Produto {itemInput.ProdutoId} não encontrado.");

            if (produto.QuantidadeEstoque < itemInput.Quantidade)
                throw new Exception($"Estoque insuficiente para o produto {produto.Nome}.");

            // Baixar estoque
            produto.BaixarEstoque(itemInput.Quantidade);
            await produtoRepository.AtualizarAsync(produto);

            // Adicionar item na venda (usa o valor atual do produto)
            venda.AdicionarItem(produto.Id, itemInput.Quantidade, produto.Preco);

            // Adiciona no DTO
            itensVendaDto.Add(new ItemVendaDto
            {
                ProdutoId = produto.Id,
                ProdutoNome = produto.Nome,
                Quantidade = itemInput.Quantidade,
                PrecoUnitario = produto.Preco
            });
        }

        // Persiste a venda
        await vendaRepository.AdicionarAsync(venda);

        // Monta o DTO de resposta
        return new VendaDto
        {
            Id = venda.Id,
            ClienteId = venda.ClienteId,
            DataVenda = venda.DataVenda,
            Itens = itensVendaDto
        };
    }

    public async Task<DevolucaoVendaDto> DevolverVendaAsync(DevolverVendaDto devolucaoInput)
    {
        // Buscar a venda
        var venda = await vendaRepository.ObterPorIdAsync(devolucaoInput.VendaId);
        if (venda is null)
            throw new Exception("Venda não encontrada.");

        // Verifica se já foi devolvida (caso você implemente o controle, ex: venda.Devolvida)
        if (venda.Devolvida)
            throw new Exception("Venda já foi devolvida.");

        var itensDevolvidos = new List<ItemDevolucaoDto>();

        foreach (var item in venda.Itens)
        {
            var produto = await produtoRepository.ObterPorIdAsync(item.ProdutoId);
            if (produto is null)
                throw new Exception($"Produto {item.ProdutoId} não encontrado para devolução.");

            produto.ReporEstoque(item.Quantidade);
            await produtoRepository.AtualizarAsync(produto);

            itensDevolvidos.Add(new ItemDevolucaoDto
            {
                ProdutoId = produto.Id,
                ProdutoNome = produto.Nome,
                QuantidadeDevolvida = item.Quantidade
            });
        }

        // Marca venda como devolvida (ideal para não permitir devolução dupla)
        venda.MarcarComoDevolvida();
        await vendaRepository.AtualizarAsync(venda);

        return new DevolucaoVendaDto
        {
            VendaId = venda.Id,
            DataDevolucao = DateTime.UtcNow,
            Itens = itensDevolvidos,
            Status = "Devolução realizada com sucesso"
        };
    }

    public async Task<DevolucaoParcialDto> DevolverItensVendaAsync(DevolverItensVendaDto devolucaoParcialInput)
    {
        // Buscar a venda
        var venda = await vendaRepository.ObterPorIdAsync(devolucaoParcialInput.VendaId);
        if (venda == null)
            throw new Exception("Venda não encontrada.");

        // Verifica se já foi totalmente devolvida
        if (venda.Devolvida)
            throw new Exception("Venda já foi devolvida completamente.");

        var itensDevolvidos = new List<ItemDevolucaoDto>();

        foreach (var itemDevolver in devolucaoParcialInput.Itens)
        {
            var itemVenda = venda.Itens.FirstOrDefault(i => i.ProdutoId == itemDevolver.ProdutoId);
            if (itemVenda == null)
                throw new Exception($"Produto {itemDevolver.ProdutoId} não pertence a essa venda.");

            // Calcula quanto já foi devolvido (caso você registre devoluções parciais no banco)
            var quantidadeDevolvida = itemVenda.QuantidadeDevolvida;

            if (itemDevolver.Quantidade <= 0)
                throw new Exception("Quantidade para devolução deve ser maior que zero.");

            if (quantidadeDevolvida + itemDevolver.Quantidade > itemVenda.Quantidade)
                throw new Exception(
                    $"Tentativa de devolver mais do que comprado para o produto {itemVenda.ProdutoId}.");

            // Repor estoque
            var produto = await produtoRepository.ObterPorIdAsync(itemVenda.ProdutoId);
            if (produto == null)
                throw new Exception($"Produto {itemVenda.ProdutoId} não encontrado para devolução.");

            produto.ReporEstoque(itemDevolver.Quantidade);
            await produtoRepository.AtualizarAsync(produto);

            // Atualiza controle de devolução
            itemVenda.Devolver(quantidadeDevolvida + itemDevolver.Quantidade);

            itensDevolvidos.Add(new ItemDevolucaoDto
            {
                ProdutoId = produto.Id,
                ProdutoNome = produto.Nome,
                QuantidadeDevolvida = itemDevolver.Quantidade
            });
        }

        // Se todos os itens da venda estiverem com quantidadeDevolvida == quantidade, marca como devolvida
        var todosItensDevolvidos = venda.Itens.All(i => i.QuantidadeDevolvida >= i.Quantidade);
        if (todosItensDevolvidos)
            venda.MarcarComoDevolvida();

        await vendaRepository.AtualizarAsync(venda);

        return new DevolucaoParcialDto
        {
            VendaId = venda.Id,
            DataDevolucao = DateTime.UtcNow,
            ItensDevolvidos = itensDevolvidos,
            Status = "Devolução parcial realizada com sucesso"
        };
    }
}