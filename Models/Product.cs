using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Region { get; set; }
        public string Season { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<PurchaseLine> PurchaseLines { get; set; }
    }
}