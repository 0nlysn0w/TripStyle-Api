using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripStyle.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Birthdate { get; set; }
        public string Password { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Token { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}