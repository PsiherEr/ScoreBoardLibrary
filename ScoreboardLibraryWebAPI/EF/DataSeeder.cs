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
            new Game {Team1Name = "Ukraine", Team2Name = "Kazahstan", Team1Score = 2, Team2Score = 3, Status = Status.Finish }
            );
            context.SaveChanges();
        }

        
    }
}
