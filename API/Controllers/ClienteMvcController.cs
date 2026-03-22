using System.Net;
using System.Web.Mvc;
using TesteCamposDealer.DB;

namespace TesteCamposDealer.Controllers
{
    public class ClienteMvcController : Controller
    {
        private readonly IClienteService _service;

        public ClienteMvcController(IClienteService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var clientes = _service.GetAll();
            return View(clientes);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var cliente = _service.GetById(id);
                return View(cliente);
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
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                _service.Criar(cliente);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ViewBag.Erro = ex.Message;
                return View(cliente);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var cliente = _service.GetById(id);
                return View(cliente);
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Cliente cliente)
        {
            try
            {
                _service.Atualizar(id, cliente);
                return RedirectToAction("Index");
            }
            catch (NotFoundException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationException ex)
            {
                ViewBag.Erro = ex.Message;
                return View(cliente);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var cliente = _service.GetById(id);
                return View(cliente);
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
