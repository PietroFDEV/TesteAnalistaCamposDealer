using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using TesteCamposDealer.DB;

namespace TesteCamposDealer.Controllers
{
    [ApiKeyAuthorize]
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetClienteById")]
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

        [HttpPost]
        [Route("")]
        public IHttpActionResult Criar([FromBody] Cliente cliente)
        {
            try
            {
                var created = _service.Criar(cliente);
                return CreatedAtRoute("GetClienteById", new { id = created.idCliente }, created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Atualizar(int id, [FromBody] Cliente cliente)
        {
            try
            {
                var updated = _service.Atualizar(id, cliente);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Deletar(int id)
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
