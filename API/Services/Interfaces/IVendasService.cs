using System.Collections.Generic;
using TesteCamposDealer.DB;

public interface IVendaService
{
    VendaDetalheDto CriarVenda(VendaDto dto);
    VendaDetalheDto GetById(int id);
    List<VendaDetalheDto> GetAll();
    List<VendaDetalheDto> GetByCliente(int idCliente);
    List<VendaDetalheDto> GetTop(int top);
    void Deletar(int id);
}
