using ChessLogicEntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogicEntityFramework.DbContextObjects
{
    public interface IDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<UserGames> UserGames { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
