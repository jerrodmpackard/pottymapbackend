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
    public class RatingsController : ControllerBase
    {
        private readonly RatingsService _ratingsService;

        public RatingsController(RatingsService ratingsService)
        {
            _ratingsService = ratingsService;
        }

        [HttpPost]
        [Route("AddRating")]
        public async Task<IActionResult> AddRating(RatingModel newRating)
        {
            var result = await _ratingsService.AddRatingAsync(newRating);
            if (!result)
            {
                return BadRequest("Failed to add rating.");
            }
            return Ok("True");
        }

        [HttpGet]
        [Route("GetAverageRating/{bathroomId}")]
        public async Task<IActionResult> GetAverageRating(int bathroomId)
        {
            var averageRating = await _ratingsService.GetAverageRatingAsync(bathroomId);
            if (averageRating == null)
            {
                return NotFound("Bathroom not found.");
            }
            return Ok(averageRating);
        }
    }
}