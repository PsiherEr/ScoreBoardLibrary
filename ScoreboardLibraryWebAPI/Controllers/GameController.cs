using AutoMapper;
using ScoreboardLibrary.Repository.Interfaces;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScoreboardLibrary.DAL.DBContext;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ScoreboardLibraryWebAPI.Controllers
{
    [Route("api/Game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IScoreboardRepository _repository;
        private readonly IMapper _mapper;

        public GameController(IScoreboardRepository repository,
            IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{status}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame(Status status)
        {
            var gameEntities = await _repository.GetGameByStatus(status);
            return Ok(gameEntities);
        }

        [HttpPatch("{id}/{status}")]
        public async Task<ActionResult<Game>> StatusChangeGame(int id, Status status)
        {
            if (status == Status.Finish)
            {
                await _repository.EndTheGame(id);
            } else
            {
                await _repository.StartTheGame(id);
                await _repository.UpdateTheScore(id, 0, 0);
            }
            await _repository.SaveChangesAsync();
            var gameEntity = await _repository.GetGame(id);
            return Ok(gameEntity);
        }

        [HttpPatch("{id}/{team1Score}/{team2Score}")]
        public async Task<ActionResult<Game>> UpdateGame(int id, int team1Score, int team2Score, Status status)
        {
            await _repository.UpdateTheScore(id, team1Score, team2Score);
            if (status == Status.Start && team1Score == 0 && team2Score == 0)
            {
                await _repository.StartTheGame(id);
            }
            else
            {
                await _repository.EndTheGame(id);
            }
            await _repository.SaveChangesAsync();
            var gameEntity = await _repository.GetGame(id);
            return Ok(gameEntity);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<IEnumerable<Game>>> RemoveData(int id)
        {
            if (await _repository.GetGame(id) != null)
            {
                await _repository.RemoveData(id);
                await _repository.SaveChangesAsync();
                var gameEntities = await _repository.GetAllGames();
                return Ok(gameEntities);
            }
            else
            {
                throw new ArgumentNullException(nameof(id));
            }
            
        }

        [HttpPatch("{team1Name}/{team2Name}/{team1Score}/{team2Score}/{status}")]
        public async Task<ActionResult<Game>> InputData(string team1Name, string team2Name, int team1Score, int team2Score, Status status)
        {
            await _repository.InputData(team1Name, team2Name, team2Score, team1Score, status);
            await _repository.SaveChangesAsync();
            var gameEntity = await _repository.GetGameByTeamNames(team1Name, team2Name);
            return Ok(gameEntity);
        }
    }
}
