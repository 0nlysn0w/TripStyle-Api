using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [HttpGet]
       /*  public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            Product product = _context.Products.Find(id);
            if  (product == null)
            {
                return NotFound();
            }

            return product;
        }*/


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
                    PurchaseLines = p.PurchaseLines
                    //ReleaseYear = m.ReleaseYear,
                    //Images = ProIma.ToList()
                };
                return result;
        }   

         [HttpGet("{id}", Name = "GetProduct")]
        public IQueryable<Product> Get(int id)
        {
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
                PurchaseLines = p.PurchaseLines
                //ReleaseYear = m.ReleaseYear,
                //Images = ProIma.ToList()
            };
    
            return result;
        }

        // [HttpGet]
        // // string gender, string type, 
          public IEnumerable<Product> Get(string color, string region)
         {
             if (color != null && region != null)
             {
                 return _context.Products.Where(product => product.Region == region && product.Color == color).ToList();
             }
             if(color != null && region==null)
             {
                 return _context.Products.Where(product => product.Color == color).ToList();
             }
             if(color == null && region != null)
             {
                 return _context.Products.Where(product => product.Region == region).ToList();
             }
             return _context.Products.ToList();
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
            
            
            var result = _context.Products.Where(p=>p.Region == searchterm).OrderBy(p=>p.Price).ToList();
             
           
            return result;     
 
        
        }   

        [HttpGet("name/{searchterm}")]
        public IEnumerable<ProductResponse> GetsearchName(string searchterm)
        {
            
            
            var result = _context.Categories.FirstOrDefault(p=>p.Name == searchterm);
            var products = _context.Products.Where(p=>p.CategoryId==result.CategoryId).Select (p=>new ProductResponse{Name=p.Name,Color=p.Color,Price=p.Price,Region=p.Region,Season=p.Season,Size=p.Size,CategoryName=searchterm}).OrderBy(p=>p.Price) .ToList();
           
            return products;     
            
 
        
        }    

          [HttpPost("Shoppingcart")]
          
          public IActionResult InsertIntoShoppingcart([FromBody]PurchaseLineApiRequest Request)
        {

            var result = this._context.Products.Where(P=>P.ProductId==Request.ProductId )
            .Include(p=>p.PurchaseLines).SelectMany(u=>u.PurchaseLines).FirstOrDefault(p=>p.IsConfirmed);
                       
                          
                       


            return new OkObjectResult(result);

        }



        /*  public IActionResult InsertIntoShoppingcart([FromBody]PurchaseLineApiRequest Request)
         {

             var result = _context.Products.Where(P=>P.ProductId==Request.ProductId ).ToList();

            
                   if (!result.Any()){
                       return BadRequest(" selected product doesn't exist");

                   }
             var result1 = _context.Users.Where(u=>u.UserId== Request.UserId).Include(u=>u.Purchases).SelectMany(u=>u.Purchases). FirstOrDefault(p=>!p.IsConfirmed);
             
               
            
             return new OkObjectResult(result1);

         }*/

       
            

           

         

}}