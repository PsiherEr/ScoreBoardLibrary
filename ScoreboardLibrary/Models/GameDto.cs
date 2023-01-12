using ScoreboardLibrary.DAL.Entities;
using System.Collections.Generic; 

namespace ScoreboardLibrary.Models
{
    public class GameDto
    {
        public string Team1Name { get; set; } = null!;
        public string Team2Name { get; set; } = null!;
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public Status Status { get; set; } = Status.Finish;
    }
}
