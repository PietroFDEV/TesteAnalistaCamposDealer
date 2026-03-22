using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using TesteCamposDealer.DB;

namespace TesteCamposDealer.Controllers
{
    [ApiKeyAuthorize]
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
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
        [Route("{id:int}", Name = "GetProdutoById")]
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
        public IHttpActionResult Criar([FromBody] Produto produto)
        {
            try
            {
                var created = _service.Criar(produto);
                return CreatedAtRoute("GetProdutoById", new { id = created.idProduto }, created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Atualizar(int id, [FromBody] Produto produto)
        {
            try
            {
                var updated = _service.Atualizar(id, produto);
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

        [HttpPatch]
        [Route("{id:int}/preco")]
        public IHttpActionResult AtualizarPreco(int id, [FromBody] decimal novoPreco)
        {
            try
            {
                var updated = _service.AtualizarPreco(id, novoPreco);
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
