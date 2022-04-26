<template>
  <div>
    <form v-on:submit.prevent="submitForm()">
      <h2 id="newLeague">Create a new League</h2>

      <div class="group">
        <input
          type="text"
          id="league-name"
          class="form-control"
          v-model="league.leagueName"
          required
        />
        <span class="highlight"> </span>
        <span class="bar"> </span>
        <label>League Name</label>
      </div>
      <!-- 
        <label id = selectacourse>
          Choose a course:</label> -->

      Choose a Course

      <div class="this">
        <select id="course-name" v-model.number="league.leagueCourse.courseId">
          <option
            class="select"
            v-for="course in courses"
            v-bind:key="course.courseId"
            v-bind:value="course.courseId"
          >
            {{ course.courseName }}
          </option>
        </select>
        <button id="no-course" v-on:click="routeToCourseForm()">Don't see your course?</button>
      </div>

      <button type="submit" class="btn-submit">Submit</button>
    </form>
  </div>
</template>

<script>
import LeagueService from "../services/LeagueService.js";
import CourseService from "../services/CourseService.js";
export default {
  name: "league-form",
  data() {
    return {
      league: {
        leagueName: "",
        organizerId: "",
        leagueCourse: {
          courseId: "",
        },
      },
      returnLeague: {
        leagueId: "",
        leagueName: "",
        organizerId: "",
        leagueCourse: {
          courseId: "",
          courseName: "",
        },
      },
      errorMsg: "",
      courses: [],
    };
  },
  created() {
    CourseService.getCourses()
      .then((response) => {
        this.courses = response.data;
      })
      .catch((error) => {
        this.handleErrorResponse(error, "generating courses for");
      });
  },
  methods: {
    submitForm() {
      const newLeague = {
        leagueName: this.league.leagueName,
        organizerId: Number(this.$store.state.user.userId),
        leagueCourse: {
          courseId: this.league.leagueCourse.courseId,
        },
      };
      LeagueService.newLeague(newLeague)
        .then((response) => {
          if (response.status === 201) {
            this.returnLeague = response.data;
            this.$router.push(`/league/${this.returnLeague.leagueId}`);
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "creating");
        });
    },
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
    routeToCourseForm() {
      this.$router.push(`/addCourse`);
    },
  },
};
</script>

<style>
#newLeague {
  /* filter: drop-shadow(3px 3px 3px black); */
  background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  text-shadow: black 5px 5px 5px;
  color: white;
  font-size: x-large;
  justify-content: center;
  display: flex;
  margin-bottom: 3px;
}
#newLeague {
  margin-bottom: 30px;
}
#pagetext > div.group > label {
  color: #184d47;
}
#page-body > div:nth-child(1) > div > form > div.this {
  justify-content: center;
  display: flex;
  flex-direction: column;
  row-gap: 20px;
}
#pagetext > div.this {
  margin-bottom: 30px;
}
#invite-form {
  row-gap: 20px;
}

#pagetext > div.group {
  display: flex;
  flex-direction: column;
  align-items: center;

  margin-top: 30px;
}

#no-course {
  margin-bottom: 30px;
}

#page-body > div:nth-child(1) > div > form > div.group > label {
  color: #184d47;
}
#league-name {
  color: #184d47;
  font-size: 18px;
  padding: 10px 10px 10px 5px;
  display: block;
  width: 300px;
  border: none;
  border-bottom: 1px solid #757575;
}

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

#no-course{
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
transition: all .2s ease-in-out; 
}
#no-course:hover{
  transform: scale(1.1); 
}


</style>