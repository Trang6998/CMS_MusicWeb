import Vue from 'vue'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'

import en from './CalendarApp/locales/en'
import vi from './CalendarApp/locales/vi'
import DaySpanVuetify from './CalendarApp/plugin'

Vue.use(Vuetify, {
})
Vue.use(DaySpanVuetify, {
    data:
    {
        locales: { vi, en }
    },
    methods: {
        getDefaultEventColor: () => '#1976d2'
    }
});
