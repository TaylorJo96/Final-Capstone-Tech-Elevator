using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class CourseSqlDao : ICourseDao
    {
        private readonly string connectionString;
        public CourseSqlDao(string dbConnectionString)
        {
            this.connectionString = dbConnectionString;
        }
        public Course CreateCourse(Course course)
        {
            int newCourseId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO courses(course_name, address)
                                                OUTPUT INSERTED.course_id
                                                VALUES (@course_name, @address)", conn);
                    cmd.Parameters.AddWithValue("@course_name", course.CourseName);
                    cmd.Parameters.AddWithValue("@address", course.Address);

                    newCourseId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                course.CourseId = newCourseId;
                return course;
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
        public Course GetCourseById(int courseId)
        {
            Course course = new Course();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM courses WHERE course_id = @course_id", conn);
                    cmd.Parameters.AddWithValue("@course_id", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        course.CourseId = Convert.ToInt32(reader["course_id"]);
                        course.CourseName = Convert.ToString(reader["course_name"]);
                        course.Address = Convert.ToString(reader["address"]);
                    }
                }
                return course;

            }
            catch (SqlException)
            {
                throw;
            }

        }

        public List<League> GetLeaguesByCourseId(int courseId)
        {
            List<League> leagueList = new List<League>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM leagues WHERE course_id = @course_id", conn);
                    cmd.Parameters.AddWithValue("@course_id", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        League league = new League();
                        league.LeagueId = Convert.ToInt32(reader["league_id"]);
                        league.LeagueName = Convert.ToString(reader["league_name"]);
                        league.OrganizerId = Convert.ToInt32(reader["organizer_id"]);
                        leagueList.Add(league);
                    }
                }
                return leagueList;

            }
            catch (SqlException)
            {
                throw;
            }
        }
    }
}
