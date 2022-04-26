<template>
  <div id="map-block">
    <div id="mapHead">
    <h3 id="search">Search for golf courses</h3>
    <input
      type="text"
      id="course-search"
      class="form-search"
      placeholder="Search Map"
      v-model="courseSearch"
    />

    <button  id="searchButton" v-on:click="locationToSearch()">Search</button>
    <button id="hideButton" v-if="showButton === true" v-on:click="hideButton()">Hide</button>
    </div>
    <div class="map">
      <iframe
        class="search-map"
        referrerpolicy="no-referrer-when-downgrade"
        v-bind:src="[srcString]"
        v-if="showMap === true"
        allowfullscreen
      >
      </iframe>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      courseSearch: "",
      showMap: false,
      showButton: false,
      srcString: "",
    };
  },
  methods: {
    locationToSearch() {
      this.showMap = true;
      this.showButton = true;
      this.srcString = `https://www.google.com/maps/embed/v1/search?key=AIzaSyCMwjT48KCWWEad9HoL0IRtRAEG2rB96os&q=golf+course+${this.courseSearch}`;
    },
    hideButton() {
      this.showMap = false;
      this.showButton = false;
    },
  },
};
</script>

<style>
.search-map {
  /* width: 100%; */
  height: 30vh;
  width: 60vw;
}
#mapHead{
  background-color: #D6EFC7;
  border-radius: 5px;
  margin:5px;
  padding:10px;
  padding-right:15px;
    filter: drop-shadow(3px 3px 3px black);
}
#map-block > div.map
{

      filter: drop-shadow(3px 3px 3px black);

}
 

#search {
  /* filter: drop-shadow(3px 3px 3px black); */
  filter: drop-shadow(3px 3px 3px black);
 background-color: #184d47;
  text-decoration: none;
  font-weight: bold;
  text-shadow: black 3px 3px 3px;
  color: white;
  font-size: x-large;
  display: flex;
  width: 100%;
  justify-content: center;
  height:40px;
  display: flex;
  align-items: center;
    font-family: Cambria, Cochin, Georgia, Times, "Times New Roman", serif;
text-transform: capitalize;
padding-left:5px;

border-radius: 5px;
margin:0;
  margin-bottom: 5px;
  /* margin-right:10px; */

}

#map {
  /* controls box around map */
  /* display: flex;
  justify-content: center; */
  background-color: rgba(209, 255, 209, 0.5);
  /* border-color: rgb(43, 126, 43); */
  /* border-style: solid;
  border-width: 3px; */
  border-radius: 6px;
  padding: 5px;
  /* margin: 30px; */
  width: 60%;
  filter: drop-shadow(5px 5px 5px black);
  display: flex;
  margin-top: 20px;
  margin-bottom: 20px;
  /* margin-left:30%; */
  /* justify-content: space-around; */
  /* padding: 4%;  */
  padding-top: 2%;
  height: 80%;
}
#searchButton {
background-color:rgb(24, 77, 71) ; 
padding:7px;
border-radius: 10px;
color:white;
font-weight: bolder;
margin: 6px;
  transition: all 0.2s ease-in-out;



}
#searchButton:hover{
transform: scale(1.2);
background-color:rgb(45, 158, 145);

}
#hideButton{
background-color:rgb(24, 77, 71) ; 
padding:7px;
border-radius: 10px;
color:white;
font-weight: bolder;
margin: 6px;
  transition: all 0.2s ease-in-out;
}
#hideButton:hover{
transform: scale(1.2);
background-color:rgb(45, 158, 145);

}



</style>