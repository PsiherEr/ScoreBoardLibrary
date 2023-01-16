using Couchbase.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScoreboardLibrary.Models;
using ScoreboardLibrary.Repository;
using ScoreboardLibrary.Repository.Interfaces;
using ScoreboardLibrary.DAL.Interfaces;
using ScoreboardLibrary.DAL.DBContext;
using ScoreboardLibrary.DAL.Entities;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ScoreboardLibrary.Repository
{
    public class ScoreboardRepository : IScoreboardRepository
    {
        private readonly DBScoreboard _context;

        public ScoreboardRepository(DBScoreboard context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task StartTheGame(int gameId)
        {
            if(gameId > 0)
            {
                try
                {
                    var gameStart = await GetGame(gameId);
                    if(gameStart.Status == Status.Finish && gameStart != null)
                    {
                        gameStart.Status = Status.Start;
                    }
                }
                catch(Exception e)
                {
                    throw new InvalidArgumentException(e.Message);
                }
            }
            else
            {
                throw new InvalidArgumentException();
            }
        }
        public async Task UpdateTheScore(int gameId, int team1Score, int team2Score)
        {
            if (gameId > 0 && team1Score >= 0 && team2Score >= 0)
            {
                try
                {
                    var gameUpdate = await GetGame(gameId);
                    if (gameUpdate != null)
                    {
                        gameUpdate.Team1Score = team1Score;
                        gameUpdate.Team2Score = team2Score;
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidArgumentException(e.Message);
                }
            }
            else
            {
                throw new InvalidArgumentException();
            }
        }

        public async Task EndTheGame(int gameId)
        {
            if (gameId > 0)
            {
                try
                {
                    var gameStart = await GetGame(gameId);
                    if (gameStart.Status == Status.Start && gameStart != null)
                    {
                        gameStart.Status = Status.Finish;
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidArgumentException(e.Message);
                }
            }
            else
            {
                throw new InvalidArgumentException();
            }
        }

        public async Task InputData(string team1, string team2, int team1Score, int team2Score, Status status)
        {
            if (team1 != null && team2 != null)
            {
                try
                {
                    var game = new Game
                    {
                        Team1Name = team1,
                        Team2Name = team2,
                        Team1Score = team1Score,
                        Team2Score = team2Score,
                        Status = status
                    };
                    await _context.Games.AddAsync(game);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new InvalidArgumentException(e.Message);
                }
            }
            else
            {
                throw new InvalidArgumentException();
            }

        }

        public async Task RemoveData(int gameId)
        {
            if (gameId > 0)
            {
                try
                {
                    var temp = await GetGame(gameId);
                    _context.Games.Remove(temp);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw new InvalidArgumentException(e.Message);
                }
            }
            else
            {
                throw new InvalidArgumentException();
            }
        }
        
        public async Task<IEnumerable<Game>> GetAllGames()
        {
            try
            {
                var games = await _context.Games.ToListAsync();
                return games;
            }
            catch (Exception e)
            {
                throw new InvalidArgumentException(e.Message);
            }
        }
        
        public async Task<Game> GetLastGame()
        {
            try
            {
                var game = await _context.Games.LastOrDefaultAsync();
                return game;
            }
            catch (Exception e)
            {
                throw new InvalidArgumentException(e.Message);
            }         {
        }
        }

        public async Task<IEnumerable<Game>> GetGameByStatus(Status status)
        {
            try
            {
                return await GameFilter.FilterByStatus(_context.Games, status);
            }
            catch (Exception e)
            {
                throw new InvalidArgumentException(e.Message);
            }
        }
        
        public async Task<Game> GetGameByTeamNames(string team1Name, string team2Name)
        {
            return await GameFilter.FilterByTeamNames(_context.Games, team1Name, team2Name);
        }
        
        public async Task<Game> GetGame(int gameId)
        {           
            return await _context.Games
                  .Where(c => c.Id == gameId).FirstOrDefaultAsync();
        }
        
        public async Task<bool> SaveChangesAsync()

        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
