using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TripStyle.Api.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime OrderDate { get; set; }
        //Wishlist or Order
        public bool IsWishlist { get; set; }

        public User User { get; set; }
        public ICollection<PurchaseLine> PurchaseLines { get; set; }
    }
}