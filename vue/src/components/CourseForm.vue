<template>
  <div id="add-course-page">
    <form v-on:submit.prevent="submitForm()">
      <div class="group">
      <input 
      type="text"
      id="course-name" 
      v-model="course.courseName"
      required
      />
      <span class="highlight"> </span>
      <span class="bar"> </span>
      <label>Course Name</label>
      </div>
      <div class="group">
      <input 
      type="text" 
      id="course-address"
      v-model="course.address" 
      required
      />
      <span class="highlight"> </span>
      <span class="bar"> </span>
      <label>Street Address</label>
      </div>
      <button type="submit" class="btn-submit">Add Course</button>
    </form>
  </div>
</template>


<script>
import CourseService from "../services/CourseService.js";
export default {
  name: "course-form",
  data() {
    return {
      course: {
        CourseName: "",
        courseId: "",
        address:""
      },
      returnCourse: {
        courseName: "",
        courseId: "",
        address: ""
      },
    };
  },
  created() {},
  methods: {
    handleErrorResponse(error, verb) {
      if (error.response) {
        this.errorMsg =
          "Error " +
          verb +
          " course. Response received was '" +
          error.response.statusText +
          "'.";
      } else if (error.request) {
        this.errorMsg =
          "Error " + verb + " course. Server could not be reached.";
      } else {
        this.errorMsg =
          "Error " + verb + " course. Request could not be created.";
      }
      console.log(this.errorMsg);
    },

    submitForm() {
      const newCourse = {
        courseName: this.course.courseName,
        address: this.course.address
      };
      CourseService.createCourse(newCourse)
        .then((response) => {
          if (response.status === 201) {
            this.returnCourse = response.data;
            alert("Course Added.");
            this.course.courseName = "";
          }
        })
        .catch((error) => {
          this.handleErrorResponse(error, "creating");
        });
    },
  },
};
</script>

<style>
#add-course-page > form {
  display: flex;
  flex-direction: column;
  align-items: center;
}
#add-course-header > h1 {
     background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  text-shadow: black 5px 5px 5px;
  color: white;
  font-size: x-large;
  display: flex;
  width: 100%;
  justify-content: center;
  
  margin-bottom: 30px;
  filter: drop-shadow(3px 3px 3px black);
}

#page-body > div:nth-child(1)
.group {
  position: relative;
  margin-bottom: 45px;
}
#course-name,
#course-address {
  font-size: 18px;
  padding: 10px 10px 10px 5px;
  display: block;
  width: 300px;
  border: none;
  border-bottom: 1px solid #757575;
}
#page-body > div:nth-child(1) > div > div > form > div:nth-child(1) > label,
#page-body > div:nth-child(1) > div > div > form > div:nth-child(2) > label {
  color: #184d47;
}
input:focus {
  outline: none;
}

/* LABEL ======================================= */
label {
  color: #184d47;
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
</style>