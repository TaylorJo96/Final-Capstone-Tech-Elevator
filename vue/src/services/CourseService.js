import axios from 'axios';


export default {

    createCourse(course) {
        return axios.post('/course', course)
    },

    getCourses() {
        return axios.get('/course')
    },

    getCourseById(courseId) {
        return axios.get(`/course/${courseId}`)
    },

    getLeaguesByCourseId(courseId) {
        return axios.get(`/course/leagues/${courseId}`)
    },

    getCoordinates(zipCode) {
        return axios.get(`/weather/${zipCode}`)
    }

}