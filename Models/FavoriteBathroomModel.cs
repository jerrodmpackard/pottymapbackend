using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class FavoriteBathroomModel
    {
        public int ID { get; set; }
        public int UserId { get; set; } // maybe this should be the user name
        public int BathroomId { get; set; } // maybe this should be the bathroom name

        public FavoriteBathroomModel()
        {
            
        }
    }
}