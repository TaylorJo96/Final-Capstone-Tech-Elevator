using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class MatchSqlDao : IMatchDao
    {
        private readonly string connectionString;

        public MatchSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Match GetMatch(int matchId)
        {
            Match match = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT match_id, match_name, league_id, start_time,
                                                     (SELECT league_name FROM leagues WHERE leagues.league_id = matches.league_id) AS league_name   
                                                    FROM matches
                                                    WHERE match_id = @match_id", conn);
                    cmd.Parameters.AddWithValue("@match_id", matchId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        match = createMatchFromReader(reader);
                        match.LeagueName = Convert.ToString(reader["league_name"]);    
                    }
                }
                return match;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public Match CreateMatch(Match match)
        {
            DateTime convertTime = DateTime.Parse(match.DateAndTime);
            int newMatchId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO matches (match_name, league_id, start_time)
                                                    OUTPUT INSERTED.match_id
                                                    VALUES (@match_name, @league_id, @start_time)", conn);
                    cmd.Parameters.AddWithValue("@match_name", match.MatchName);
                    cmd.Parameters.AddWithValue("@league_id", match.LeagueId);
                    cmd.Parameters.AddWithValue("@start_time", convertTime);

                    newMatchId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return GetMatch(newMatchId);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<User> GetUsersByLeague(int LeagueId)
        {
            List<User> usersInLeague = new List<User>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM users 
                                                        JOIN user_league ON users.user_id = user_league.user_id
                                                        WHERE league_id = @league_Id", conn);
                    cmd.Parameters.AddWithValue("@league_Id", LeagueId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(reader["user_id"]);
                        
                        user.Username = Convert.ToString(reader["username"]);
                        usersInLeague.Add(user);


                    }
                }
                return usersInLeague;

            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Match> GetMatchesByLeagueId(int leagueId)
        {
            List<Match> leagueMatches = new List<Match>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                                         SELECT * from matches WHERE league_id =@leagueId;", conn);
                    cmd.Parameters.AddWithValue("@leagueId", leagueId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Match newmatch = createMatchFromReader(reader);
                        leagueMatches.Add(newmatch);
                    }
                }
                return leagueMatches;

            }
            catch (SqlException)
            {
                throw;
            }
        }    
             

        private Match createMatchFromReader(SqlDataReader reader)
        {
            Match match = new Match();

            match.MatchId = Convert.ToInt32(reader["match_id"]);
            match.MatchName = Convert.ToString(reader["match_name"]);
            match.LeagueId = Convert.ToInt32(reader["league_id"]);
            match.DateAndTime = Convert.ToString(reader["start_time"]);
            
            return match;
        }
        
    }
}
