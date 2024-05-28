using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pottymapbackend.Models;
using pottymapbackend.Services.Context;

namespace pottymapbackend.Services
{
    public class RatingsService
    {
        private readonly DataContext _context;

        public RatingsService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRatingAsync(RatingModel newRating)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Check if the bathroom exists
                var bathroom = await _context.BathroomInfo.FindAsync(newRating.BathroomId);
                if (bathroom == null)
                {
                    return false;
                }

                // Check if the user has already rated this bathroom
                var existingRating = await _context.RatingsInfo
                    .FirstOrDefaultAsync(r => r.UserId == newRating.UserId && r.BathroomId == newRating.BathroomId);

                if (existingRating != null)
                {
                    // Rating already exists
                    return false;
                }

                // Add the new rating
                _context.RatingsInfo.Add(newRating);
                await _context.SaveChangesAsync();

                // Update the average rating for the bathroom
                var averageRating = await _context.RatingsInfo
                    .Where(r => r.BathroomId == newRating.BathroomId)
                    .AverageAsync(r => r.Rating);

                bathroom.Rating = averageRating;
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                // Rollback the transaction if any error occurs
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<double?> GetAverageRatingAsync(int bathroomId)
        {
            var bathroom = await _context.BathroomInfo.FindAsync(bathroomId);
            if (bathroom == null)
            {
                return null;
            }

            var averageRating = await _context.RatingsInfo
                .Where(r => r.BathroomId == bathroomId)
                .AverageAsync(r => r.Rating);

            return averageRating;
        }
    }
}