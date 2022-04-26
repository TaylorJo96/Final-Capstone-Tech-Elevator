<template>
  <div>
    <table id="league-matches">
      <thead>
        <tr>
          <th id="matchHeader">Match Name</th>
          <th id="matchHeader2">Date</th>
          <th id="matchHeader3">Start Time</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="match in matchInfo"
          v-bind:key="match.matchId"
          id="match-row"
        >
          <td id="matchNames">
            <router-link
              :to="{ path: '/match/' + match.matchId }"
              id="match_names"
            >
              {{ match.name }}
            </router-link>
          </td>
          <td id="dateAndTimes">{{ match.date }}</td>
          <td id="dateAndTimes">{{ match.time }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import MatchService from "../services/MatchService.js";
export default {
  name: "league-matches",
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
    MatchService.getMatches(this.$route.params.id)
      .then((response) => {
        this.matches = response.data;
        this.matches.forEach((item) => {
          let newMatch = {};
          newMatch.matchId = item.matchId;
          newMatch.name = item.matchName;
          let fullDate = item.dateAndTime;
          newMatch.date = new Date(fullDate).toDateString();
          newMatch.time = new Date(fullDate).toLocaleTimeString([], {
            hour: "2-digit",
            minute: "2-digit",
          });
          this.matchInfo.push(newMatch);
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
          " matches. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " matches. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " matches. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#leaguePage > div:nth-child(6) {
  background-color: rgb(250, 213, 134);
  margin: 10px;
}

#league-matches > tbody > tr:nth-child() {
  /* padding: 15px; */
}
#match_names {
  margin: 7px;
}
#match_names {
  color: white;
  text-transform: capitalize;
  text-decoration: none;
  font-weight: bold;
  background: rgb(248, 218, 154);
  display: flex;
  justify-content: center;
  align-items: center;
  color:rgb(24, 77, 71) ;
    font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
height: 30px;
  
 /* margin: 10px; */
 /* margin-top:50px; */
}
#match_names:hover {
  background: rgb(248, 202, 103);
}
#dateAndTimes {
  color: rgb(248, 230, 192);
  padding-left: 5%;
}
#matchHeader,
#matchHeader2,
#matchHeader3 {
  font-weight: bold;
}
#matchHeader2 {
  padding-right: 20%;
}
#matchHeader3 {
  padding-right: 5%;
}

#dateAndTimes {
  font-weight: bold;
}
</style>