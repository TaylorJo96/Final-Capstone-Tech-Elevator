using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ILeagueDao
    {
        public League CreateLeague(League league);
        public League GetLeague(int LeagueId);
        public void AddUserLeagueTable(int userId, int leagueId);
        public List<League> GetLeaguesByUserId(int userId);
        public List<Course> GetCourses();
        public List<LeagueScore> GetLeagueScores(int leagueId);
    }
}
