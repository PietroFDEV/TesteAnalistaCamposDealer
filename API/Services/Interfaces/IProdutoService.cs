using System.Collections.Generic;
using TesteCamposDealer.DB;

public interface IProdutoService
{
    Produto GetById(int id);
    List<Produto> GetAll();
    Produto Criar(Produto produto);
    Produto Atualizar(int id, Produto produto);
    Produto AtualizarPreco(int id, decimal novoPreco);
    void Deletar(int id);
}
