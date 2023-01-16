using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ScoreboardLibrary.Repository
{
    public class GameFilter
    {
        public static async Task<IEnumerable<Game>> FilterByStatus(DbSet<Game> game, Status status) =>
            await game.Where(m => m.Status == status).ToArrayAsync();
        public static async Task<Game> FilterByTeamNames(DbSet<Game> game, string team1Name, string team2Name) =>
            await game.Where(m => m.Team1Name == team1Name && m.Team2Name == team2Name).FirstOrDefaultAsync();
    }
}
