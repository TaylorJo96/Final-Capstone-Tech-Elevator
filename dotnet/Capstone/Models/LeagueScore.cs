using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class LeagueScore
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Score { get; set; }
        public int Par { get; set; }
        public int TotalScore { get; set; }
    }
}
