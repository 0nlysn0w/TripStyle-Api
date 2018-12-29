using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;

namespace TripStyle.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly TripStyleContext _context;

        public ProductController(TripStyleContext context)
        {
            _context = context;
        }

        [ProducesResponseType(200)]
        [HttpGet]
        
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        [ProducesResponseType(200)]
        [HttpGet("{id}", Name = "GetProduct")]
        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Product product)
        {
            if (product == null)
            {
                return NoContent();
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = product.ProductId}, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Product product)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = product.Name;
            todo.Make = product.Make;
            todo.Price = product.Price;
            todo.Stock = product.Stock;
            todo.Size = product.Size;
            todo.Color = product.Color;
            todo.Region = product.Region;
            todo.Season = product.Season;
            todo.Name = product.Name;

            _context.Products.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetProduct", new { id = todo.ProductId }, todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{color}")]
        public IEnumerable<Product> Get(string color)
        {
            return _context.Products.Where(product => product.Color == color).ToList();
        }
        
        [HttpGet("Region/{searchterm}")]
        public IEnumerable<Product> Getsearch(string searchterm)
        {
            return _context.Products.Where(p=>p.Region == searchterm).OrderBy(p=>p.Price).ToList();
        }    
    }
}