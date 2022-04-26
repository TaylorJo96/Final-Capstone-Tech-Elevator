using Capstone.DAO;
using Capstone.DAO.Interfaces;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseDao courseDao;
        private readonly IWeatherDAO weather;
        public CourseController(ICourseDao _courseDao, IWeatherDAO _weather)
        {
            courseDao = _courseDao;
            weather = _weather;
        }
        [HttpPost()]
        public ActionResult<Course> AddCourse(Course course)
        {
            Course returnCourse = courseDao.CreateCourse(course);
            if (returnCourse == null)
            {
                return NotFound();
            }
            else if(returnCourse != null)
            {
                return Created($"course/{returnCourse.CourseId}", returnCourse);
            }
            else
            {
                return StatusCode(500);
            }

        }
        [HttpGet()]
        public ActionResult<List<Course>> GetAllCourses()
        {
            List<Course> allCourses = courseDao.GetCourses();
            if (allCourses != null)
            {
                return allCourses;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{courseId}")]
        public ActionResult<Course> GetCourseById(int courseId)
        {
            Course returnCourse = courseDao.GetCourseById(courseId);
            if(returnCourse == null)
            {
                return NotFound();
            }
            else if(returnCourse != null)
            {
                return returnCourse;
            }
            return StatusCode(500);
        }
        [HttpGet("leagues/{courseId}")]
        public ActionResult<List<League>> GetLeaguesByCourseId(int courseId)
        {
            List<League> courseLeagues = courseDao.GetLeaguesByCourseId(courseId);
            if (courseLeagues == null)
            {
                return NotFound();
            }
            else if (courseLeagues != null)
            {
                return courseLeagues;
            }
            return StatusCode(500);
        }
        [HttpGet("/weather/{zipCode}")]
        public ActionResult<Coordinate> GetCoordinate(int zipCode)
        {
            Coordinate coordinate = weather.getCoordinates(zipCode);
            if(coordinate != null)
            {
                return coordinate;
            }
            else if(coordinate == null)
            {
                return NotFound();
            }
            return StatusCode(500);
        }
    }
}
