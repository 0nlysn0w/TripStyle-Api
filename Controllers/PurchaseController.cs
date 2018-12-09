using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;

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

        // [HttpGet("{id}")]
        // public IQueryable<Purchase> GetBasketOfUser(int id)
        // {
         
        //     var purchase = _context.Purchases.Where(i => i.IsConfirmed == false);

        //     return purchase;
        // }

        // TODO: implement correct CRUD functions. The current one are just placeholders
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