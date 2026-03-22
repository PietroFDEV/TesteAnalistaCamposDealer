using System.Collections.Generic;
using TesteCamposDealer.DB;

public interface IVendaRepository
{
    int CriarVenda(Venda venda, List<VendaItem> itens);
    Venda GetById(int id);
    List<Venda> GetAll();
    List<VendaItem> GetItensByVenda(int idVenda);
    List<Venda> GetByCliente(int idCliente);
    List<Venda> GetTop(int top);
    void Delete(int id);
}
