using System.Collections.Generic;
using System.Linq;
using TesteCamposDealer.DB;

public class ClienteRepository : IClienteRepository
{
    private readonly DBTesteCamposDealerDataContext _db;

    public ClienteRepository(DBTesteCamposDealerDataContext db)
    {
        _db = db;
    }

    public Cliente GetById(int id)
    {
        return _db.Cliente.FirstOrDefault(c => c.idCliente == id);
    }

    public List<Cliente> GetAll()
    {
        return _db.Cliente.ToList();
    }

    public void Add(Cliente cliente)
    {
        _db.Cliente.InsertOnSubmit(cliente);
        _db.SubmitChanges();
    }

    public void Update(Cliente cliente)
    {
        _db.SubmitChanges();
    }

    public void Delete(Cliente cliente)
    {
        _db.Cliente.DeleteOnSubmit(cliente);
        _db.SubmitChanges();
    }
}
