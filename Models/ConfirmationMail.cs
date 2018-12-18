using System;
using System.ComponentModel.DataAnnotations;

namespace TripStyle.Api.Models
{

    public class ConfirmationMail 
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }
        public string ConfirmationToken { get; set; }
        public int AccountStatus { get; set; }
    }
}