<template>
  <div id="login" class="text-center">
    <form class="form-signin" @submit.prevent="login">
      <h1 class="h3 mb-3 font-weight-normal">Please Sign In</h1>
      <div class="alert alert-danger" role="alert" v-if="invalidCredentials">
        Invalid username and password!
      </div>
      <div
        class="alert alert-success"
        role="alert"
        v-if="this.$route.query.registration"
      >
        Thank you for registering, please sign in.
      </div>

      <div class="group">
        <!-- <label for="username" class="sr-only">Username</label> -->
        <input
          type="text"
          id="username"
          class="form-control"
          v-model="user.username"
          required
        />
        <span class="highlight"> </span>
        <span class="bar"> </span>
        <label>Username</label>
      </div>
      <div class="group">
        <!-- <label for="password" class="sr-only">Password</label> -->
        <input
          type="password"
          id="password"
          class="form-control"
          v-model="user.password"
          required
        />
        <span class="highlight"> </span>
        <span class="bar"> </span>
        <label>Password</label>
      </div>

      <button id="signInBtn" class="btn" type="submit">Sign in</button>
<div id="needAccount">
      <router-link :to="{ name: 'register' }">Need an account?</router-link>
    </div>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "login",
  components: {},
  created() {},
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
      invalidCredentials: false,
    };
  },
  methods: {
    login() {
      authService
        .login(this.user)
        .then((response) => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch((error) => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
  },
};
</script>
<style>
#needAccount > a
{text-decoration: none;
color:darkslategray}
#needAccount{

  
  background:#D6EFC7;
  padding:5px;
  padding-top:0px;
  padding-bottom:0px;
  border-radius: 5px;
  text-decoration: none;
  font-size: smaller;
  margin: 10px;
  transition: all .2s ease-in-out; 
}
#needAccount:hover{
    background: #7eac63;
    transform: scale(1.1); 
}
#login {
  display: flex;
  flex-direction: column;
  align-items: center;
}
#login > form {
  /* this controls the properties of the form inside the sign in box */
  filter: drop-shadow(3px 3px 3px black);
  display: flex;
  flex-direction: column;
  align-items: center;
  row-gap: 5px;
  /* this line adds space between items on sign in form */

  width: 80%;
}

#signInBtn{
  background:#D6EFC7;
  width:150px;
  height:40px;
  border-radius: 7px;
  font-weight: bold;
  font-size: medium;
  margin-bottom: 0;
  
}
#signInBtn:hover{
   background: #7eac63;

transform: scale(1.2); 
transition: all .2s ease-in-out; 
}

#login > form > button {
  margin-top: 20px;
}
#login > form > h1 {
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
#login > form > label:nth-child(2),
#login > form > label:nth-child(4) {
  width: 35%;
  background-color: rgb(24, 77, 71);
  margin-top: 5px;
  margin-bottom: 5px;
  color: white;
  padding-left: 5px;
  border-radius: 3px;
  
}
#login > form > div:nth-child(2) {
  margin-top: 30px;
}

#login > form > div:nth-child(2) > label,
#login > form > div:nth-child(3) > label {
  color: #184d47;
}

/* 
#login > form {
  filter: drop-shadow(3px 3px 3px black);
  text-shadow: rgb(90, 87, 87) 3px 3px 3px;
}
#login > form > a {
  filter: none;
  text-shadow:none;
} 


/* form starting stylings ------------------------------- */
.group {
  position: relative;
  margin-bottom: 45px;
}
#username,
#password {
  font-size: 20px;
  padding: 10px 10px 10px 5px;
  display: block;
  width: 300px !important;
  border: none;
  border-bottom: 1px solid #757575;
}
input:focus {
  outline: none;
}

/* LABEL ======================================= */
label {
  color: #184d47;
  font-size: 25px;
  font-weight: bolder;
  position: absolute;
  pointer-events: none;
  left: 5px;
  top: 10px;
  transition: 0.2s ease all;
  -moz-transition: 0.2s ease all;
  -webkit-transition: 0.2s ease all;
}

/* active state */
input:focus ~ label,
input:valid ~ label {
  top: -20px;
  font-size: 12px;
  color: #000000;
}

/* BOTTOM BARS ================================= */
.bar {
  position: relative;
  display: block;
  width: 315px !important;
}
.bar:before,
.bar:after {
  content: "";
  height: 3px !important;
  width: 0;
  bottom: 1px;
  position: absolute;
  background: #78a85b !important; 
  transition: .5s ease all !important;
  -moz-transition: 0.2s ease all;
  -webkit-transition: 0.2s ease all;
}
.bar:before {
  left: 50%;
}
.bar:after {
  right: 50%;
}

/* active state */
input:focus ~ .bar:before,
input:focus ~ .bar:after {
  width: 50%;
}

/* HIGHLIGHTER ================================== */
.highlight {
  position: absolute;
  height: 60%;
  width: 100px;
  top: 25%;
  left: 0;
  pointer-events: none;
  opacity: 0.5;
}

/* active state */
input:focus ~ .highlight {
  -webkit-animation: inputHighlighter 0.3s ease;
  -moz-animation: inputHighlighter 0.3s ease;
  animation: inputHighlighter 0.3s ease;
}

/* ANIMATIONS ================ */
@-webkit-keyframes inputHighlighter {
  from {
    background: #5264ae;
  }
  to {
    width: 0;
    background: transparent;
  }
}
@-moz-keyframes inputHighlighter {
  from {
    background: #6071b6;
  }
  to {
    width: 0;
    background: transparent;
  }
}
@keyframes inputHighlighter {
  from {
    background: #5264ae;
  }
  to {
    width: 0;
    background: transparent;
  }
}
#login > form > div:nth-child(3){
  margin-bottom:0px !important;
}

</style>