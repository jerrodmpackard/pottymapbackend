using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pottymapbackend.Models;

namespace pottymapbackend.Services.Context
{
    public class DataContext : DbContext
    {
        public DbSet<UserModel> UserInfo { get; set; }
        public DbSet<BathroomModel> BathroomInfo { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        // create function to build out our table in database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}