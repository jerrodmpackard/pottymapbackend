using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class CommentModel
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int? BathroomId { get; set; }
        public string Reply { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTime PostedAt { get; set; }
        public UserModel User { get; set; }

        public CommentModel()
        {
            
        }
    }
}