using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class BathroomModel
    {
        // Leaving these string properties as nullable allows them to accept a null value, but we can still require the user to enter a value for each of them on the front-end

        public int ID { get; set; }
        public string? Location { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Gender { get; set; }
        public string? Type { get; set; }
        public string? NumberOfStalls { get; set; }
        public string? WheelchairAccessibility { get; set; }
        public string? HoursOfOperation { get; set; }
        public string? OpenToPublic { get; set; }
        public string? KeyRequired { get; set; }
        public string? BabyChangingStation { get; set; }
        public string? Cleanliness { get; set; }
        public string? Safety { get; set; }
        public string? Rating { get; set; }
        public bool? IsDeleted { get; set; }

        public BathroomModel()
        {
            
        }
    }
}