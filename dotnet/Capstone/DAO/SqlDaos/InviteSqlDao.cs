using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class InviteSqlDao : IInviteDao
    {
        private readonly string connectionString;

        public InviteSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<ReturnUser> GetUsersForInvite(int leagueId)
        {
            List<ReturnUser> inviteUsers = new List<ReturnUser>();
            Dictionary<Tuple<string, int>, List<int>> allUsers = new Dictionary<Tuple<string,int>, List<int>>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT DISTINCT users.user_id, username, league_id FROM user_league
                        JOIN users ON user_league.user_id = users.user_id", conn);
                    
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReturnUser u = new ReturnUser()
                        {
                            UserId = Convert.ToInt32(reader["user_id"]),
                            Username = Convert.ToString(reader["username"]),
                             leagueId = Convert.ToInt32(reader["league_id"])

                         };

                        List<int> leagueIds = new List<int>();
                        Tuple<string, int> keyTuple = new Tuple<string,int>(u.Username, u.UserId); 

                        if (allUsers.ContainsKey(keyTuple))
                        {
                            allUsers[keyTuple].Add(u.leagueId);
                        }
                        else
                        {
                            allUsers.Add(keyTuple, leagueIds);
                            allUsers[keyTuple].Add(u.leagueId);
                        }
                        
                    }
                    foreach(KeyValuePair<Tuple<string, int>, List<int>> kvp in allUsers)
                    {
                        if (!kvp.Value.Contains(leagueId)){
                            ReturnUser u2 = new ReturnUser();
                            u2.Username = kvp.Key.Item1;
                            u2.UserId = kvp.Key.Item2;
                            inviteUsers.Add(u2);
                        }
                    }
                    

                }
            }
            catch (SqlException)
            {
                throw;
            }
            
            return inviteUsers;
        }
        public List<Invite> GetLeaguePendingInvites(int leagueId)
        {
            List<Invite> leaguePendingInvites = new List<Invite>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT username, invite_id, invite_status, to_user, to_league
                                                FROM invites
                                                JOIN users ON users.user_id = invites.to_user
                                                WHERE to_league = @league_id AND invite_status = 'pending'", conn);
                    cmd.Parameters.AddWithValue("@league_id", leagueId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Invite newInvite = createInviteFromReader(reader);
                        newInvite.ToUsername = Convert.ToString(reader["username"]);
                        leaguePendingInvites.Add(newInvite);
                    }
                }
                return leaguePendingInvites;
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public Invite CreateInvite(Invite invite)
        {
            int newInviteId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO invites(invite_status, to_user, to_league)
                                                OUTPUT INSERTED.invite_id
                                                VALUES ('pending', @to_user, @to_league)", conn);
                    cmd.Parameters.AddWithValue("@to_user", invite.ToUserId);
                    cmd.Parameters.AddWithValue("@to_league", invite.ToLeagueId);

                    newInviteId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                return GetInvite(newInviteId);
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public Invite GetInvite(int inviteId)
        {
            Invite invite = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" SELECT invite_id, invite_status, to_user, to_league, league_name,
                                    (SELECT username FROM users WHERE users.user_id = leagues.organizer_id) AS organizer_name
                                    FROM invites
                                    JOIN leagues ON leagues.league_id = invites.to_league
                                    WHERE invite_id = @inviteId", conn);
                    cmd.Parameters.AddWithValue("@inviteId", inviteId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        invite = createInviteFromReader(reader);
                        invite.ToLeagueName = Convert.ToString(reader["league_name"]);
                        invite.InviteFrom = Convert.ToString(reader["organizer_name"]);
                    }
                }
                return invite;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void AcceptInvite(int inviteId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE invites
                                                SET invite_status = 'accepted'
                                                WHERE invite_id = @inviteId", conn);
                    cmd.Parameters.AddWithValue("@inviteId", inviteId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public void DeclineInvite(int inviteId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE invites
                                                SET invite_status = 'declined'
                                                WHERE invite_id = @inviteId", conn);
                    cmd.Parameters.AddWithValue("@inviteId", inviteId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public List<Invite> ListPendingInvites(int userId)
        {
            List<Invite> userLeagueInvites = new List<Invite>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT invite_id, invite_status, to_user, to_league, league_name
                                                FROM invites
                                                JOIN leagues ON leagues.league_id = invites.to_league
                                                WHERE to_user = @user_id AND invite_status = 'pending'", conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Invite newInvite = createInviteFromReader(reader);
                        newInvite.ToLeagueName = Convert.ToString(reader["league_name"]);
                        userLeagueInvites.Add(newInvite);
                    }
                }
                return userLeagueInvites;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private Invite createInviteFromReader(SqlDataReader reader)
        {
            Invite invite = new Invite();

            invite.InviteId = Convert.ToInt32(reader["invite_id"]);
            invite.InviteStatus = Convert.ToString(reader["invite_status"]);
            invite.ToUserId = Convert.ToInt32(reader["to_user"]);
            invite.ToLeagueId = Convert.ToInt32(reader["to_league"]);

            return invite;
        }
    }
}
