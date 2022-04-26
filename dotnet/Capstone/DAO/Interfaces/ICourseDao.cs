using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface ICourseDao
    {
        public Course CreateCourse(Course course);
        public List<Course> GetCourses();
        public Course GetCourseById(int courseId);
        public List<League> GetLeaguesByCourseId(int courseId);
        
    }

}
