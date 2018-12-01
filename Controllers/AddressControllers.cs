using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;

namespace TripStyle.Api.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly TripStyleContext _context;

        public AddressController(TripStyleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Address> Get()
        {
            return _context.Addresses.ToList();
        }

        [HttpGet("{id}", Name = "GetAddress")]
        public ActionResult<Address> GetById(int id)
        {
            Address address = _context.Addresses.Find(id);
            if  (address == null)
            {
                return NotFound();
            }

            return address;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Address address)
        {
            if (address == null)
            {
                return NoContent();
            }

            _context.Addresses.Add(address);
            _context.SaveChanges();

            return CreatedAtRoute("GetAddress", new { id = address.AddressId}, address);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Address address)
        {
            var todo = _context.Addresses.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Type = address.Type;
            todo.Street = address.Street;
            todo.City = address.City;
            todo.Country = address.Country;

            _context.Addresses.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetAddress", new { id = todo.AddressId }, todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Addresses.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }      
    }   
}