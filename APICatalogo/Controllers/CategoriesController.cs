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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter as categorias do banco de dados");
            }
        }

        [HttpGet("GetCategoryWithProducts")]
        public ActionResult<IEnumerable<Category>> GetCategoryWithProducts()
        {
            try
            {
                return _context.Categories.Include(x => x.Products).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter as categorias do banco de dados");
            }
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null) return NotFound($"A categoria com id={id} não foi encontrada");
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar obter as categorias do banco de dados");
            }
            
        }

        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return new CreatedAtRouteResult("GetCategory", new { id = category.Id }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar cadastrar a categoria no banco de dados");
            }
            
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Put(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.Id) return BadRequest($"Não foi possível alterar a categoria com id={id}");
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar atualizar a categoria no banco de dados");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == id);
                if (category == null) return NotFound($"A categoria com id={id} não foi encontrada");

                _context.Categories.Remove(category);
                _context.SaveChanges();
                return category;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar deletar a categoria no banco de dados");
            }

        }
    }
}
