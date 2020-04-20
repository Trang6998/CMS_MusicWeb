import VueCkeditor from './VueCkeditor.vue';
const install = (Vue: any) => {
    Vue.component('v-ckeditor', VueCkeditor)
};

export default install