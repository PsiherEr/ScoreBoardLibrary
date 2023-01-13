using ScoreboardLibrary.DAL.DBContext;
using ScoreboardLibrary.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ScoreboardLibraryWebAPI.EF
    //дописать
{
    public class DataSeeder
    {
        private readonly DBScoreboard context;

        public DataSeeder(DBScoreboard context)
        {
            this.context = context;
        }

        public void Initial()
        {
            context.AddRange(
            new Game {Team1Name = "Ukraine", Team2Name = "Kazahstan", Team1Score = 2, Team2Score = 3, Status = Status.Finish },
            new Game { Team1Name = "England", Team2Name = "Scotland", Team1Score = 0, Team2Score = 1, Status = Status.Start },
            new Game { Team1Name = "Germany", Team2Name = "France", Team1Score = 1, Team2Score = 0, Status = Status.Start },
            new Game { Team1Name = "Italy", Team2Name = "Spain", Team1Score = 0, Team2Score = 0, Status = Status.Start },
            new Game { Team1Name = "Poland", Team2Name = "Belarus", Team1Score = 0, Team2Score = 2, Status = Status.Finish }
            );
            context.SaveChanges();
        }

        
    }
}
