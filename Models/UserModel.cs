using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public bool IsModerator { get; set; }

        public UserModel()
        {
            
        }
    }
}