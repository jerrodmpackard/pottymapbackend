using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class FavoritePottySpotModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string? PublishedName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }

        public FavoritePottySpotModel()
        {

        }
    }
}