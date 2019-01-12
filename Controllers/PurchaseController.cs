using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace TripStyle.Api.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {
        private readonly TripStyleContext _context;

        public PurchaseController(TripStyleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Purchase> Get()
        {
            return _context.Purchases.ToList();
        }

         [HttpGet("{id}", Name = "GetPurchase")]
        public ActionResult<Purchase> GetById(int id)
        {
            Purchase Purchase = _context.Purchases.Find(id);
            if (Purchase == null)
            {
                return NotFound();
            }

            return Purchase;
        }

        [HttpGet("Quantity")]

        public IActionResult GetPurchase (string PurchaseName)

        {
            var listCategories = new List<string>();
            var listQuantity = new List<int>();
            var result = _context.Categories.Include(c=>c.Products).ThenInclude(p=> p.PurchaseLines).SelectMany(c=>c.Products)
            .Select(p=>new {
                CategoryName=p.Category.Name,
                Quantity = p.PurchaseLines.Count()});
            // .GroupBy(r=>r.CategoryName).Select(grp=>grp.ToList()).ToList();


            // foreach (var table in template)
            // {
            //     listCategories.Add(table.Key);
            //     listQuantity.Add(table);
            // }

            // var result = new { listCategories, listQuantity };



          // var GroupedCustomerList = result.GroupBy(r=>r.CategoryName).Select(grp=>grp.ToList()).ToList();
           //return GroupedCustomerList;
           return Ok(result);
            //var result = _context.PurchaseLines.FirstOrDefault(p=> p.Name  == PurchaseName);
          /*  var categoryid = _context.Categories.FirstOrDefault(p=>p.Name==PurchaseName).CategoryId;
            var category = _context.Products.Where(p=>p.CategoryId==categoryid).ToList().Count() ;
           // . Select(grp => new{total   = grp.Count(), CategoryName=grp.Name}).ToList();*/
           // return category;

            //return  new PurchaseApRequest{CategoryName=PurchaseName,Quantity=category};
            //Select(p=> new PurchaseApRequest(CategoryName=p.CategoryName,Quantity=p.Quantity ) );
         /*   var result = _context.Categories.Include(c=>c.Products).ThenInclude(p=> p.PurchaseLines).SelectMany(c=>c.Products)
            .Select(p=>new PurchaseApRequest
            { 
                CategoryName=p.Category.Name,
                Quantity = p.PurchaseLines.Count()}).ToList();

           var GroupedCustomerList = result.GroupBy(r=>r.CategoryName).Select(grp=>grp.ToList()).ToList();
           return GroupedCustomerList;
        }*/
        }


        [HttpPost]
        public IActionResult Create([FromBody]Purchase purchase)
        {
            if (purchase == null)
            {
                return NoContent();
            }
            _context.Purchases.Add(purchase);
            _context.SaveChanges();

            return CreatedAtRoute("GetPurchase", new { id = purchase.PurchaseId }, purchase);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Purchase purchase)
        {
            var todo = _context.Purchases.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Purchases.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetPurchase", new { id = todo.PurchaseId }, todo);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Purchases.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}