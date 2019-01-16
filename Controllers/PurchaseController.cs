using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;
using TripStyle.Api.Models.Emails;

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
        
        [HttpGet("mail")]
        public string GetMail(int userId)
        {   
            // var email = from u in _context.Users where u.UserId == userId select u.Email;
            // Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%" + email);

            var email = _context.Users.Where(u => u.UserId == userId).Select(u => u.Email).ToList();

            var emailstring = email[0];

            return emailstring;            
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

        [HttpPost]
        public IActionResult Create([FromBody]Purchase purchase)
        {
            if (purchase == null)
            {
                return NoContent();
            }

            purchase.OrderDate = DateTime.UtcNow;

            _context.Purchases.Add(purchase);
            _context.SaveChanges();

            Mail.Factuur(GetMail(purchase.UserId));
            // GetMail(purchase.UserId);
            
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