import Vue from 'vue'
export default {
    install(Vue: any) {
        var $eventBus = new Vue();
        Vue.$eventBus = $eventBus;
        Vue.prototype.$eventBus = $eventBus;
    }
}