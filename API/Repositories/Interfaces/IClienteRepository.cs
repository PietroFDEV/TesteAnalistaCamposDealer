using System.Collections.Generic;
using TesteCamposDealer.DB;

public interface IClienteRepository
{
    Cliente GetById(int id);
    List<Cliente> GetAll();
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(Cliente cliente);
}
