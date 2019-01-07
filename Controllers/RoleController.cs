using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;

namespace TripStyle.Api.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly TripStyleContext _context;

        public RoleController(TripStyleContext context)
        {
            _context = context;
        }

        [ProducesResponseType(200)]
        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return _context.Roles.ToList();
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}", Name = "GetRole")]
        public ActionResult<Role> GetById(int id)
        {
            Role role = _context.Roles.Find(id);
            if  (role == null)
            {
                return NotFound();
            }

            return role;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Role role)
        {
            if (role == null)
            {
                return NoContent();
            }

            _context.Roles.Add(role);
            _context.SaveChanges();

            return CreatedAtRoute("GetRole", new { id = role.RoleId}, role);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Role role)
        {
            var todo = _context.Roles.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = role.Name;
            todo.Description = role.Description;

            _context.Roles.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetRole", new { id = todo.RoleId }, todo);

        }

       [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Roles.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}