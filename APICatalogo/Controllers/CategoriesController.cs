using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _context.Categories.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public ActionResult<Category> Put(int id, [FromBody] Category category)
        {
            if(id != category.Id)return BadRequest();
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return category;
        }
    }
}
