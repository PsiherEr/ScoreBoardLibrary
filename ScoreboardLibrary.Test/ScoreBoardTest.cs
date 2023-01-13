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
    public class ScoreBoardTest
    {
        [Fact]
        public async void MustContainsGame_returnTrue()
        {
            Game game = new Game { Team1Name = "Ukraine", Team2Name = "Kazahstan", Team1Score = 2, Team2Score = 3, Status = Status.Start };
            var sut = new Moq.Mock<IScoreboardRepository>();
            sut.Setup(x => x.GetGameByStatus(It.IsAny<Status>())).ReturnsAsync(new List<Game> { game });

            Assert.Contains(game, await sut.Object.GetGameByStatus(Status.Start));
        }
        [Fact]
        public async void GetGameByStatus_MustDoesNotContainThroughStatus()
        {
            //Arrange
            Game game = new Game { Team1Name = "Ukraine", Team2Name = "Kazahstan", Team1Score = 2, Team2Score = 3, Status = Status.Start };

            var sut = new Moq.Mock<IScoreboardRepository>();

            //Act
            sut.Setup(x => x.GetGameByStatus(It.IsAny<Status>()))
               .ReturnsAsync(new List<Game>
               { new Game { Team1Name = "Ukraine", Team2Name = "Kazahstan", Team1Score = 2, Team2Score = 3, Status = Status.Start }});

            //Assert
            Assert.DoesNotContain(game, await sut.Object.GetGameByStatus(Status.Finish));
        }
        [Fact]
        public async void StartTheGame_MustNullInvalidArgumentException()
        {
            //Arrange
            var sut = new Moq.Mock<IScoreboardRepository>();

            //Act
            sut.Setup(x => x.StartTheGame(-1))
              .ThrowsAsync(new InvalidArgumentException());

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => sut.Object.StartTheGame(-1));
        }
        [Fact]
        public async void UpdateTheScore_MustNullInvalidArgumentException()
        {
            //Arrange
            var sut = new Moq.Mock<IScoreboardRepository>();

            //Act
            sut.Setup(x => x.UpdateTheScore(1, -5, 2))
              .ThrowsAsync(new InvalidArgumentException());

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => sut.Object.UpdateTheScore(1, -5, 2));
        }
        [Fact]
        public async void EndTheScore_MustInvalidArgumentException()
        {
            //Arrange
            var sut = new Moq.Mock<IScoreboardRepository>();

            //Act
            sut.Setup(x => x.EndTheGame(-1))
              .ThrowsAsync(new InvalidArgumentException());

            //Assert
            await Assert.ThrowsAsync<InvalidArgumentException>(() => sut.Object.EndTheGame(-1));
        }
    }
}