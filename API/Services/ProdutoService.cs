using System;
using System.Collections.Generic;
using TesteCamposDealer.DB;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepo;
    private readonly DBTesteCamposDealerDataContext _db;

    public ProdutoService(IProdutoRepository produtoRepo, DBTesteCamposDealerDataContext db)
    {
        _produtoRepo = produtoRepo;
        _db = db;
    }

    public Produto GetById(int id)
    {
        var produto = _produtoRepo.GetById(id);

        if (produto == null)
            throw new NotFoundException("Produto não encontrado");

        return produto;
    }

    public List<Produto> GetAll()
    {
        return _produtoRepo.GetAll();
    }

    public Produto Criar(Produto produto)
    {
        if (string.IsNullOrWhiteSpace(produto.dscProduto))
            throw new ValidationException("Descrição do produto é obrigatória");

        if (produto.vlrProduto <= 0)
            throw new ValidationException("Preço deve ser maior que zero");

        _produtoRepo.Add(produto);
        return produto;
    }

    public Produto Atualizar(int id, Produto produto)
    {
        var existente = _produtoRepo.GetById(id);

        if (existente == null)
            throw new NotFoundException("Produto não encontrado");

        existente.dscProduto = produto.dscProduto;

        _produtoRepo.Update(existente);
        return existente;
    }

    public Produto AtualizarPreco(int id, decimal novoPreco)
    {
        var produto = _produtoRepo.GetById(id);

        if (produto == null)
            throw new NotFoundException("Produto não encontrado");

        if (novoPreco <= 0)
            throw new ValidationException("Preço inválido");

        using (var transaction = _db.Connection.BeginTransaction())
        {
            try
            {
                _db.Transaction = transaction;

                var historico = new ProdutoPrecoHistorico
                {
                    ProdutoId = produto.idProduto,
                    Preco = produto.vlrProduto,
                    DataAlteracao = DateTime.Now
                };

                _db.ProdutoPrecoHistorico.InsertOnSubmit(historico);

                produto.vlrProduto = novoPreco;

                _db.SubmitChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                _db.Transaction = null;
            }
        }

        return produto;
    }

    public void Deletar(int id)
    {
        var produto = _produtoRepo.GetById(id);

        if (produto == null)
            throw new NotFoundException("Produto não encontrado");

        _produtoRepo.Delete(produto);
    }
}
