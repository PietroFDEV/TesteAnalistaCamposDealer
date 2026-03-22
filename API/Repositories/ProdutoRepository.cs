using System.Collections.Generic;
using System.Linq;
using TesteCamposDealer.DB;

public class ProdutoRepository : IProdutoRepository
{
    private readonly DBTesteCamposDealerDataContext _db;

    public ProdutoRepository(DBTesteCamposDealerDataContext db)
    {
        _db = db;
    }

    public Produto GetById(int id)
        => _db.Produto.FirstOrDefault(x => x.idProduto == id);

    public List<Produto> GetAll()
        => _db.Produto.ToList();

    public void Add(Produto produto)
    {
        _db.Produto.InsertOnSubmit(produto);
        _db.SubmitChanges();
    }

    public void Update(Produto produto)
    {
        _db.SubmitChanges();
    }

    public void Delete(Produto produto)
    {
        _db.Produto.DeleteOnSubmit(produto);
        _db.SubmitChanges();
    }
}