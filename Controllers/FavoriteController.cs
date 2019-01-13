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
    public class FavoriteController : Controller
    {
        private readonly TripStyleContext _context;

        public FavoriteController(TripStyleContext context)
        {
            _context = context;
        }

        [ProducesResponseType(200)]
        [HttpGet]
        public IEnumerable<Favorite> Get()
        {
            return _context.Favorites.Include(f => f.Product).ToList();
        }

        [HttpGet("check")]
        public ActionResult<Favorite> GetById([FromQuery]int productId, [FromQuery]int userId)
        {
            Favorite favorite = _context.Favorites.FirstOrDefault(f => f.ProductId == productId && f.UserId == userId);
            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }


        [HttpPost]
        public ActionResult Create([FromBody]Favorite favorite)
        {
            if (favorite == null)
            {
                return NoContent();
            }

            _context.Favorites.Add(favorite);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Favorites.FirstOrDefault(f => f.ProductId == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}