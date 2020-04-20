import DatePicker from './DatePicker.vue';
import DateRangePicker from './DateRangePicker.vue';
import TimePicker from './TimePicker.vue';
import DateTimePicker from './DateTimePicker.vue';
const install = (Vue: any) => {
    Vue.component('v-datepicker', DatePicker),
        Vue.component('v-daterangepicker', DateRangePicker),
        Vue.component('v-timepicker', TimePicker),
        Vue.component('v-datetimepicker', DateTimePicker)
};

export default install