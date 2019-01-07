using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _context.Products.Include(p => p.Category);
        }

        // Stub for pagination?
        [ProducesResponseType(200)]
        [HttpGet("getfive")]
        public IEnumerable<Product> GetFive()
        {
            return _context.Products.Include(p =>p.Category).Take(5);
        }
        
        // Stub for pagination?
        [ProducesResponseType(200)]
        [HttpGet("get20")]
        public IEnumerable<Product> Get20()
        {
            return _context.Products.Include(p =>p.Category).Take(20);
        }

        [ProducesResponseType(200)]
        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> GetById(int id)
        {
            return _context.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);
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

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
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

        [HttpGet("search")]
        public IEnumerable<Product> GetSearchedProducts([FromQuery]string name)
        {
            return _context.Products.Where(p => p.Name.Contains(name)).Take(8);
        }

        [HttpGet("filter")]
        public IEnumerable<Product> GetFilteredProducts([FromQuery]string name = null, [FromQuery]string region = null, [FromQuery]string category = null)
        {
            IEnumerable<Product> products =  _context.Products.Include(p => p.Category).OrderBy(p => p.ProductId);

            if (!string.IsNullOrEmpty(name)) {
                products = products.Where(p => p.Name.Contains(name));
            }
            
            if (!string.IsNullOrEmpty(region)) {
                products = products.Where(p => p.Region.Contains(region));
            }

            if (!string.IsNullOrEmpty(category)) {
                products = products.Where(p => p.Category.Name.Contains(category));
            }


            return products.Take(10);
        }    
    }
}