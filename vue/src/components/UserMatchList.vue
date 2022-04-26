<template>
  <div>
    <table id="league-matches" v-if="this.matches.length > 0">
      <thead>
        <tr>
          <th>Match Name</th>
          <th>Date</th>
          <th>Tee Time</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="match in matchInfo" v-bind:key="match.matchId">
          <td id="matchListItem">
            <div id="nameOfMatch" class="grow">
              <router-link :to="{ path: '/match/' + match.matchId }">
                {{ match.name }}
              </router-link>
            </div>
          </td>
          <td>{{ match.date }}</td>
          <td>{{ match.time }}</td>
        </tr>
      </tbody>
    </table>
    <div id="noMatch" v-if="this.matches.length <= 0">
      You do not have any upcoming matches.
    </div>
  </div>
</template>

<script>
import MatchService from "../services/MatchService.js";
export default {
  name: "user-matches",
  data() {
    return {
      matches: [],
      matchInfo: [],
      match: {
        matchId: "",
        name: "",
        date: "",
        time: "",
      },
    };
  },
  created() {
    MatchService.getUserMatchesHomePage(this.$store.state.user.userId)
      .then((response) => {
        this.matches = response.data;
        this.matches.forEach((item) => {
          let newMatch = {};
          newMatch.matchId = item.matchId;
          newMatch.name = item.matchName;
          let fullDate = item.teeTime;
          newMatch.date = new Date(fullDate).toDateString();
          newMatch.time = new Date(fullDate).toLocaleTimeString([], {
            hour: "2-digit",
            minute: "2-digit",
          });
          this.matchInfo.push(newMatch);
        });
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
          " match. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " match. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " match. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#league-matches {
  width: 100%;
 
}
#league-matches > tbody > tr {
  background: rgb(24, 77, 71);
  width: 100%;
}

#nameOfMatch {
  width: 90%;

  display: flex;
  justify-content: center;
  padding: 5px;

  padding: 5px;
}
#upcoming{
  width:70% !important;
}

#nameOfMatch > a {
  background: wheat;
  text-decoration: none;
  color: rgb(24, 77, 71);
  width: 100%;
  border-radius: 4px;
  display: flex;
  justify-content: center;
  font-weight: 900;
  word-spacing: -2px;

  text-transform: capitalize;
  font-variant: small-caps;
  text-decoration: underline;
}
#nameOfMatch > a:hover {
  background: #ebc96e;
}
.grow {
  transition: all 0.2s ease-in-out;
}

.grow:hover {
  transform: scale(1.1);
}
</style>