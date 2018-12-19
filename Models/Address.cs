using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Type { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // public List<Purchase> Purchases { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public int? Current { get; set; }
    }
}