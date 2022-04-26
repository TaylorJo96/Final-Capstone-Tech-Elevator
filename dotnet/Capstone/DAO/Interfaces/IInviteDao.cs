using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IInviteDao
    {
        public Invite CreateInvite(Invite invite);
        public Invite GetInvite(int inviteId);
        public void AcceptInvite(int inviteId);
        public void DeclineInvite(int inviteId);
        public List<Invite> ListPendingInvites(int userId);
        public List<ReturnUser> GetUsersForInvite( int userId);
        public List<Invite> GetLeaguePendingInvites(int leagueId);
    }
}
