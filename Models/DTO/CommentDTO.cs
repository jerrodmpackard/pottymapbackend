using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models.DTO
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Reply { get; set; }
        public DateTime PostedAt { get; set; }
    }
    public class UserCommentDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}