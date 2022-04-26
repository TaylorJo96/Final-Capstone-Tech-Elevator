using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Invite
    {
        public int InviteId { get; set; }
        public string InviteStatus { get; set; }
        public int ToUserId { get; set; }
        public int ToLeagueId { get; set; }
        public string ToLeagueName { get; set; }
        public string InviteFrom { get; set; }
        public string ToUsername { get; set; }
    }
}
