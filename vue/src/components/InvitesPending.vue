<template>
  <div id="invitations">
    <div
      id="invite-list"
      v-for="invite in pendingInvites"
      :key="invite.inviteId"
    >
      You are invited to Join
      <router-link :to="{ path: '/invite/' + invite.inviteId }" id="inviteLink">
        {{ invite.toLeagueName }}
      </router-link>
      
    </div><div id="noInvite" v-if="this.pendingInvites.length < 1">
      You do not have any pending invites.
    </div>
  </div>
</template>

<script>
import InviteService from "../services/InviteService.js";
export default {
  name: "invites-pending",
  data() {
    return {
      pendingInvites: [],
    };
  },
  created() {
    InviteService.getPendingInvites(this.$store.state.user.userId)
      .then((response) => {
        this.pendingInvites = response.data;
        let leagueToArray = [];
        this.pendingInvites.forEach(element => {
          
          leagueToArray.unshift(element.leagueTo)
        });
        
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
          " invites. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " invites. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " invites. Request could not be created.";
      }
      console.log(this.errorMsg);
    },
  },
};
</script>

<style>
#invite-list > a{
text-decoration: none;
  background:rgb(214, 239, 199);
  padding:2px;
  font-weight: bolder;
  text-transform: capitalize;
  border-radius: 50px;
  border: solid rgb(150, 187, 124) 2px;
  filter: drop-shadow(3px 3px 3px black);
  color: rgb(24, 77, 71);
}

#invite-list > a:hover {
  background: rgb(150, 187, 124);
}
#invitations {
  margin: 10px;
  padding-bottom:15px
  /* font-weight: bold; */
}
#invites {
  margin-top: 5px;
  margin-bottom: 10px;
}
#invite-list{
margin:20px;

}
</style>