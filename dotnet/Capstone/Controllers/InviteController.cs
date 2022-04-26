using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class InviteController : ControllerBase
    {
        private readonly ILeagueDao leagueDao;
        private readonly IInviteDao inviteDao;

        public InviteController(ILeagueDao _leagueDao, IInviteDao _inviteDao)
        {
            leagueDao = _leagueDao;
            inviteDao = _inviteDao;
        }
        [HttpGet("{leagueId}/users")]
        public ActionResult<List<ReturnUser>> ListUsersForInvite( int leagueId)
        {
            List<ReturnUser> usersForInvite = inviteDao.GetUsersForInvite( leagueId);
           
            if(usersForInvite != null)
            {
                return usersForInvite;
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet("users/{leagueId}/pending")]
        public ActionResult<List<Invite>> GetLeaguePendingInvites(int leagueId)
        {
            List<Invite> pendingInvites = inviteDao.GetLeaguePendingInvites(leagueId);
            if (pendingInvites != null)
            {
                return pendingInvites;
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost()]
        public ActionResult<Invite> NewInvite(Invite invite)
        {
            Invite addedInvite = inviteDao.CreateInvite(invite);
            return Created($"/invite/{addedInvite.InviteId}", addedInvite);
        }

        [HttpPost("{userId}")]
        public ActionResult AddUserToLeague(int userId, League league)
        {
            leagueDao.AddUserLeagueTable(userId, league.LeagueId);
            return Ok();
        }

        [HttpPut("{inviteId}/accept")]
        public ActionResult InviteAccepted(int inviteId)
        {
            inviteDao.AcceptInvite(inviteId);
            return Ok();
        }

        [HttpPut("{inviteId}/decline")]
        public ActionResult InviteDeclined(int inviteId)
        {
            inviteDao.DeclineInvite(inviteId);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Invite>> GetPendingInvites(int userId)
        {
            List<Invite> allInvites = inviteDao.ListPendingInvites(userId);
            if (allInvites != null)
            {
                return allInvites;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{inviteId}")]
        public ActionResult<Invite> GetInviteById(int inviteId)
        {
            Invite invite = inviteDao.GetInvite(inviteId);
            if (invite != null)
            {
                return invite;
            }
            else
            {
                return NotFound();
            }
        }

    }
}
