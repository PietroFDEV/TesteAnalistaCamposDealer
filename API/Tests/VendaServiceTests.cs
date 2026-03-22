using System;
using System.Collections.Generic;
using Xunit;
using TesteCamposDealer.DB;

public class VendaServiceTests
{
    [Fact]
    public void Deve_Calcular_Total_Corretamente()
    {
        var vendaRepo = new FakeVendaRepository();
        var produtoRepo = new FakeProdutoRepository();
        var clienteRepo = new FakeClienteRepository();

        produtoRepo.Add(new Produto { idProduto = 1, dscProduto = "P1", vlrProduto = 10m });
        clienteRepo.Add(new Cliente { idCliente = 1, nomeCliente = "C1", endereco = "R1", dthRegistro = DateTime.Now });

        var service = new VendaService(vendaRepo, produtoRepo, clienteRepo);

        var dto = new VendaDto
        {
            IdCliente = 1,
            Itens = new List<VendaItemDto>
            {
                new VendaItemDto { IdProduto = 1, Quantidade = 2 }
            }
        };

        var result = service.CriarVenda(dto);

        Assert.Equal(20m, result.ValorTotal);
        Assert.Single(result.Itens);
        Assert.Equal(10m, result.Itens[0].ValorUnitario);
        Assert.Equal(20m, result.Itens[0].ValorTotal);
    }

    [Fact]
    public void Deve_Lancar_Erro_Se_Cliente_Nao_Existe()
    {
        var vendaRepo = new FakeVendaRepository();
        var produtoRepo = new FakeProdutoRepository();
        var clienteRepo = new FakeClienteRepository();

        var service = new VendaService(vendaRepo, produtoRepo, clienteRepo);

        var dto = new VendaDto
        {
            IdCliente = 99,
            Itens = new List<VendaItemDto>
            {
                new VendaItemDto { IdProduto = 1, Quantidade = 1 }
            }
        };

        Assert.Throws<NotFoundException>(() => service.CriarVenda(dto));
    }

    [Fact]
    public void Deve_Lancar_Erro_Se_Produto_Nao_Existe()
    {
        var vendaRepo = new FakeVendaRepository();
        var produtoRepo = new FakeProdutoRepository();
        var clienteRepo = new FakeClienteRepository();

        clienteRepo.Add(new Cliente { idCliente = 1, nomeCliente = "C1", endereco = "R1", dthRegistro = DateTime.Now });

        var service = new VendaService(vendaRepo, produtoRepo, clienteRepo);

        var dto = new VendaDto
        {
            IdCliente = 1,
            Itens = new List<VendaItemDto>
            {
                new VendaItemDto { IdProduto = 999, Quantidade = 1 }
            }
        };

        Assert.Throws<NotFoundException>(() => service.CriarVenda(dto));
    }

    private sealed class FakeVendaRepository : IVendaRepository
    {
        private Venda _venda;
        private List<VendaItem> _itens;
        private int _nextId = 1;

        public int CriarVenda(Venda venda, List<VendaItem> itens)
        {
            venda.idVenda = _nextId++;
            _venda = venda;
            _itens = itens;
            return venda.idVenda;
        }

        public Venda GetById(int id)
        {
            return _venda != null && _venda.idVenda == id ? _venda : null;
        }

        public List<Venda> GetAll()
        {
            return _venda == null ? new List<Venda>() : new List<Venda> { _venda };
        }

        public List<VendaItem> GetItensByVenda(int idVenda)
        {
            return _itens ?? new List<VendaItem>();
        }

        public List<Venda> GetByCliente(int idCliente)
        {
            return _venda != null && _venda.idCliente == idCliente ? new List<Venda> { _venda } : new List<Venda>();
        }

        public List<Venda> GetTop(int top)
        {
            return GetAll();
        }

        public void Delete(int id)
        {
            _venda = null;
            _itens = null;
        }
    }

    private sealed class FakeProdutoRepository : IProdutoRepository
    {
        private readonly Dictionary<int, Produto> _produtos = new Dictionary<int, Produto>();

        public void Add(Produto produto)
        {
            _produtos[produto.idProduto] = produto;
        }

        public Produto GetById(int id)
        {
            return _produtos.TryGetValue(id, out var p) ? p : null;
        }

        public List<Produto> GetAll()
        {
            return new List<Produto>(_produtos.Values);
        }

        public void Update(Produto produto)
        {
        }

        public void Delete(Produto produto)
        {
        }
    }

    private sealed class FakeClienteRepository : IClienteRepository
    {
        private readonly Dictionary<int, Cliente> _clientes = new Dictionary<int, Cliente>();

        public void Add(Cliente cliente)
        {
            _clientes[cliente.idCliente] = cliente;
        }

        public Cliente GetById(int id)
        {
            return _clientes.TryGetValue(id, out var c) ? c : null;
        }

        public List<Cliente> GetAll()
        {
            return new List<Cliente>(_clientes.Values);
        }

        public void Update(Cliente cliente)
        {
        }

        public void Delete(Cliente cliente)
        {
        }
    }
}
