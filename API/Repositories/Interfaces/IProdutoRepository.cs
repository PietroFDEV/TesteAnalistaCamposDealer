using System.Collections.Generic;
using TesteCamposDealer.DB;

public interface IProdutoRepository
{
    Produto GetById(int id);
    List<Produto> GetAll();
    void Add(Produto produto);
    void Update(Produto produto);
    void Delete(Produto produto);
}