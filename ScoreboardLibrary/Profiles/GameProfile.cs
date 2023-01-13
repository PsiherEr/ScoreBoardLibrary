using AutoMapper;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.Models;

namespace ScoreboardLibrary.Profiles
{
    public class GameProfile: Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameDto>();
            CreateMap<GameDto, Game>();
        }
    }
}
