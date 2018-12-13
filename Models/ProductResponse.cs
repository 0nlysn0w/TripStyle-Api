using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace TripStyle.Api.Models
{
    public class ProductResponse
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Region { get; set; }
        public string Season { get; set; }
        public string CategoryName { get; set; }
        


    }
}

