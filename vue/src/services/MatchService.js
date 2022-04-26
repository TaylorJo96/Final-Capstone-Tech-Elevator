import axios from 'axios';
import store from '../store/index.js';
store.getters.config;


export default {
    getMatches(leagueId) {
        return axios.get(`/match/${leagueId}`)
    },

    newMatch(match) {
        return axios.post(`/createMatch`, match)
    },

    getUserMatches(matchId) {
        return axios.get(`/userMatch/${matchId}`)
    },

    addMatchScore(userMatch) {
        return axios.put('/userMatch', userMatch)
    },

    getMatch(matchId) {
        return axios.get(`/match/info/${matchId}`)
    },

    getUserMatchesHomePage(userId) {
        return axios.get(`/userMatch/list/${userId}`)
    },

    setTeeTimeForUser(userMatch) {
        return axios.post('/userMatch', userMatch)
    },

    getAllMatchScores(matchId) {
        return axios.get(`/userMatch/match/${matchId}`)
    },

    getAllUserScores(userId) {
        return axios.get(`/userMatch/user/${userId}`)
    },

    getAllUserMatches(matchId) {
        return axios.get(`/userMatch/all/${matchId}`)
    }

}