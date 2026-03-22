using System.Net;
using System.Web.Mvc;
using TesteCamposDealer.DB;

namespace TesteCamposDealer.Controllers
{
    public class ProdutoMvcController : Controller
    {
        private readonly IProdutoService _service;

        public ProdutoMvcController(IProdutoService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var produtos = _service.GetAll();
            return View(produtos);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var produto = _service.GetById(id);
                return View(produto);
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            try
            {
                _service.Criar(produto);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ViewBag.Erro = ex.Message;
                return View(produto);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var produto = _service.GetById(id);
                return View(produto);
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Produto produto)
        {
            try
            {
                var atual = _service.GetById(id);

                _service.Atualizar(id, produto);

                if (atual.vlrProduto != produto.vlrProduto)
                    _service.AtualizarPreco(id, produto.vlrProduto);

                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationException ex)
            {
                ViewBag.Erro = ex.Message;
                return View(produto);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var produto = _service.GetById(id);
                return View(produto);
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            try
            {
                _service.Deletar(id);
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
