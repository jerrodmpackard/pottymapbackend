using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pottymapbackend.Models;
using pottymapbackend.Services.Context;

namespace pottymapbackend.Services
{
    public class BathroomService
    {
        private readonly DataContext _context;

        public BathroomService(DataContext context)
        {
            _context = context;
        }
        public bool AddBathroom(BathroomModel newBathroom)
        {
            _context.Add(newBathroom);
            // Any return other than zero, we save changes. This is because this function is set up as a bool
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<BathroomModel> GetAllBathrooms()
        {
            return _context.BathroomInfo;
        }

        public bool UpdateBathroom(BathroomModel bathroomUpdate)
        {
            _context.Update<BathroomModel>(bathroomUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteBathroom(BathroomModel bathroomToDelete)
        {
            bathroomToDelete.IsDeleted = true;
            _context.Update<BathroomModel>(bathroomToDelete);
            return _context.SaveChanges() != 0;
        }
    }
}