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

        [HttpGet]
        public IQueryable<Product> Get()
        {
                var result = from p in _context.Products 
                join i in _context.Images
                on p.ProductId equals i.ImageId into ProIma
                select new Product{
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Name = p.Name,
                    Make = p.Make,
                    Stock =p.Stock,
                    Size = p.Size,
                    Color =p.Color,
                    Region =p.Region,
                    Season = p.Season,
                    Category =p.Category,
                    PurchaseLines = p.PurchaseLines,
                    //ReleaseYear = m.ReleaseYear,
                    Images = ProIma.ToList()
                };
                return result;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IQueryable<Product> Get(int id)
        {

            //var product = _context.Products.FirstOrDefault(p => p.ProductId ==id);
            //if (product == null)    
            //    {  
            //    return NotFound ();
            //    }
            //return new ObjectResult (product);
            
            // Product product = _context.Products.Find(id);
            // if  (product == null)
            // {
            //     return NotFound();
            // }
            // return product;
                var result = from p in _context.Products where p.ProductId == id
                join i in _context.Images
                on p.ProductId equals i.ImageId into ProIma
                select new Product{
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Name = p.Name,
                    Make = p.Make,
                    Stock =p.Stock,
                    Size = p.Size,
                    Color =p.Color,
                    Region =p.Region,
                    Season = p.Season,
                    Category =p.Category,
                    PurchaseLines = p.PurchaseLines,
                    //ReleaseYear = m.ReleaseYear,
                    Images = ProIma.ToList()
                };
    
            return result;
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
    }
}