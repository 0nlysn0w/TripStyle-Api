using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TripStyle.Api.Models;

namespace TripStyle.Api.Controllers
{
    //TODO doesnt work yet
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly TripStyleContext _context;

        public UserController(TripStyleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(int id)
        {
            User user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (user == null)
            {
                return NoContent();
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetUser", new { UserId = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]User user)
        {
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Firstname = user.Firstname;
            todo.Lastname = user.Lastname;
            todo.Gender = user.Gender;
            todo.Email = user.Email;
            todo.Phonenumber = user.Phonenumber;
            todo.Password = user.Password;
            todo.Birthdate = user.Birthdate;

            _context.Users.Update(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetUser", new { id = todo.UserId }, todo);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Users.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}