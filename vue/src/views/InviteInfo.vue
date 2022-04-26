<template>
<div>
  <div>
    <h3>Invite to: {{ invite.toLeagueName }}</h3>
    <h3>From: {{ invite.inviteFrom }}</h3>
    <button id="accept-invite" v-on:click="accept(), addToLeague()">
      Accept Invite
    </button>
    <button id="decline-invite" v-on:click="decline()">Decline Invite</button>
  </div>
  </div>
</template>

<script>
import InviteService from "../services/InviteService.js";
export default {
  data() {
    return {
      invite: {
        toLeagueName: "",
        inviteFrom: "",
        toLeagueId: "",
      },
      league: {
        leagueId: "",
      },
    };
  },
  created() {
    InviteService.getCurrentInvite(this.$route.params.id)
      .then((response) => {
        this.invite = response.data;
      })
      .catch((error) => {
        this.handleErrorResponse(error, "creating");
      });
  },
  methods: {
    accept() {
      InviteService.acceptInvite(this.$route.params.id)
        .then((response) => {
          if (response.status === 200) {
            this.$router.push("/");
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "accepting");
        });
    },
    addToLeague() {
      const addLeague = {
        leagueId: this.invite.toLeagueId,
      };
      InviteService.addUserToLeague(this.$store.state.user.userId, addLeague)
        .then((response) => {
          if (response.status === 200) {
            alert("Invite accepted");
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "adding user to league from");
        });
    },
    decline() {
      InviteService.declineInvite(this.$route.params.id)
        .then((response) => {
          if (response.status === 200) {
            this.$router.push("/");
            alert("Invite declined");
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "declining");
        });
    },
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " invite. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " invite. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " invite. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#page-body > div:nth-child(1) > h3:nth-child(1){
  text-transform: capitalize;
  margin:0px;
}
#page-body > div:nth-child(1) > h3:nth-child(2){
  text-transform: capitalize;
  margin:0px;
}

#accept-invite, #decline-invite{
 
  background-color:rgb(24, 77, 71) ; 
padding:7px;
border-radius: 10px;
color:white;
font-weight: bolder;

  transition: all 0.2s ease-in-out;
  width:200px;
}
#accept-invite:hover,#decline-invite:hover{
  transform: scale(1.2);
background-color:rgb(45, 158, 145);
}
#page-body > div:nth-child(1) > div{
  display: flex;
  flex-direction: column;
  align-items: center;
}
h3{
  margin:5px;
}
#page-body > div:nth-child(1) > div > h3:nth-child(1){
  text-transform: capitalize;
}
#page-body > div:nth-child(1) > div{
  background-color: rgb(150, 187, 124);
  margin:20px;
 width:90%;
  border-radius:5px;
  padding:5px;
 
}

</style>