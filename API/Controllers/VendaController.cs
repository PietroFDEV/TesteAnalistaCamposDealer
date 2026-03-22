using System.Net;
using System.Web.Http;

namespace TesteCamposDealer.Controllers
{
    [ApiKeyAuthorize]
    [RoutePrefix("api/venda")]
    public class VendaController : ApiController
    {
        private readonly IVendaService _service;

        public VendaController(IVendaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CriarVenda([FromBody] VendaDto dto)
        {
            try
            {
                var venda = _service.CriarVenda(dto);

                return CreatedAtRoute(
                    "GetVendaById",
                    new { id = venda.IdVenda },
                    venda
                );
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetVendaById")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (NotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("cliente/{idCliente:int}")]
        public IHttpActionResult GetByCliente(int idCliente)
        {
            return Ok(_service.GetByCliente(idCliente));
        }

        [HttpGet]
        [Route("top/{top:int}")]
        public IHttpActionResult GetTop(int top)
        {
            try
            {
                return Ok(_service.GetTop(top));
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Deletar(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
