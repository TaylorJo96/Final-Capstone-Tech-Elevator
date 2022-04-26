<template>
  <div id="add-course-box">
    <div id="add-course-header">
      <h1>Add A Course</h1>
    </div>
    <div>
      <course-form />
    </div>
  </div>
</template>

<script>
import CourseForm from "../components/CourseForm.vue";
export default {
  components: { CourseForm },
  data() {
    return {};
  },
};
</script>

<style>
#add-course-box {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>