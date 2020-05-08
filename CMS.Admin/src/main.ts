import '@babel/polyfill'
import Vue from 'vue';
import '@mdi/font/css/materialdesignicons.css'
import Vuetify from 'vuetify';
require('@/plugins/vuetify');
import 'vuetify/dist/vuetify.min.css';
import App from './App.vue';
import router from './router';
import store from './store/store';
import './registerServiceWorker';
import VeeValidate, { Validator } from "vee-validate";
import Snotify from 'vue-snotify';
import { SnotifyService } from 'vue-snotify/SnotifyService';
import * as moment from 'moment';
import DateTimePicker from '@/components/Commons/DateTimePicker';
import VChooseFile from '@/components/Commons/VChooseFile';
import VueCkeditor from '@/components/Commons/VueCkeditor';
import EventBus from '@/EventBus';
import VueTheMask from 'vue-the-mask'
Vue.use(VueTheMask)

import CommonFunctions, { CommonFunctionsService } from './Utils/CommonFunctions';
Vue.use(DateTimePicker);
Vue.use(VChooseFile);
Vue.use(VueCkeditor); 

require('moment/locale/vi');

Vue.use(require('vue-moment'), {
    moment
});
Vue.use(EventBus); 
Vue.use(CommonFunctions);
Vue.use(VeeValidate);
Validator.localize('vi');
Vue.use(Snotify);

const opts = {
    dark: true,
    icons: {
        iconfont: 'mdi',
    },
} as any
Vue.use(Vuetify);

Vue.config.productionTip = false;

declare module 'vue/types/vue' {
    interface Vue { 
        $snotify: SnotifyService,
        $moment: any
    }
}
new Vue({
    vuetify: new Vuetify(opts),
    router,
    store,
    render: (h) => h(App),
}).$mount('#app');
