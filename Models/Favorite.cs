using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class Favorite
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}