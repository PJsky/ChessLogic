﻿using ChessLogicEntityFramework.DbContextObjects;
using ChessLogicEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace XUnitDbAccessEFTest.TestingDatabase
{
    public class ChessAppTestingContext : DbContext, IDbContext, ITestingDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGames> UserGames { get; set; }
        public DbSet<Friendship> Friendships { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbChess3;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.ID);

            modelBuilder.Entity<Game>()
                .HasKey(g => g.ID);

            modelBuilder.Entity<UserGames>()
                .HasKey(ug => new { ug.UserID, ug.GameID });

            modelBuilder.Entity<UserGames>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGames)
                .HasForeignKey(ug => ug.UserID);

            modelBuilder.Entity<UserGames>()
                .HasOne(ug => ug.Game)
                .WithMany(g => g.UserGames)
                .HasForeignKey(ug => ug.GameID);
        }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public void ClearDatabase()
        {
            this.Database.ExecuteSqlRaw("DELETE FROM [Games]");
            this.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([Games], RESEED, 0)");

            this.Database.ExecuteSqlRaw("DELETE FROM [Users]");
            this.Database.ExecuteSqlRaw("DBCC CHECKIDENT ([Users], RESEED, 0)");
        }

    }
}
