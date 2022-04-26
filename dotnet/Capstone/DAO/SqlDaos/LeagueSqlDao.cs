using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class LeagueSqlDao : ILeagueDao
    {
        private readonly string connectionString;

        public LeagueSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        
        public League GetLeague(int leagueId)
        {
            League league = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT league_id, league_name, organizer_id, leagues.course_id, course_name,
                                                (SELECT username FROM users WHERE users.user_id = leagues.organizer_id)AS organizer_username
                                                FROM leagues JOIN courses ON courses.course_id = leagues.course_id
                                                WHERE league_id = @league_id", conn);
                    cmd.Parameters.AddWithValue("@league_id", leagueId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        league = createLeagueFromReader(reader);
                    }
                }
                return league;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public League CreateLeague(League league)
        {
            int newLeagueId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO leagues(league_name, organizer_id, course_id)
                                                OUTPUT INSERTED.league_id
                                                VALUES (@league_name, @organizer_id, @course_id);", conn);
                    cmd.Parameters.AddWithValue("@league_name", league.LeagueName);
                    cmd.Parameters.AddWithValue("@organizer_id", league.OrganizerId);
                    cmd.Parameters.AddWithValue("@course_id", league.LeagueCourse.CourseId);

                    newLeagueId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return GetLeague(newLeagueId);
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void AddUserLeagueTable(int userId, int leagueId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO user_league(user_id, league_id)
                                                VALUES (@user_id, @league_id);", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@league_id", leagueId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<League> GetLeaguesByUserId(int userId)
        {
            List<League> userLeagues = new List<League>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT leagues.league_id, league_name, organizer_id,
                                            (SELECT username FROM users WHERE users.user_id = leagues.organizer_id)AS organizer_username,
                                            leagues.course_id, courses.course_name
                                            FROM leagues
                                            JOIN courses ON courses.course_id = leagues.course_id
                                            JOIN user_league on leagues.league_id = user_league.league_id
                                            WHERE user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        League newLeague = createLeagueFromReader(reader);
                        userLeagues.Add(newLeague);
                    }
                }
                return userLeagues;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<Course> GetCourses()
        {
            List<Course> courseList = new List<Course>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM courses", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Course course = new Course();
                        course.CourseId = Convert.ToInt32(reader["course_id"]);
                        course.CourseName = Convert.ToString(reader["course_name"]);
                        courseList.Add(course);
                    }
                }
                return courseList;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public List<LeagueScore> GetLeagueScores(int leagueId)
        {
            List<LeagueScore> leagueScores = new List<LeagueScore>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT user_match.user_id, username, SUM(score) AS total_score, 
                                                SUM(par) AS total_par, (SUM(score) - SUM(par)) AS score_vs_par
                                                FROM user_match
                                                JOIN users ON users.user_id = user_match.user_id
                                                JOIN matches ON matches.match_id = user_match.match_id
                                                WHERE league_id = @league_id AND score IS NOT NULL
                                                GROUP BY user_match.user_id, username
                                                ORDER BY score_vs_par ASC", conn);
                    cmd.Parameters.AddWithValue("@league_id", leagueId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LeagueScore leagueScore = createLeagueScoreFromReader(reader);
                        leagueScores.Add(leagueScore);                        
                    }
                    return leagueScores;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private League createLeagueFromReader(SqlDataReader reader)
        {
            League league = new League();
            Course course = new Course();

            league.LeagueId = Convert.ToInt32(reader["league_id"]);
            league.LeagueName = Convert.ToString(reader["league_name"]);
            league.OrganizerId = Convert.ToInt32(reader["organizer_id"]);
            course.CourseId = Convert.ToInt32(reader["course_id"]);
            course.CourseName = Convert.ToString(reader["course_name"]);
            league.OrganizerName = Convert.ToString(reader["organizer_username"]);

            league.LeagueCourse = course;
            return league;
        }

        private LeagueScore createLeagueScoreFromReader(SqlDataReader reader)
        {
            LeagueScore leagueScore = new LeagueScore();

            leagueScore.UserId = Convert.ToInt32(reader["user_id"]);
            leagueScore.Username = Convert.ToString(reader["username"]);
            leagueScore.Score = Convert.ToInt32(reader["total_score"]);
            leagueScore.Par = Convert.ToInt32(reader["total_par"]);
            leagueScore.TotalScore = Convert.ToInt32(reader["score_vs_par"]);

            return leagueScore;
        }
    }
}
