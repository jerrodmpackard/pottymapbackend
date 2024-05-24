using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pottymapbackend.Models;
using pottymapbackend.Services;

namespace pottymapbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FavoriteBathroomController : ControllerBase
    {
        private readonly FavoriteBathroomService _data;

        public FavoriteBathroomController(FavoriteBathroomService data)
        {
            _data = data;
        }

        [HttpPost]
        [Route("AddFavorite")]
        public async Task<IActionResult> AddFavoriteBathroom(FavoriteBathroomModel newFavoriteBathroom)
        {
            var result = await _data.AddFavoriteBathroomAsync(newFavoriteBathroom);
            return result ? Ok("True") : StatusCode(500, "Failed to add favorite bathroom");
        }

        [HttpDelete]
        [Route("RemoveFavorite/{userId}/{bathroomId}")]
        public async Task<IActionResult> RemoveFavoriteBathroom(int userId, int bathroomId)
        {
            var result = await _data.RemoveFavoriteBathroomAsync(userId, bathroomId);
            return result ? Ok("True") : StatusCode(500, "Failed to remove favorite bathroom");
        }

        [HttpGet]
        [Route("GetFavoritesByUserID/{userId}")]
        public async Task<ActionResult<IEnumerable<BathroomModel>>> GetFavoriteBathroomsByUserID(int userId)
        {
            var favorites = await _data.GetFavoriteBathroomsAsync(userId);
            return Ok(favorites);
        }
    }
}