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
    public class FavoritePottySpotController : ControllerBase
    {
        private readonly FavoritePottySpotService _data;

        public FavoritePottySpotController(FavoritePottySpotService data)
        {
            _data = data;
        }


        // Add Favorite Potty Spot
        [HttpPost]
        [Route("AddFavoritePottySpot")]
        public bool AddFavoritePottySpot(FavoritePottySpotModel newFavoritePottySpot)
        {
            return _data.AddFavoritePottySpot(newFavoritePottySpot);
        }


        // Get all Favorite Potty Spots
        [HttpGet]
        [Route("GetAllFavoritePottySpots")]
        public IEnumerable<FavoritePottySpotModel> GetAllFavoritePottySpots()
        {
            return _data.GetAllFavoritePottySpots();
        }


        [HttpGet]
        [Route("GetFavoritePottySpotsByUserId/{userId}")]
        public IEnumerable<FavoritePottySpotModel> GetFavoritePottySpotsByUserId(int userId)
        {
            return _data.GetFavoritePottySpotsByUserId(userId);
        }


        [HttpGet]
        [Route("GetPublishedFavoritePottySpots")]
        public IEnumerable<FavoritePottySpotModel> GetPublishedFavoritePottySpots()
        {
            return _data.GetPublishedFavoritePottySpots();
        }


        [HttpGet]
        [Route("GetFavoritePottySpotsById/{id}")]
        public FavoritePottySpotModel GetFavoritePottySpotsById(int id)
        {
            return _data.GetFavoritePottySpotsById(id);
        }


        // Update Favorite Potty Spot
        [HttpPut]
        [Route("UpdateFavoritePottySpot")]
        public bool UpdateFavoritePottySpot(FavoritePottySpotModel pottySpotToUpdate)
        {
            return _data.UpdateFavoritePottySpot(pottySpotToUpdate);
        }


        // Delete Favorite Potty Spot
        [HttpDelete]
        [Route("DeleteFavoritePottySpot")]
        public bool DeleteFavoritePottySpot(FavoritePottySpotModel pottySpotToDelete)
        {
            return _data.DeleteFavoritePottySpot(pottySpotToDelete);
        }
    }
}