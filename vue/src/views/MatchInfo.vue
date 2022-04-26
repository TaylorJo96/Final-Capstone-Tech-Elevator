<template>
  <div id="matchInformation">
    <div id="match_title">
      <h2 v-on:created="populateLeague()" id="matchTitle">
        {{ match.matchName }}
      </h2>
    </div>
    <div>
    <button v-on:click="backButton()">Back to League Home</button>
    </div>
    <div id="matchScores">
      <match-score-list />
    </div>

    <div id="teeTime">
      <tee-assignment-form
        v-if="league.organizerId === $store.state.user.userId"
      />
    </div>
    <div id="updatescores">
      <match-score v-if="league.organizerId === $store.state.user.userId" />
    </div>
  </div>
</template>

<script>
import LeagueService from "../services/LeagueService.js";
import MatchService from "../services/MatchService.js";
import TeeAssignmentForm from "../components/TeeAssignmentForm.vue";
import MatchScore from "../components/MatchScore.vue";
import MatchScoreList from "../components/MatchScoreList.vue";

export default {
  components: { TeeAssignmentForm, MatchScore, MatchScoreList },
  data() {
    return {
      match: {
        matchId: "",
        matchName: "",
        leagueId: "",
        leagueName: "",
        StartTime: "",
      },
      league: {
        leagueId: "",
        organizerId: "",
      },
      errorMsg: "",
      clicker: "",
    };
  },
  created() {
    MatchService.getMatch(this.$route.params.id)
      .then((response) => {
        this.match = response.data;
        LeagueService.getCurrentLeague(this.match.leagueId)
          .then((response) => {
            this.league = response.data;
          })
          .catch((error) => {
            this.handleErrorResponse(error, "getting current league");
          });
      })
      .catch((error) => {
        this.handleErrorResponse(error, "getting current league");
      });
  },
  methods: {
    backButton() {
      this.$router.push(`/league/${this.league.leagueId}`);
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          "  Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg = "Error " + verb + "  Server could not be reached.";
      } else {
        this.errorMsg = "Error " + verb + "  Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style scoped>
#matchScores {
  background: tan;
  margin: 10px;
  border-radius: 10px;
  padding-left: 5px;
  padding-right: 5px;
}
#match_title {
  display: flex;
  justify-content: center;
  background: #fad586 !important;
  padding: 2px;
  filter: drop-shadow(2px 2px 2px black);
  border-bottom: #22577a 3px solid;
  border-top: #22577a 3px solid;
  text-transform: capitalize;
  color: #22577a;
  font-variant: small-caps;
  font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
}
#matchInformation > div:nth-child(2){
  width:300px;
}
#teeTime {

  background: tan;
  margin: 10px;
  border-radius: 10px;
  padding-left: 20px;
  padding-right: 5px;
}
#updatescores {
  background: tan;
  margin: 10px;
  border-radius: 10px;
  padding-left: 5px;
  padding-right: 5px;
}
#matchTitle {
  margin: 0px;
}
#matchInformation > div:nth-child(2) > button{
  
  background-color:rgb(24, 77, 71) ; 
padding:7px;
border-radius: 10px;
color:white;
font-weight: bolder;

  transition: all 0.2s ease-in-out;
  width:200px;
}
#matchInformation > div:nth-child(2) > button:hover{
   transform: scale(1.2);
background-color:rgb(45, 158, 145);

}
#matchInformation{
  display: flex;
  flex-direction: column;
  justify-content: space-between !important;
  align-items: space-between !important;
}
#matchInformation > div:nth-child(2){
margin-top: 20px !important;
  margin-bottom: 20px !important;}
  #teeTime{margin-top: 20px !important;
  margin-bottom: 20px !important;}
  #match-scores{
  
   
  }

</style>
