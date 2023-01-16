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
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetLastGame();
        Task<IEnumerable<Game>> GetGameByStatus(Status status);
        Task<IEnumerable<Game>> SortGames();
        Task<Game> GetGameByTeamNames(string team1Name, string team2Name);
        Task StartTheGame(int gameId);
        Task UpdateTheScore(int gameId, int team1Score, int team2Score);
        Task EndTheGame(int gameId);
        Task InputData(string team1Name, string team2Name, int team1Score, int team2Score, Status status);
        Task RemoveData(int id);
        Task<Game> GetGame(int gameId);
        Task<bool> SaveChangesAsync();
    }
}
