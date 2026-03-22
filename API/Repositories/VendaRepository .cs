using System.Collections.Generic;
using System.Linq;
using TesteCamposDealer.DB;

public class VendaRepository : IVendaRepository
{
    private readonly DBTesteCamposDealerDataContext _db;

    public VendaRepository(DBTesteCamposDealerDataContext db)
    {
        _db = db;
    }

    public int CriarVenda(Venda venda, List<VendaItem> itens)
    {
        using (var transaction = _db.Connection.BeginTransaction())
        {
            try
            {
                _db.Transaction = transaction;

                _db.Venda.InsertOnSubmit(venda);
                _db.SubmitChanges();

                foreach (var item in itens)
                {
                    item.IdVenda = venda.idVenda;
                    _db.VendaItem.InsertOnSubmit(item);
                }

                _db.SubmitChanges();

                transaction.Commit();

                return venda.idVenda;
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
    }

    public Venda GetById(int id)
    {
        return _db.Venda.FirstOrDefault(v => v.idVenda == id);
    }

    public List<Venda> GetAll()
    {
        return _db.Venda.ToList();
    }

    public List<VendaItem> GetItensByVenda(int idVenda)
    {
        return _db.VendaItem.Where(i => i.IdVenda == idVenda).ToList();
    }

    public List<Venda> GetByCliente(int idCliente)
    {
        return _db.Venda
            .Where(v => v.idCliente == idCliente)
            .ToList();
    }

    public List<Venda> GetTop(int top)
    {
        return _db.Venda
            .OrderByDescending(v => v.vlrTotal)
            .Take(top)
            .ToList();
    }

    public void Delete(int id)
    {
        using (var transaction = _db.Connection.BeginTransaction())
        {
            try
            {
                _db.Transaction = transaction;

                var venda = _db.Venda.FirstOrDefault(v => v.idVenda == id);
                if (venda == null)
                    return;

                var itens = _db.VendaItem.Where(i => i.IdVenda == id).ToList();
                _db.VendaItem.DeleteAllOnSubmit(itens);
                _db.Venda.DeleteOnSubmit(venda);

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
    }
}
