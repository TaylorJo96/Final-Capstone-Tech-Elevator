<template>
  <div>
    <div id="match_board">
      <h2 id d="matchboard">Match Leaderboard</h2>
    </div>
    <table id="match-scores">
      <thead>
        <tr>
          <th>Player</th>
          <th>Match Score</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="score in matchScores" v-bind:key="score.userId">
          <td id="name">
            {{ score.username }}
          </td>
          <td id="score">{{ score.total }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import MatchService from "../services/MatchService.js";

export default {
  name: "match-score-list",
  data() {
    return {
      matchScores: [],
    };
  },
  created() {
    MatchService.getAllMatchScores(this.$route.params.id)
      .then((response) => {
        this.matchScores = response.data;
        this.matchScores.forEach((item) => {
          if (item.total === 0) {
            item.total = "E";
          } else if (item.total > 0) {
            item.total = "+" + item.total;
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
          " match scores. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " match scores. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " match scores. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#match_board {
  font-style: italic;
  font-weight: bold;
  background: wheat;
  border-radius: 4px;
  font-size: large;
  color: #184d47;
  font-size: small;
  filter: drop-shadow(3px 3px 3px black);
  text-transform: capitalize;
}

#match-scores > tbody{
 background:#D6EFC7;
  text-transform: capitalize;
}
#match-scores > tbody > tr:nth-child(1){
  color:green;
  font-weight: bold;
}
#match-scores{
  margin-left:25%;
}


</style>