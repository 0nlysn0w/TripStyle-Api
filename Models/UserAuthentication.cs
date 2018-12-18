using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripStyle.Api.Models
{
    public class UserAuthentication
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }

    }
}