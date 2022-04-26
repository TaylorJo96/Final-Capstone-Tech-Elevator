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
    [Route("userMatch")]
    [ApiController]
    [Authorize]
    public class UserMatchController : ControllerBase
    {
        private IUserMatchDao userMatchDao;
        public UserMatchController(IUserMatchDao _userMatchDao)
        {
            this.userMatchDao = _userMatchDao;
        }

        [HttpPost()]
        public ActionResult TeeTimeForUser(UserMatch userMatch)
        {
            userMatchDao.SetTeeTimeForUser(userMatch);
            return Created($"userMatch/{userMatch}", userMatch);
        }

        [HttpGet("list/{userId}")]
        public ActionResult<List<UserMatch>> GetUserMatchesForUser(int userId)
        {
            List<UserMatch> userMatches = userMatchDao.GetUserMatchesUpcoming(userId);
            if (userMatches != null)
            {
                return userMatches;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{matchId}")]
        public ActionResult<List<UserMatch>> UserMatchesInLeague(int matchId)
        {
            List<UserMatch> userMatchesInLeague = userMatchDao.GetUserMatchesForMatch(matchId);
            if (userMatchesInLeague != null)
            {
                return userMatchesInLeague;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("all/{matchId}")]
        public ActionResult<List<UserMatch>> GetAllUserMatches(int matchId)
        {
            List<UserMatch> allUserMatches = userMatchDao.GetAllUserMatches(matchId);
            if (allUserMatches != null)
            {
                return allUserMatches;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut()]
        public ActionResult AddMatchScore(UserMatch userMatch)
        {
            userMatchDao.InsertMatchScore(userMatch);
            return Ok();
        }

        [HttpGet("match/{matchId}")]
        public ActionResult<List<UserMatch>> GetMatchScores(int matchId)
        {
            List<UserMatch> matchScores = userMatchDao.GetUserScoresByMatch(matchId);
            if (matchScores != null)
            {
                return matchScores;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<UserMatch>> GetUserScores(int userId)
        {
            List<UserMatch> userScores = userMatchDao.GetMatchScoresByUser(userId);
            if (userScores != null)
            {
                return userScores;
            }
            else
            {
                return NotFound();
            }
        }

    }
}
