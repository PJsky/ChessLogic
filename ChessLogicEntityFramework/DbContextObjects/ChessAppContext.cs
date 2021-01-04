using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ChessLogicEntityFramework
{
    public class ChessAppContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbChess2;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<Game>()
                .HasKey(g => g.ID);
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }        
    }
}
