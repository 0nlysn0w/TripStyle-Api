using System;
using System.Collections.Generic;

namespace TripStyle.Api.Models
{
    public class UserAuthentication:Base
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }

    }
}