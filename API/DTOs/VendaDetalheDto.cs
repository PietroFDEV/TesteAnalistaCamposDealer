using System;
using System.Collections.Generic;

public class VendaDetalheDto
{
    public int IdVenda { get; set; }
    public int IdCliente { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal ValorTotal { get; set; }
    public List<VendaItemDetalheDto> Itens { get; set; }
}

public class VendaItemDetalheDto
{
    public int IdProduto { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
}
