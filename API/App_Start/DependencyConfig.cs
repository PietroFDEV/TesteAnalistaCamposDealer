using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using TesteCamposDealer.Controllers;
using TesteCamposDealer.DB;

public static class DependencyConfig
{
    public static void Register(HttpConfiguration config)
    {
        var resolver = new SimpleDependencyResolver();
        config.DependencyResolver = resolver;
        DependencyResolver.SetResolver(resolver);
    }
}

public sealed class SimpleDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
{
    private const string ScopeKey = "_SimpleDependencyScope";

    public IDependencyScope BeginScope()
    {
        return GetOrCreateScope();
    }

    public object GetService(Type serviceType)
    {
        return GetOrCreateScope().GetService(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return GetOrCreateScope().GetServices(serviceType);
    }

    public void Dispose()
    {
    }

    private static SimpleDependencyScope GetOrCreateScope()
    {
        var context = HttpContext.Current;
        if (context == null)
            return new SimpleDependencyScope();

        if (context.Items[ScopeKey] is SimpleDependencyScope existing)
            return existing;

        var scope = new SimpleDependencyScope();
        context.Items[ScopeKey] = scope;
        return scope;
    }
}

internal sealed class SimpleDependencyScope : IDependencyScope, System.Web.Mvc.IDependencyResolver
{
    private readonly DBTesteCamposDealerDataContext _db;
    private IClienteRepository _clienteRepo;
    private IProdutoRepository _produtoRepo;
    private IVendaRepository _vendaRepo;
    private IClienteService _clienteService;
    private IProdutoService _produtoService;
    private IVendaService _vendaService;

    public SimpleDependencyScope()
    {
        _db = new DBTesteCamposDealerDataContext();
    }

    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(DBTesteCamposDealerDataContext))
            return _db;

        if (serviceType == typeof(IClienteRepository))
            return _clienteRepo ?? (_clienteRepo = new ClienteRepository(_db));

        if (serviceType == typeof(IProdutoRepository))
            return _produtoRepo ?? (_produtoRepo = new ProdutoRepository(_db));

        if (serviceType == typeof(IVendaRepository))
            return _vendaRepo ?? (_vendaRepo = new VendaRepository(_db));

        if (serviceType == typeof(IClienteService))
            return _clienteService ?? (_clienteService = new ClienteService((IClienteRepository)GetService(typeof(IClienteRepository))));

        if (serviceType == typeof(IProdutoService))
            return _produtoService ?? (_produtoService = new ProdutoService((IProdutoRepository)GetService(typeof(IProdutoRepository)), _db));

        if (serviceType == typeof(IVendaService))
            return _vendaService ?? (_vendaService = new VendaService(
                (IVendaRepository)GetService(typeof(IVendaRepository)),
                (IProdutoRepository)GetService(typeof(IProdutoRepository)),
                (IClienteRepository)GetService(typeof(IClienteRepository))
            ));

        if (serviceType == typeof(ClienteController))
            return new ClienteController((IClienteService)GetService(typeof(IClienteService)));

        if (serviceType == typeof(ProdutoController))
            return new ProdutoController((IProdutoService)GetService(typeof(IProdutoService)));

        if (serviceType == typeof(VendaController))
            return new VendaController((IVendaService)GetService(typeof(IVendaService)));

        if (serviceType == typeof(ClienteMvcController))
            return new ClienteMvcController((IClienteService)GetService(typeof(IClienteService)));

        if (serviceType == typeof(ProdutoMvcController))
            return new ProdutoMvcController((IProdutoService)GetService(typeof(IProdutoService)));

        if (serviceType == typeof(VendaMvcController))
            return new VendaMvcController((IVendaService)GetService(typeof(IVendaService)));

        return null;
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        var service = GetService(serviceType);
        if (service == null)
            return new object[0];

        return new[] { service };
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
