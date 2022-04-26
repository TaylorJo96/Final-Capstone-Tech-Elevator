using Capstone.DAO.Interfaces;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class UserMatchSqlDao : IUserMatchDao
    {
        private readonly string connectionString;

        public UserMatchSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void SetTeeTimeForUser(UserMatch userMatch)
        {
            DateTime teeTime = DateTime.Parse(userMatch.TeeTime);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO user_match (user_id, match_id, tee_time) 
                                                VALUES(@user_id, @match_id, @tee_time); ", conn);
                    cmd.Parameters.AddWithValue("@match_id", userMatch.MatchId);
                    cmd.Parameters.AddWithValue("@user_id", userMatch.UserId);
                    //cmd.Parameters.AddWithValue("@tee_time", userMatch.TeeTime);
                    cmd.Parameters.AddWithValue("@tee_time", teeTime);

                    cmd.ExecuteNonQuery();
                };
            }
            catch (SqlException)
            {
                throw;
            }
        }

        //method below currently set to only get user matches with no score
        public List<UserMatch> GetUserMatchesForMatch(int matchId)
        {
            List<UserMatch> userMatchesInLeague = new List<UserMatch>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT user_match.user_id, username, user_match.match_id, 
                                        score, par, tee_time, league_id, match_name
                                        FROM user_match
                                        JOIN matches ON matches.match_id = user_match.match_id
                                        JOIN users ON users.user_id = user_match.user_id
                                        WHERE user_match.match_id = @match_id AND score IS NULL", conn);
                    cmd.Parameters.AddWithValue("@match_id", matchId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserMatch userMatch = createUserMatchFromReader(reader);
                        userMatchesInLeague.Add(userMatch);
                    }
                }
                return userMatchesInLeague;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<UserMatch> GetAllUserMatches(int matchId)
        {
            List<UserMatch> allUserMatches = new List<UserMatch>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT user_match.user_id, username, user_match.match_id, 
                                        score, par, tee_time, league_id, match_name
                                        FROM user_match
                                        JOIN matches ON matches.match_id = user_match.match_id
                                        JOIN users ON users.user_id = user_match.user_id
                                        WHERE user_match.match_id = @match_id", conn);
                    cmd.Parameters.AddWithValue("@match_id", matchId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserMatch userMatch = createUserMatchFromReader(reader);
                        allUserMatches.Add(userMatch);
                    }
                }
                return allUserMatches;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<UserMatch> GetUserMatchesUpcoming(int userId)
        {
            List<UserMatch> userMatchesInLeague = new List<UserMatch>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT user_match.user_id, username, user_match.match_id, 
                                        score, tee_time, league_id, match_name
                                        FROM user_match
                                        JOIN matches ON matches.match_id = user_match.match_id
                                        JOIN users ON users.user_id = user_match.user_id
                                        WHERE user_match.user_id = @user_id AND score IS NULL", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserMatch userMatch = createUserMatchFromReader(reader);
                        userMatchesInLeague.Add(userMatch);
                    }
                }
                return userMatchesInLeague;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void InsertMatchScore(UserMatch userMatch)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE user_match 
                                                SET score = @score, par = @par
                                                WHERE match_id = @match_id AND user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@score", userMatch.Score);
                    cmd.Parameters.AddWithValue("@par", userMatch.Par);
                    cmd.Parameters.AddWithValue("@match_id", userMatch.MatchId);
                    cmd.Parameters.AddWithValue("@user_id", userMatch.UserId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<UserMatch> GetUserScoresByMatch(int matchId)
        {
            List<UserMatch> matchScores = new List<UserMatch>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT user_match.user_id, username, score, par, (score - par) AS total, tee_time
                                                FROM user_match
                                                JOIN users ON users.user_id = user_match.user_id
                                                WHERE match_id = @match_id AND score IS NOT NULL
                                                ORDER BY total ASC", conn);
                    cmd.Parameters.AddWithValue("@match_id", matchId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserMatch userMatchScore = createUserScoresFromReader(reader);
                        userMatchScore.Username = Convert.ToString(reader["username"]);
                        matchScores.Add(userMatchScore);
                    }
                }
                return matchScores;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<UserMatch> GetMatchScoresByUser(int userId)
        {
            List<UserMatch> userScores = new List<UserMatch>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT user_id, match_name, score, par, (score - par) AS total, tee_time
                                                FROM user_match
                                                JOIN matches ON matches.match_id = user_match.match_id
                                                WHERE user_id = @user_id AND score IS NOT NULL
                                                ORDER BY tee_time ASC", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserMatch userScore = createUserScoresFromReader(reader);
                        userScore.MatchName = Convert.ToString(reader["match_name"]);
                        userScores.Add(userScore);
                    }
                }
                return userScores;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private UserMatch createUserScoresFromReader(SqlDataReader reader)
        {
            UserMatch userMatch = new UserMatch();

            userMatch.UserId = Convert.ToInt32(reader["user_id"]);                      
            userMatch.Score = Convert.ToInt32(reader["score"]);
            userMatch.Par = Convert.ToInt32(reader["par"]);
            userMatch.Total = Convert.ToInt32(reader["total"]);
            userMatch.TeeTime = Convert.ToString(reader["tee_time"]);
           
            return userMatch;
        }

        private UserMatch createUserMatchFromReader(SqlDataReader reader)
        {
            UserMatch userMatch = new UserMatch();

            userMatch.MatchId = Convert.ToInt32(reader["match_id"]);
            userMatch.MatchName = Convert.ToString(reader["match_name"]);
            userMatch.UserId = Convert.ToInt32(reader["user_id"]);
            userMatch.Username = Convert.ToString(reader["username"]);
            userMatch.TeeTime = Convert.ToString(reader["tee_time"]);
            
            return userMatch;
        }
    }
}
