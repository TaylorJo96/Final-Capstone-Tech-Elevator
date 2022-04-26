<template>
  <div id="scoreboard">
    <div id="leaderboardheader">
    leaderboard
    </div>
    <div>
      <table>
        <thead>
          <tr>
            <th>Player</th>
            <th>Total Score</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="score in leagueScores" v-bind:key="score.userId">
            <td id="name">
              {{ score.username }}
            </td>
            <td id = "score">{{ score.totalScore }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import LeagueService from "../services/LeagueService.js";

export default {
  name: "league-scores",
  data() {
    return {
      leagueScores: [],
    };
  },
  created() {
    LeagueService.getScoresForLeague(this.$route.params.id)
      .then((response) => {
        this.leagueScores = response.data;
        this.leagueScores.forEach((item) => {
          if (item.totalScore === 0) {
            item.totalScore = "E";
          } else if (item.totalScore > 0) {
            item.totalScore = "+" + item.totalScore;
          }
        });
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
          " leaderboard. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " leaderboard. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " leaderboard. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>

#scoreboard > div > table > thead > tr{
  background:
rgb(150, 187, 124)
}
#scoreboard > div > table > tbody{
  background:#D6EFC7;
  text-transform: capitalize;

}
#scoreboard > div > table > tbody > tr:nth-child(1){
  color:green;
  font-weight: bolder;

}
#leaderboardheader{
  display:flex;
  justify-content: center;
  font-size:x-large;
  background-color:rgb(24, 77, 71);
  text-transform: capitalize;
  font-weight: bolder;
  color:white;
  text-shadow: 2px 2px 2px black;
  padding:4px;
      font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;


}
#score{
  display: flex;
  justify-content: center;
}
#name{padding-left:8px}
#scoreboard{
  background-color: rgb(24, 77, 71);
}
#scoreboard > div:nth-child(2) > table > tbody > tr:last-child{
  /* color:red; */
  /* font-weight: bold; */
}
</style>