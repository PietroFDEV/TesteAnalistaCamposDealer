using System;
using System.Collections.Generic;
using TesteCamposDealer.DB;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repo;

    public ClienteService(IClienteRepository repo)
    {
        _repo = repo;
    }

    public Cliente GetById(int id)
    {
        var cliente = _repo.GetById(id);

        if (cliente == null)
            throw new NotFoundException("Cliente não encontrado");

        return cliente;
    }

    public List<Cliente> GetAll()
    {
        return _repo.GetAll();
    }

    public Cliente Criar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.nomeCliente))
            throw new ValidationException("Nome é obrigatório");

        if (cliente.dthRegistro == default(DateTime))
            cliente.dthRegistro = DateTime.Now;

        _repo.Add(cliente);
        return cliente;
    }

    public Cliente Atualizar(int id, Cliente cliente)
    {
        var existente = _repo.GetById(id);

        if (existente == null)
            throw new NotFoundException("Cliente não encontrado");

        existente.nomeCliente = cliente.nomeCliente;
        existente.endereco = cliente.endereco;

        _repo.Update(existente);
        return existente;
    }

    public void Deletar(int id)
    {
        var cliente = _repo.GetById(id);

        if (cliente == null)
            throw new NotFoundException("Cliente não encontrado");

        _repo.Delete(cliente);
    }
}
