using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ScoreboardLibrary.DAL.Entities
{
    public enum Status
    {
        Start = 1,
        Finish = 2,
    }
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Team1Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Team2Name { get; set; }
        [Required]
        public int Team1Score { get; set; }
        [Required]
        public int Team2Score { get; set; }
        [Required]
        public Status Status { get; set; } = Status.Finish;
    }
}
