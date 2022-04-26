import axios from 'axios';


export default {

  newLeague(league) {
    return axios.post('/League', league)
  },

  getLeagues(userId) {
    return axios.get(`/League/user/${userId}`)
  },

  getCurrentLeague(leagueId) {
    return axios.get(`/League/${leagueId}`)
  },

  getUsersByLeague(leagueId) {
    return axios.get(`/match/user/${leagueId}`)
  },

  getScoresForLeague(leagueId) {
    return axios.get(`/league/scores/${leagueId}`)
  }

}