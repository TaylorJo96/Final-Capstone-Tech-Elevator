using Capstone.Models;
using System.Collections.Generic;
namespace Capstone.DAO
{
    public interface IUserDao
    {
        User GetUser(string username);

        User AddUser(string username, string password, string role);

        //        public List<User> GetUserList();

        public List<ReturnUser> GetUserList();


    }
}
