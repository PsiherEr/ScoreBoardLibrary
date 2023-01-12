using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ScoreboardLibrary.Repository;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.Models;

namespace ScoreboardLibrary.Repository.Interfaces
{
    public interface IScoreboardRepository 
    {
        Task<IEnumerable<Game>> GetGameByStatus(Status status);
        Task StartTheGame(int gameId);
        Task UpdateTheScore(int gameId, int team1Score, int team2Score);
        Task EndTheGame(int gameId);

        Task<Game> GetGame(int gameId);
        Task<bool> SaveChangesAsync();
    }
}
