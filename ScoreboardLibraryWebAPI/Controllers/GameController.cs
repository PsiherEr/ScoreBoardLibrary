using AutoMapper;
using ScoreboardLibrary.Repository.Interfaces;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
