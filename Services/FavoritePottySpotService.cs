using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pottymapbackend.Models;
using pottymapbackend.Services.Context;

namespace pottymapbackend.Services
{
    public class FavoritePottySpotService
    {
        private readonly DataContext _context;

        public FavoritePottySpotService(DataContext context)
        {
            _context = context;
        }

        public bool AddFavoritePottySpot(FavoritePottySpotModel newFavoritePottySpot)
        {
            _context.Add(newFavoritePottySpot);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<FavoritePottySpotModel> GetAllFavoritePottySpots()
        {
            return _context.FavoritePottySpotInfo;
        }

        public IEnumerable<FavoritePottySpotModel> GetFavoritePottySpotsByUserId(int userId)
        {
            return _context.FavoritePottySpotInfo.Where(item => item.UserID == userId);
        }

        public IEnumerable<FavoritePottySpotModel> GetPublishedFavoritePottySpots()
        {
            return _context.FavoritePottySpotInfo.Where(item => item.IsPublished == true);
        }

        public FavoritePottySpotModel GetFavoritePottySpotsById(int id)
        {
            return _context.FavoritePottySpotInfo.SingleOrDefault(item => item.ID == id);
        }

        public bool UpdateFavoritePottySpot(FavoritePottySpotModel pottySpotToUpdate)
        {
            _context.Update<FavoritePottySpotModel>(pottySpotToUpdate);
            return _context.SaveChanges() != 0;
        }


        public bool DeleteFavoritePottySpot(FavoritePottySpotModel pottySpotToDelete)
        {
            pottySpotToDelete.IsDeleted = true;
            _context.Update<FavoritePottySpotModel>(pottySpotToDelete);
            return _context.SaveChanges() != 0;
        }

    }
}