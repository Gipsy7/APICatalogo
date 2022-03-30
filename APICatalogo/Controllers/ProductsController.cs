﻿using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _context.Products.AsNoTracking().ToList();
        }

        [HttpGet("{id}",Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return new CreatedAtRouteResult("GetProduct", new {id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Product product)
        {
            if (id != product.Id) return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return product;
        }
    }
}