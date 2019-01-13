using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripStyle.Api.Models;
using TripStyle.Api.Services;

namespace TripStyle.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly TripStyleContext _context;
        private IUserService _userService;

        public UserController(TripStyleContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("stats/{id}")]
        public IActionResult GetUserPurchases(int id)
        {
            var result = _context.Purchases.Include(p => p.PurchaseLines).ThenInclude(pl => pl.Product).Where(p => p.UserId == id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            Console.WriteLine("==> " + userParam);
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                // return BadRequest(new { message = "Email or password is incorrect" });
                return Unauthorized();

            return new OkObjectResult(user);
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
        public ActionResult<User> Create([FromBody]User user)
        {
            if (user == null)
            {
                return NoContent();
            }
            _context.Users.Add(user);
            _context.SaveChanges();

            // return Ok();
            return user;
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