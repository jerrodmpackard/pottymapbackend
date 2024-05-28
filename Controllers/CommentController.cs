using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pottymapbackend.Models.DTO;
using pottymapbackend.Services;

namespace pottymapbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _data;
        public CommentController(CommentService data)
        {
            _data = data;
        }


        [HttpGet]
        [Route("GetCommentById/{id}")]
        public IActionResult GetCommentById(int id)
        {
            return _data.GetCommentById(id);
        }


        // Get List of Users who Commented 
        [HttpGet]
        [Route("GetUserComments/{userId}")]
        public async Task<ActionResult<List<UserCommentDTO>>> GetUserComments(int userId)
        {
            return await _data.GetUserComments(userId);
        }


        // Get Replies from Bathroom
        [HttpGet]
        [Route("GetBathroomReplies/{bathroomId}")]
        public async Task<IActionResult> GetBathroomReplies(int bathroomId)
        {
            return await _data.GetBathroomReplies(bathroomId);
        }


        // Get Replies from Comments
        [HttpGet]
        [Route("GetRepliesFromComment/{commentId}")]
        public async Task<IActionResult> GetRepliesFromComment(int commentId)
        {
            return await _data.GetRepliesFromComment(commentId);
        }


        // Add Reply To Bathroom
        [HttpPost]
        [Route("AddCommentForBathroom/{bathroomId}/{userId}")]
        public async Task<IActionResult> AddCommentForBathroom(int bathroomId, [FromBody] string reply, int userId)
        {
            return await _data.AddCommentForBathroom(bathroomId, reply, userId);
        }


        // Add Reply To Comment]
        [HttpPost]
        [Route("AddReplyForComment/{commentId}/{userId}")]
        public async Task<IActionResult> AddReplyForComment(int commentId, int userId, [FromBody] string reply)
        {
            return await _data.AddReplyForComment(commentId, userId, reply);
        }


        // Updating Top Level Reply
        [HttpPut]
        [Route("UpdateReplyFromBathroom/{commentId}")]
        public async Task<IActionResult> UpdateReplyFromBathroom(int commentId, [FromBody] string reply)
        {
            return await _data.UpdateReplyFromBathroom(commentId, reply);
        }


        [HttpDelete]
        [Route("DeleteComment/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            return await _data.DeleteComment(commentId);
        }
    }
}