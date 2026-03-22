using System.Collections.Generic;

public class VendaDto
{
    public int IdCliente { get; set; }
    public List<VendaItemDto> Itens { get; set; }
}

public class VendaItemDto
{
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
}