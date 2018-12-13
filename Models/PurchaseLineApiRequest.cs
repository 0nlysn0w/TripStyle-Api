using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TripStyle.Api.Models
{
    public class PurchaseLineApiRequest
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string  Make { get; set; }
        public int ProductId { get; set;}
        public Product Product {get;set;}
        public User User {get ; set;}
        public int UserId {get; set;}
        
        


    }
}

