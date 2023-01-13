using ScoreboardLibrary.Repository.Interfaces;
using Couchbase.Core.Exceptions;
using Moq;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.DAL.DBContext;
using ScoreboardLibrary.Repository;
using Microsoft.EntityFrameworkCore;

namespace ScoreboardLibrary.Test
{
    public class UnitTest1
    {
        private readonly IScoreboardRepository _repository;
        [Fact]
        public async void UpdateTeamScore_returntrue()
        {
            await _repository.UpdateTheScore(1, 0, 0);
        }
    }
}