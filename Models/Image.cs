using Microsoft.EntityFrameworkCore;

namespace TripStyle.Api.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Order { get; set; }

        public Product Product { get; set; }
    }
}