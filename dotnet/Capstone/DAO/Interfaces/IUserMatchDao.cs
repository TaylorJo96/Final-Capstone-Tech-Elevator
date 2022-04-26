using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO.Interfaces
{
    public interface IUserMatchDao
    {
        public List<UserMatch> GetUserMatchesForMatch(int leagueId);
        public void InsertMatchScore(UserMatch userMatch);
        public void SetTeeTimeForUser(UserMatch userMatch);
        public List<UserMatch> GetUserMatchesUpcoming(int userId);
        public List<UserMatch> GetUserScoresByMatch(int matchId);
        public List<UserMatch> GetMatchScoresByUser(int userId);
        public List<UserMatch> GetAllUserMatches(int matchId);

    }
}
