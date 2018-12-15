using System;

namespace TripStyle.Api.Models
{

    public class ConfirmationMail :Base
    {

        public int UserId { get; set; }
        public User User { get; set; }
        public string ConfirmationToken { get; set; }
        public int AccountStatus { get; set; }
    }
}