using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TripStyle.Api.Models
{
    public class PurchaseLine
    {
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Name { get; set; }
        public string Make { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }

    }
}