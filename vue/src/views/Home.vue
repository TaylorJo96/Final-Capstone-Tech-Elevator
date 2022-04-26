<template>
  <div class="home">
    <div id="league-info-home">
      <h1>My Leagues</h1>
      <div class="nav-link">
        <router-link class="leagueLink" v-bind:to="{ name: 'league' }"
          >New League</router-link
        >
        <!-- &nbsp;&nbsp; -->
      </div>
    </div>

    <league-list />

    <div>
      <div id="inviteBox">
        <div id="invites">
          <h3 id="inviteHead">League Invitations</h3>
        </div>
        <invites-pending />
      </div>
    </div>
    <div id="dashboard-flex">
      <div id="upcoming">
        <h1 id="upcomingMatches">Upcoming Matches</h1>
        <user-matches />
      </div>
      <div id="history">
        <h1 id="match-history">Match History</h1>
        <user-scores />
      </div>
    </div>
  </div>
</template>

<script>
import InvitesPending from "../components/InvitesPending.vue";
import LeagueList from "../components/LeagueList.vue";
import UserMatches from "../components/UserMatchList.vue";
import UserScores from "../components/UserScores.vue";

export default {
  name: "home",
  components: { LeagueList, InvitesPending, UserMatches, UserScores },
};
</script>
<style scoped>
.grow {
  transition: all 0.2s ease-in-out;
}

.grow:hover {
  transform: scale(1.1);
}

#page-body > div.home {
  background-color: rgba(209, 255, 209, 0.5);
  /* border-color: rgb(43, 126, 43); */
  /* border-style: solid;
  border-width: 3px; */
  border-radius: 6px;
  padding: 5px;
  /* margin: 30px; */
  width: 90%;
  filter: drop-shadow(5px 5px 5px black);
  display: flex;
  margin-top: 20px;
  margin-bottom: 20px;
  flex-direction: column;
  /* justify-content: space-around; */
  align-items: center;
  max-width: 95vh;
}
#league-info-home > h1
 {
 
 filter: drop-shadow(3px 3px 3px black);
 background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  text-shadow: black 5px 5px 5px;
  color: white;
  font-size: xx-large;
  display: flex;
  width: 100%;
  justify-content: center;
  margin-bottom: 3px;
  height:50px;
  display: flex;
  align-items: center;
    font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;

  
}
#league-info-home {
  display: flex;
  flex-direction: column;

  width: 100%;
}
#league-info-home > div {
  display: flex;
  justify-content: flex-end;
}

#league-info-home > div > a {
  margin-top: 3px;
  text-decoration: none;
  background: oldlace;
  border-radius: 40px;
  padding: 4px;
  padding-top: 0;
  padding-bottom: 0;
  font-size: 75%;
  color: rgb(122, 107, 73);
  font-weight: bold;
  border: 1px rgb(122, 107, 73) solid;
  /* text-shadow:rgba(0, 0, 0, 0.2) 1px 1px 1px; */
}
#league-info-home > div > a:hover {
  background: #ebc96e;
}
#dashboard-flex {
  flex-direction: column;
  width: 100%;
}
#upcoming {
  background: rgba(24, 77, 71, 0.5);
  border-radius: 4px;
  padding: 10px;
}
#upcomingMatches {
  font-style: italic;
  font-weight: bold;
  background: wheat;
  border-radius: 4px;
  font-size: large;
  color: #184d47;

  filter: drop-shadow(3px 3px 3px black);
  text-transform: capitalize;
}
#inviteHead {
  font-style: italic;
  font-weight: bold;
  background: wheat;
  border-radius: 4px;
  font-size: large;
  color: #184d47;


  filter: drop-shadow(3px 3px 3px black);
  text-transform: capitalize;
margin-bottom:5px ;
margin-top:10px;
  text-shadow: none;
}
#inviteBox {
  display: flex;
  flex-direction: column;
  background-color: rgba(24, 77, 71, 0.5);

  border-radius: 4px;
  padding: 10px;
  margin-bottom: 10px;
  margin-top: 10px;
width:80% !important;
}

#match-history{
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
  height:40px;
  display: flex;
  align-items: center;
    font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
margin-bottom: 10px;
}
#page-body > div.home > div:nth-child(3){
  width: 100%;
}
#invites{
  margin-bottom: 0;

}

#league-info-home{
  margin:20px !important;
}
#page-body > div.home > div{
  margin:10px;
}


</style>