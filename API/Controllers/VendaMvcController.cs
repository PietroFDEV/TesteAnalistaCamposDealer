using System.Net;
using System.Web.Mvc;

namespace TesteCamposDealer.Controllers
{
    public class VendaMvcController : Controller
    {
        private readonly IVendaService _service;

        public VendaMvcController(IVendaService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var vendas = _service.GetAll();
            return View(vendas);
        }

        public ActionResult Details(int id)
        {
            try
            {
                var venda = _service.GetById(id);
                return View(venda);
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
        public ActionResult Create(VendaDto dto)
        {
            try
            {
                _service.CriarVenda(dto);
                return RedirectToAction("Index");
            }
            catch (ValidationException ex)
            {
                ViewBag.Erro = ex.Message;
                return View(dto);
            }
            catch (NotFoundException ex)
            {
                ViewBag.Erro = ex.Message;
                return View(dto);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var venda = _service.GetById(id);
                return View(venda);
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
