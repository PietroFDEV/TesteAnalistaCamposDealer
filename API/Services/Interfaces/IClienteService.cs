using System.Collections.Generic;
using TesteCamposDealer.DB;

public interface IClienteService
{
    Cliente GetById(int id);
    List<Cliente> GetAll();
    Cliente Criar(Cliente cliente);
    Cliente Atualizar(int id, Cliente cliente);
    void Deletar(int id);
}
