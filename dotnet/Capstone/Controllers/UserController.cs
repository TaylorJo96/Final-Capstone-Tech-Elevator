using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase    
    {
        private IUserDao userDao;
        public UserController(IUserDao dao)
        {
            this.userDao = dao;
        }

        [HttpGet()]
        public ActionResult<List<ReturnUser>> GetUsers()
        {
            List<ReturnUser> allUsersExceptCurrent = new List<ReturnUser>();
            List<ReturnUser> allUsers = userDao.GetUserList();

            if(allUsers == null)
            {
                return NotFound();
            }
            else if(allUsers != null)
            {
                foreach(ReturnUser user in allUsers)
                {
                    if(user.Username != User.Identity.Name)
                    {
                        allUsersExceptCurrent.Add(user);
                    }
                }
                return allUsersExceptCurrent;
            }
            return StatusCode(500);
        }
    }
    

}
