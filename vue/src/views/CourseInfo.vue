<template>
  <div>
    <h3>{{ course.courseName }}</h3>

    <iframe
      width="600"
      height="450"
      style="border: 0"
      loading="lazy"
      allowfullscreen
      referrerpolicy="no-referrer-when-downgrade"
      v-bind:src="[srcString]"
    >
    </iframe> 
    <h3>Address: {{ course.address }}</h3>
 <vue-weather
        v-if="showForecast == true"
        api-key="8967dc1398824eb30d8ee9b314182915"
        units="us"
        v-bind:latitude="[geometry.lat]"
        v-bind:longitude="[geometry.lon]"
      />
   
    <h3>Active Leagues:</h3>
    <div>
      <router-link
        :to="{ path: '/league/' + league.leagueId }"
        v-for="league in leagueList"
        v-bind:key="league.leagueId"
        v-text="`${league.leagueName}` + ' '"
      >
      </router-link>
     
    </div>
  </div>
</template>

<script>
import CourseService from "../services/CourseService.js";
import VueWeather from "vue-weather-widget";
export default {
  data() {
    return {
      course: {
        courseId: "",
        courseName: "",
        address: "",
      },
      leagueList: [],
      srcString: "",
      geometry: {},
      showForecast: false,
    };
  },
  components: {
    VueWeather,
  },
  created() {
    CourseService.getCourseById(this.$route.params.id).then((response) => {
      this.course = response.data;
      this.srcString = `https://www.google.com/maps/embed/v1/place?key=AIzaSyCMwjT48KCWWEad9HoL0IRtRAEG2rB96os&q=${this.course.courseName}`;
      CourseService.getLeaguesByCourseId(this.course.courseId).then(
        (response) => {
          this.leagueList = response.data;
          CourseService.getCoordinates(
            this.course.address.substr(this.course.address.length - 5)
          ).then((response) => {
            this.geometry = response.data;
            this.showForecast = true;
          });
        }
      );
    });
  },
};
</script>

<style>
#page-body > div:nth-child(1){
  display: flex;
  justify-content: center;
  align-items: center;
}
#page-body > div:nth-child(1) > div > div{
width:100%

}
#page-body > div:nth-child(1) > div{
  margin: 0px;
}
#page-body > div:nth-child(1) > div.vww__widget > div.vww__content > div.vww__currently{
  /* width:150px; */
  margin-right:20px;
 
}
#page-body > div:nth-child(1) > div > div > div.vww__content > div.vww__daily{
  width:800px;
 
  /* height:100% */
}
#page-body > div:nth-child(1) > div > div > div.vww__content{
  height: 200px;
 
}
#page-body > div:nth-child(1) > div.vww__widget > div.vww__content > div.vww__daily{
  width: 600px;
  margin-top:20px;
  background: #D6EFC7;
  padding:5px;
  border-radius:10px;
}
#page-body > div:nth-child(1) > div:nth-child(6) > a{
  background: #FAD586;
  text-decoration: none;
  padding:20px;
  color: #184d47;
  font-weight:bolder;
      font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
border-radius: 20px;
transition: all 0.2s ease-in-out;
}
#page-body > div:nth-child(1) > div:nth-child(6) > a:hover{
   background: #f5c45b;
    transform: scale(1.1);
}


#page-body > div:nth-child(1) > div.vww__widget > div.vww__content > div.vww__currently{
 margin-top:10px;
 background: #FAD586;
  padding-top:20px;
  border-radius: 10px;;
}
#page-body > div:nth-child(1) > div.vww__widget > div.vww__header{
 
  color:white;filter: drop-shadow(3px 3px 3px black);
 background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  text-shadow: black 5px 5px 5px;
  color: white;
  font-size: xx-large;
  padding:5px;
  display: flex;
  justify-content: center;
      font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;

}
#page-body > div:nth-child(1) > h3:nth-child(1){
 display: flex;
  justify-content: center;
  background: #fad586;
  padding: 2px;
  filter: drop-shadow(2px 2px 2px black);
  border-bottom: #22577a 3px solid;
  border-top: #22577a 3px solid;
  text-transform: capitalize;
  color: #22577a;
  font-variant: small-caps;
  font-family: Cambria, Cochin, Georgia, Times, "Times New Roman";
  margin-bottom:20px;
}

</style>