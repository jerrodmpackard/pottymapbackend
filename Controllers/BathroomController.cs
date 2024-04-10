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
    public class BathroomController : ControllerBase
    {
        private readonly BathroomService _data;

        public BathroomController(BathroomService data)
        {
            _data = data;
        }

        // Add bathroom
        [HttpPost]
        [Route("AddBathroom")]
        public bool AddBathroom(BathroomModel newBathroom)
        {
            return _data.AddBathroom(newBathroom);
        }

        // Get bathrooms
        [HttpGet]
        [Route("GetAllBathrooms")]
        public IEnumerable<BathroomModel> GetAllBathrooms()
        {
            return _data.GetAllBathrooms();
        }

        // Update bathroom
        // Since we are updating a bathroom, we want to take in the entire BathroomModel and call it bathroomUpdate
        [HttpGet]
        [Route("UpdateBathroom")]
        public bool UpdateBathroom(BathroomModel bathroomUpdate)
        {
            return _data.UpdateBathroom(bathroomUpdate);
        }

        // Delete bathroom
        [HttpDelete]
        [Route("DeleteBathroom")]
        public bool DeleteBathroom(BathroomModel bathroomToDelete)
        {
            return _data.DeleteBathroom(bathroomToDelete);
        }
    }
}