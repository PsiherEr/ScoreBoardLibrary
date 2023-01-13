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
            if (gameId > 0)
            {
                try
                {
                    var gameUpdate = await GetGame(gameId);
                    /*if (gameUpdate != null)
                    {
                        gameUpdate.Team1Score = team1Score;
                        gameUpdate.Team2Score = team2Score;
                    }*/
                    if (gameUpdate != null)
                    {
                        gameUpdate.Team1Score += team1Score;
                        gameUpdate.Team2Score += team2Score;
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
