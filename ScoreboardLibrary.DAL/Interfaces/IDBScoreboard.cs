using ScoreboardLibrary.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoreboardLibrary.DAL.Interfaces
{
    public interface IDBScoreboard
    {
        public DbSet<Game> Games { get; set; }
    }
}
