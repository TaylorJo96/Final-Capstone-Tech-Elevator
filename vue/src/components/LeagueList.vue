<template>
  <div id="league-board">
    <div id="league-div">
      <div class="loading" v-if="isLoading">
        <img src="../images/spinner.svg" />
      </div>
      <div id="league-list" v-for="league in leagues" :key="league.id">
        <div id="league-names" class="grow">
          <router-link :to="{ path: '/league/' + league.leagueId }" id=leagueNames>
            {{ league.leagueName }}
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import LeagueService from "../services/LeagueService.js";
export default {
  name: "League-List",
  data() {
    return {
      leagues: [],
      isLoading: true,
    };
  },
  created() {
    LeagueService.getLeagues(this.$store.state.user.userId)
      .then((response) => {
        this.leagues = response.data;
        this.leagues.shift();
        this.isLoading = false;
      })
      .catch((error) => {
        this.handleErrorResponse(error, "creating");
      });
  },

  methods: {
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " league. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " league. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " league. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#league-board {
  margin-top: 5px;
  border-radius: 15px;
  background: rgb(250, 213, 134);
  background: linear-gradient(
    0deg,
    rgba(250, 213, 134, 0.9164040616246498) 0%,
    rgba(251, 237, 206, 1) 50%,
    rgba(250, 213, 134, 0.7847514005602241) 100%
  );
  filter: drop-shadow(3px 3px 3px black);
  width: 70%;
  padding: 20px;
  display: flex;
  justify-content: center;
  margin: 10px !important;
}
#leagueNames{
  text-decoration: none;
  background-color: rgb(24, 77, 71);
  border-radius: 40px;
  text-shadow: black 1px 1px 1px;
  color: white;
  font-weight: bolder;
  padding: 4px;
  display: flex;
  justify-content: center;
  filter: drop-shadow(3px 3px 3px black);
  width:300px;
  margin:10px;
text-transform: capitalize;
}
#leagueNames:hover
{  background-color: rgb(58, 124, 117);}
#league-list{
  display: flex;
  justify-content: center;
}
</style>

