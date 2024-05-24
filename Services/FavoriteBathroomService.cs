using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pottymapbackend.Models;
using pottymapbackend.Services.Context;

namespace pottymapbackend.Services
{
    public class FavoriteBathroomService
    {
        private readonly DataContext _context;
        private readonly BathroomService _bathroomService;

        public FavoriteBathroomService(DataContext context, BathroomService bathroomService)
        {
            _context = context;
            _bathroomService = bathroomService;
        }

        public async Task<bool> AddFavoriteBathroomAsync(FavoriteBathroomModel newFavoriteBathroom)
        {
            var user = await _context.UserInfo.FindAsync(newFavoriteBathroom.UserId);
            if (user == null) return false;

            var existingFavorite = await _context.FavoriteBathroomsInfo
            .FirstOrDefaultAsync(fb => fb.UserId == newFavoriteBathroom.UserId && fb.BathroomId == newFavoriteBathroom.BathroomId);

            if (existingFavorite != null)
            {
                // Favorite bathroom already exists
                return false;
            }

            var favoriteBathroom = new FavoriteBathroomModel
            {
                ID = newFavoriteBathroom.ID,
                UserId = newFavoriteBathroom.UserId,
                BathroomId = newFavoriteBathroom.BathroomId
            };

            _context.FavoriteBathroomsInfo.Add(favoriteBathroom);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFavoriteBathroomAsync(int userId, int bathroomId)
        {
            var favoriteBathroom = await _context.FavoriteBathroomsInfo
                .SingleOrDefaultAsync(f => f.UserId == userId && f.BathroomId == bathroomId);
            if (favoriteBathroom == null) return false;

            _context.FavoriteBathroomsInfo.Remove(favoriteBathroom);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BathroomModel>> GetFavoriteBathroomsAsync(int userId)
        {
            var favoriteBathrooms = await _context.FavoriteBathroomsInfo
                .Where(f => f.UserId == userId)
                // .Include(f => f.Bathroom)
                .ToListAsync();

            List<BathroomModel> favoriteBathroomModels = new List<BathroomModel>();
            favoriteBathrooms.ForEach(bathroom => favoriteBathroomModels.Add(_bathroomService.GetBathroomByID(bathroom.ID)));

            return favoriteBathroomModels; // removed .toList() at the end of the return line here
        }

    }
}