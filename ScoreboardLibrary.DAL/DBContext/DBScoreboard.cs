using ScoreboardLibrary.DAL.Entities;
using ScoreboardLibrary.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ScoreboardLibrary.DAL.DBContext
{
    public class DBScoreboard : DbContext, IDBScoreboard
    {
        public DBScoreboard(DbContextOptions<DBScoreboard> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DBScoreboard()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string? connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
