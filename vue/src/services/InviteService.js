import axios from 'axios';

export default {

    newInvite(invite) {
        return axios.post('/Invite', invite)
    },

    getPendingInvites(userId) {
        return axios.get(`/Invite/user/${userId}`)
    },

    addUserToLeague(userId, league) {
        return axios.post(`/Invite/${userId}`, league)
    },

    acceptInvite(inviteId) {
        return axios.put(`/Invite/${inviteId}/accept`)
    },

    declineInvite(inviteId) {
        return axios.put(`/Invite/${inviteId}/decline`)
    },

    getAllUsers() {
        return axios.get('/user')
    },

    getCurrentInvite(inviteId) {
        return axios.get(`/Invite/${inviteId}`)
    },

    getUsersForInvite(leagueId) {
        return axios.get(`/Invite/${leagueId}/users/`)
    },

    getPendingInvitesForLeague(leagueId) {
        return axios.get(`/Invite/users/${leagueId}/pending`)
    }


}
