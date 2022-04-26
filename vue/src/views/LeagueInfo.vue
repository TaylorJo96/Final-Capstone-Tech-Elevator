<template>
  <div id="leaguePage">
    <div id="leagueTop">
      <div>
        <div id="leagueHead">
          <h1 id="leagueTitle">{{ league.leagueName }}</h1>
        </div>

        <div id="league-info">
          <div id="top">
            <div>
              <div>
              <h2 id="course">
                Course:
                <router-link
                  :to="{ path: '/course/' + league.leagueCourse.courseId }"
                  text-decoration="none"
                  >{{ league.leagueCourse.courseName }}</router-link
                >
              </h2>
            </div>

            <div id="organ">
              <h2 id="organizerName">Organizer: {{ league.organizerName }}</h2>
            </div>
            </div>
          <div>
        <table>
          <thead>
            <tr>
              <th>League Members</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="user in userList" v-bind:key="user.toUserId">
              <td>{{ user.username }}</td>
            </tr>
          </tbody>
        </table>
      </div>

            <league-scores />
          </div>
        </div>  
        <!-- <h3>
      {{userlist}}
          </h3>      this is a list of users in this league- we can use it to choose a user to setr -->

      </div>
    </div>
<league-matches />
    <invite-form v-if="league.organizerId === this.$store.state.user.userId" />
    <match-form v-if="league.organizerId === this.$store.state.user.userId" />
    
    <!-- ADD MATCH LIST COMPONENT -->
  </div>
</template>

<script>
import LeagueService from "../services/LeagueService.js";
import InviteForm from "../components/InviteForm.vue";
import MatchForm from "../components/MatchForm.vue";
import LeagueMatches from "../components/LeagueMatchList.vue";
import LeagueScores from "../components/LeagueScores.vue";

export default {
  components: { InviteForm, MatchForm, LeagueMatches, LeagueScores },
  data() {
    return {
      league: {
        leagueName: "",
        organizerId: "",
        leagueCourse: {
          courseName: "",
          courseId: "",
        },
        organizerName: "",
      },
      userList: [],
    };
  },
  created() {
    LeagueService.getCurrentLeague(this.$route.params.id)
      .then((response) => {
        this.league = response.data;
      })
      .catch((error) => {
        this.handleErrorResponse(error, "generating");
      });
    LeagueService.getUsersByLeague(this.$route.params.id)
      .then((response) => {
        this.userList = response.data;
      })
      .catch((error) => {
        this.handleErrorResponse(error, "generating");
      });
  },
  methods: {
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " league info. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " league info. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " league info. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#page-body > div:nth-child(1) {
  width: 90%;
}
/* #league-title > h1 {
  filter: drop-shadow(3px 3px 3px black);
  background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  text-shadow: black 5px 5px 5px;
  color: white;
  font-size: x-large;
  display: flex;
  width: 100%;
  justify-content: center;
  margin-bottom: 3px;
} */

#page-body > div:nth-child(1) {
  background-color: rgba(209, 255, 209, 0.5);
  border-width: 3px;
  border-radius: 6px;
  padding: 10px;
  /* margin: 30px; */
  width: 80%;
  filter: drop-shadow(5px 5px 5px black);
  display: flex;
  margin-top: 20px;
  margin-bottom: 20px;
  flex-direction: column;
}
#leagueTitle {
  display: flex;
  justify-content: center;
  background: #fad586;
  padding: 2px;
  filter: drop-shadow(2px 2px 2px black);
  border-bottom: #22577a 3px solid;
  border-top: #22577a 3px solid;
  text-transform: capitalize;
  color: #22577a;
  font-variant: small-caps;
  font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
}
#league-info {
  display: flex;
  flex-direction: row;
  justify-content: center;
  text-shadow: none;
  margin: 0;
 align-content: center;
}
#top > div:nth-child(1) > div:nth-child(1){
  background: #D6EFC7;
  filter: drop-shadow(2px 2px 2px black);
  border-radius: 2px;
  padding:3px;
}
#course {
  margin: 0;
  font-size: medium;
  font-weight: bolder;
  text-transform: capitalize;

}
#leagueTop > div{
 display: flex;
 flex-direction: column;



}
#organizerName {
  margin-top: 0;
  font-size: medium;
  text-transform: capitalize;
  background: #D6EFC7;
  border-radius: 2px;
  padding:3px;
  filter: drop-shadow(2px 2px 2px black);
}
#course > a {
  color: #1d3d52;
}
#course > a:hover {
  color: #fad586;
}
#leaguePage > div:nth-child(1) {
}
#top{
  
  width: 100%;
  display:flex;
  justify-content: space-around;
  align-content: space-between;
  padding-left:30px;
  padding-right:30px;
  margin-bottom:30px;
}
#top > div:nth-child(1) > div:nth-child(1){
  margin-bottom: 30px;
}
#top > div:nth-child(1){
padding:20px;
display: flex;
flex-direction: column;
/* align-content: space-between;
justify-content: space-around; */
justify-content: center;
background-color: rgba(150, 187, 124, .9);
margin-right: 20px;
width:40%;
border-radius: 30px;
filter: drop-shadow(2px 2px 2px black);
margin-top:30px;
height: 60%;
}
div{
  margin-bottom: 10px;
}
#page-body > div.home > div:nth-child(3){
  width:80% !important;
}
#leagueTop{
  margin-bottom: 20px !important;
}
#leaguePage > div:nth-child(3){
margin-top: 20px !important;
  margin-bottom: 20px !important;
}
</style>

