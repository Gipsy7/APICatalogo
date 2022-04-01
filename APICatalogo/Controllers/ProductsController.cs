using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return _context.Products.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter o produto no banco de dados");

            }
        }

        [HttpGet("{id}",Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var product = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (product == null) return NotFound($"O produto de id={id} não foi encontrado");
                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter o produto no banco de dados");
            }

        }

        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar cadastrar o produto no banco de dados");
            }

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Product product)
        {
            try
            {
                if (id != product.Id) return BadRequest($"Não foi possível alterar o produto com id={id}");

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar o produto no banco de dados");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == id);

                if (product == null) return NotFound($"O produto de id={id} não foi encontrado");

                _context.Products.Remove(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar deletar o produto no banco de dados");
            }

        }
    }
}
