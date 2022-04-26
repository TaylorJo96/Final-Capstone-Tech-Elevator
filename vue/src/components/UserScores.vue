<template>
  <div>
    <table id="user-scores">
      <thead >
        <tr id="matchDateScore">
          <th>Match</th>
          <th>Date</th>
          <th>Score</th>
        </tr>
      </thead >
      <tbody>
        <tr id="bodyTable" v-for="score in userScores" v-bind:key="score.matchId">
          <td>{{ score.matchName }}</td>
          <td>{{ score.teeTime }}</td>
          <td>{{ score.total }}</td>
        </tr>
      </tbody>
    </table>

    <div>
      <h3 id="handy">Your handicap:</h3>
      <p id="playerHandy">{{ handicap }}</p>
    </div>
  </div>
</template>

<script>
import MatchService from "../services/MatchService.js";

export default {
  name: "user-scores",
  data() {
    return {
      userScores: [],
      scores: [],
      pars: [],
      scoreTotal: "",
      parTotal: "",
      handicap: "Not enough data to calculate handicap",
      dates: [],
    };
  },
  created() {
    MatchService.getAllUserScores(this.$store.state.user.userId)
      .then((response) => {
        this.userScores = response.data;
        this.userScores.forEach((item) => {
          if (item.total === 0) {
            item.total = "E";
          } else if (item.total > 0) {
            item.total = "+" + item.total;
          }
          this.scores.push(item.score);
          this.pars.push(item.par);
          let myDate = item.teeTime;
          let date = new Date(myDate).toDateString();
          item.teeTime = date;
        });
        this.gameCount = this.userScores.length;
        this.scoreTotal = this.scores.reduce((sum, number) => {
          return sum + number;
        }, 0);
        this.parTotal = this.pars.reduce((sum, number) => {
          return sum + number;
        });
        if (this.userScores.length >= 1) {
          this.handicap = (
            (this.scoreTotal - this.parTotal) /
            this.userScores.length
          ).toFixed(1);
        }
        if (this.handicap < 1) {
          this.handicap = "You're a scratch golfer. Well done!";
        }
      })
      .catch((error) => {
        this.handleErrorResponse(error, "retrieving");
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
#user-scores{
  background: purple;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-content: center;
}

#bodyTable{
  background: rgb(214, 239, 199);
  text-transform: capitalize;
  border:solid green 1px;
  
}
#user-scores > tbody{

}
#matchDateScore{
  display: flex;
  justify-content: space-around;
  
    background: rgb(24, 77, 71);
    color: white;
}
#bodyTable > td:nth-child(1){
  width: 150px;
}
#matchDateScore > th:nth-child(1){
  width:150px;
}
#bodyTable{
  display: flex;
  justify-content: space-around;
}
#user-scores{
  border:rgb(24, 77, 71) 4px solid;
  filter: drop-shadow(2px 2px 3px black);
  
}
#match-history{
display:flex;
justify-content: center;
 filter: drop-shadow(3px 3px 3px black);
  background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  /* text-shadow: black 5px 5px 5px; */
  color: white;
  font-size: x-large;
  display: flex;
  border-radius: 3px;
  justify-content: center;
  margin-bottom: 7px;
}
#handy{
  text-shadow: none;
  filter: drop-shadow(0px, 0px, 0px);
  color: white;
  /* padding:10px */

}
#playerHandy{
  padding-left:10px;
  padding-top:6px;
  /* padding:10px;
  padding-top:13px;
margin:0px; */
color:white;
font-weight: bold;
}
#history > div > div{
  background: rgba(24, 77, 71, .8);
  display: flex;

}
</style>