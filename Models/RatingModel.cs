using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class RatingModel
    {
        public int ID { get; set; }
        public int BathroomId { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public RatingModel()
        {
            
        }
    }
}