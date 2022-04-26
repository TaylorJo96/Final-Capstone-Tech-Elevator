<template>
  <div>
    <league-form />
  </div>
</template>

<script>
import LeagueForm from "../components/LeagueForm.vue";
export default {
  components: { LeagueForm },
  name: "home",
};
</script>

<style scoped>
#league-div {
  display: flex;
  flex-direction: column;
  padding: 20px;
}
div > #league-list {
  display: flex;
  flex-direction: column;
  padding: 5px;
  justify-content: center;
  width: 60vw;
}

#league-list > #league-names > a:hover {
  background-color: rgb(58, 124, 117);
}
#league-names {
  display: flex;
  justify-content: center;
}

#page-body > div:nth-child(1) > div > form {
  /* background-color: rgba(209, 255, 209, 0.5); */
  /* border-color: rgb(43, 126, 43); */
  /* border-style: solid;
  border-width: 3px; */
  border-radius: 6px;
  padding: 5px;
  /* margin: 30px; */
  width: 80%;
  filter: drop-shadow(5px 5px 5px black);
  display: flex;
  margin-top: 20px;
  margin-bottom: 20px;
  flex-direction: column;
  /* justify-content: space-around; */
  align-items: center;
}
#page-body > div:nth-child(1) > div {
  display: flex;
  justify-content: space-around;
}
</style>