import Vue from 'vue'
import App from './App.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'

// npm install for vue2-google-maps
import * as VueGoogleMaps from 'vue2-google-maps'

Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

// loading Google Maps API key
Vue.use(VueGoogleMaps, {
  load: {
    key: 'AIzaSyD5yBxq_6nK0k-w_yowJCoWgtbO29zlt90',
    libraries: 'places',
  }
})

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
