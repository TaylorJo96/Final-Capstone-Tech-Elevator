<template>
  <div id="register" class="text-center">
    <form class="form-register" @submit.prevent="register">
      <h1 class="h3 mb-3 font-weight-normal">Create Account</h1>
      <div class="alert alert-danger" role="alert" v-if="registrationErrors">
        {{ registrationErrorMsg }}
      </div>
      <div class="group">
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
        <input
          type="password"
          class="form-control"
          id="password"
          v-model="user.password"
          required
        />
        <span class="highlight"> </span>
        <span class="bar"> </span>
        <label>Password</label>
      </div>
      <div class="group">
        <input
          type="password"
          id="confirmPassword"
          class="form-control"
          v-model="user.confirmPassword"
          required
        />
        <span class="highlight"> </span>
        <span class="bar"> </span>
        <label>Confirm Password</label>
      </div>
      <button class="btn btn-lg btn-primary btn-block" type="submit" id ="sub">
        Create Account
      </button>
      <router-link :to="{ name: 'login' }">Have an account?</router-link>
    </form>
  </div>
</template>

<script>
import authService from "../services/AuthService";

export default {
  name: "register",
  data() {
    return {
      user: {
        username: "",
        password: "",
        confirmPassword: "",
        role: "user",
      },
      registrationErrors: false,
      registrationErrorMsg: "There were problems registering this user.",
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = "Password & Confirm Password do not match.";
      } else {
        authService
          .register(this.user)
          .then((response) => {
            if (response.status == 201) {
              this.$router.push({
                path: "/login",
                query: { registration: "success" },
              });
            }
          })
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = "Bad Request: Validation Errors";
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = "There were problems registering this user.";
    },
  },
};
</script>

<style scoped>
#register > form > div:nth-child(2) > label,
#register > form > div:nth-child(3) > label,
#register > form > div:nth-child(4) > label {
  color: #184d47;
}

#username,
#password,
#confirmPassword {
  font-size: 18px;
  padding: 10px 10px 10px 5px;
  display: block;
  width: 300px;
  border: none;
  border-bottom: 1px solid #757575;
}
#register > form > h1 {
  /* filter: drop-shadow(3px 3px 3px black); */
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
}
#register {
  display: flex;
  flex-direction: column;
  align-items: center;
}
#register > form {
  /* this controls the properties of the form inside the sign in box */

  display: flex;
  flex-direction: column;
  align-items: center;
  row-gap: 30px;
  /* this line adds space between items on sign in form */

  width: 85%;
}
#register > form > label:nth-child(2) {
  display: none;
}
sr-only {
  display: none;
}
.btn {
  width: 30%;
  
}
#register > form > label:nth-child(2),
#register > form > label:nth-child(4) {
  width: 35%;
  background-color: rgb(24, 77, 71);
  margin-top: 5px;
  margin-bottom: 5px;
  color: white;
  padding-left: 5px;
  border-radius: 3px;
}

#register > form {
  filter: drop-shadow(3px 3px 3px black);
}
#register > form > a {
  filter: none;
  text-shadow: none;
}

.group {
  position: relative;
  margin-bottom: 45px;
}
input:focus {
  outline: none;
}

/* LABEL ======================================= */
label {
  color: #999;
  font-size: 18px;
  font-weight: normal;
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
  font-size: 14px;
  color: #5264ae;
}

/* BOTTOM BARS ================================= */
.bar {
  position: relative;
  display: block;
  width: 300px;
}
.bar:before,
.bar:after {
  content: "";
  height: 2px;
  width: 0;
  bottom: 1px;
  position: absolute;
  background: #5264ae;
  transition: 0.2s ease all;
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
    background: #5264ae;
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
#sub{
  background: white;
}
</style>
