<template>
  <div>
    <form id="submit-score-form" v-on:submit.prevent="submitScore()">
      <div id ="updatescore">
      <h2>Update scores</h2>
      </div>
      <div>
        <select class="user-match" v-model.number="userMatch.userId">
          <option value="" selected="selected" class="default-selector">
            Select a Player
          </option>
          <option
            v-for="user in userMatches"
            v-bind:key="user.counter"
            v-bind:value="user.userId"
          >
            {{ user.username }}
          </option>
        </select>
      </div>
      <div>
        <input
          id="score"
          type="number"
          min="0"
          placeholder="Enter player score"
          v-model.number="userMatch.score"
        />
      </div>
      <div>
        <input
          id="score"
          type="number"
          min="0"
          placeholder="Enter par score"
          v-model.number="userMatch.par"
        />
      </div>
      <button type="submit" class="btn-submit">Submit</button>
    </form>
  </div>
</template>

<script>
import MatchService from "../services/MatchService.js";

export default {
  name: "match-score",
  data() {
    return {
      userMatch: {
        matchId: "",
        userId: "",
        score: "",
        par: "",
      },
      match: {
        leagueId: "",
      },
      userMatches: [],
    };
  },
  created() {
    MatchService.getMatch(this.$route.params.id).then((response) => {
      this.match = response.data;
      MatchService.getUserMatches(this.$route.params.id)
        .then((response) => {
          this.userMatches = response.data;
          let adder = 0;
          this.userMatches.forEach((item) => {
            adder++;
            item.counter = adder;
          });
        })
        .catch((error) => {
          this.handleErrorResponse(error, "finding");
        });
    });
  },
  methods: {
    submitScore() {
      this.userMatch = {
        matchId: Number(this.$route.params.id),
        userId: this.userMatch.userId,
        score: this.userMatch.score,
        par: this.userMatch.par,
      };
      MatchService.addMatchScore(this.userMatch)
        .then((response) => {
          if (response.status === 200) {
            //this.$router.push("/");
            alert("Score updated");
            location.reload();
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "updating score in");
        });
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " user matches. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " user matches. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " user matches. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
  computed: {
    filteredList() {
      let filteredMatches = this.userMatches;

      filteredMatches.filter((match) => match.userId === this.userMatch.userId);

      return filteredMatches;
    },
  },
};
</script>

<style>
#updatescore{
      font-style: italic;
  font-weight: bold;
background:wheat;
border-radius:4px;
font-size:large;
color:#184D47;
font-size: small;
 filter: drop-shadow(3px 3px 3px black);
text-transform: capitalize;
}
.default-selector {
  color: lightgray;
  text-decoration-style: wavy;
  display: none;
}
#match-scores{
  background-color: rgb(24, 77, 71);
}
#submit-score-form > button{
   margin-top:5px;
  
  background-color:rgb(24, 77, 71) ; 
padding:7px;
border-radius: 10px;
color:white;
font-weight: bolder;

  transition: all 0.2s ease-in-out;
  width:200px;}
#submit-score-form > button:hover{  transform: scale(1.2);
background-color:rgb(45, 158, 145);}
</style>