using System;
using System.Collections.Generic;
using System.Linq;
using TesteCamposDealer.DB;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepo;
    private readonly IProdutoRepository _produtoRepo;
    private readonly IClienteRepository _clienteRepo;

    public VendaService(
        IVendaRepository vendaRepo,
        IProdutoRepository produtoRepo,
        IClienteRepository clienteRepo)
    {
        _vendaRepo = vendaRepo;
        _produtoRepo = produtoRepo;
        _clienteRepo = clienteRepo;
    }

    public VendaDetalheDto CriarVenda(VendaDto dto)
    {
        if (dto.Itens == null || !dto.Itens.Any())
            throw new ValidationException("Venda deve ter itens");

        var cliente = _clienteRepo.GetById(dto.IdCliente);

        if (cliente == null)
            throw new NotFoundException("Cliente não encontrado");

        var venda = new Venda
        {
            idCliente = dto.IdCliente,
            dthRegistro = DateTime.Now
        };

        var itens = new List<VendaItem>();
        decimal total = 0;

        foreach (var itemDto in dto.Itens)
        {
            if (itemDto.Quantidade <= 0)
                throw new ValidationException("Quantidade inválida");

            var produto = _produtoRepo.GetById(itemDto.IdProduto);

            if (produto == null)
                throw new NotFoundException($"Produto {itemDto.IdProduto} não encontrado");

            var item = new VendaItem
            {
                IdProduto = produto.idProduto,
                Quantidade = itemDto.Quantidade,
                ValorUnitario = produto.vlrProduto
            };

            total += item.Quantidade * item.ValorUnitario;

            itens.Add(item);
        }

        venda.vlrTotal = total;

        var primeiroItem = itens.First();
        venda.idProduto = primeiroItem.IdProduto;
        venda.vlrProduto = (double)primeiroItem.ValorUnitario;

        var vendaId = _vendaRepo.CriarVenda(venda, itens);

        return GetById(vendaId);
    }

    public VendaDetalheDto GetById(int id)
    {
        var venda = _vendaRepo.GetById(id);

        if (venda == null)
            throw new NotFoundException("Venda não encontrada");

        var itens = _vendaRepo.GetItensByVenda(id);

        return Map(venda, itens);
    }

    public List<VendaDetalheDto> GetAll()
    {
        var vendas = _vendaRepo.GetAll();
        return vendas.Select(v => Map(v, _vendaRepo.GetItensByVenda(v.idVenda))).ToList();
    }

    public List<VendaDetalheDto> GetByCliente(int idCliente)
    {
        var vendas = _vendaRepo.GetByCliente(idCliente);

        return vendas.Select(v => Map(v, _vendaRepo.GetItensByVenda(v.idVenda))).ToList();
    }

    public List<VendaDetalheDto> GetTop(int top)
    {
        if (top <= 0)
            throw new ValidationException("Top inválido");

        var vendas = _vendaRepo.GetTop(top);

        return vendas.Select(v => Map(v, _vendaRepo.GetItensByVenda(v.idVenda))).ToList();
    }

    public void Deletar(int id)
    {
        var venda = _vendaRepo.GetById(id);

        if (venda == null)
            throw new NotFoundException("Venda não encontrada");

        _vendaRepo.Delete(id);
    }

    private static VendaDetalheDto Map(Venda venda, List<VendaItem> itens)
    {
        return new VendaDetalheDto
        {
            IdVenda = venda.idVenda,
            IdCliente = venda.idCliente,
            DataVenda = venda.dthRegistro,
            ValorTotal = venda.vlrTotal,
            Itens = itens.Select(i => new VendaItemDetalheDto
            {
                IdProduto = i.IdProduto,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario,
                ValorTotal = i.Quantidade * i.ValorUnitario
            }).ToList()
        };
    }
}
