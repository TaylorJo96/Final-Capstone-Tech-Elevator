using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int OrganizerId { get; set; }
        public Course LeagueCourse { get; set; }
        public string OrganizerName { get; set; }
    }
}
