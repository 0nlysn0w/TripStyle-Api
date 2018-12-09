using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Make { get; set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Region { get; set; }
        public string Season { get; set; }

        public ICollection<Image> Images { get; set; }

        public Category Category { get; set; }
        public ICollection<PurchaseLine> PurchaseLines { get; set; }
        // public ICollection<BasketProduct> BasketProducts { get; set; }
    }

    public class ProductImages : Product {
        public ICollection<Image> Images { get; set; }

    }

    public class ProductImage : Product {

        ProductImage(Product p) {

        }
        public Image Images { get; set; }

    }
}