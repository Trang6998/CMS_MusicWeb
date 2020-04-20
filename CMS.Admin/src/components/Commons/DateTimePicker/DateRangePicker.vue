<template>
    <div>
        <v-text-field :value="dateRangeText"
                      :label="label"
                      mask="date"
                      :prepend-icon="prependIcon"
                      :append-icon="appendIcon"
                      :clearable="clearable"
                      :hide-details="hideDetails"
                      :disabled="disabled"
                      placeholder="dd/mm/yyyy"
                      :error-messages="errorMessages"
                      @click:append="showMenuDate"
                      :class="classCss"></v-text-field>
        <v-dialog ref="dialogDate"
                  v-model="menuDate"
                  :return-value.sync="dates"
                  lazy
                  full-width
                  width="290px">
            <v-date-picker v-model="dates"
                           :max="valueMax" locale="vi-vn"
                           :min="valueMin"
                           range>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="menuDate = false">Hủy</v-btn>
                <v-btn text color="primary" @click="$refs.dialogDate.save(dates); saveDate(dates)">OK</v-btn>
            </v-date-picker>
        </v-dialog>
    </div>
</template>

<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    export default Vue.extend({
        props: {
            value: {
                type: [Date, String],
                default: null
            },
            label: {
                type: String,
                default: ''
            },
            clearable: {
                type: Boolean,
                default: false
            },
            format: {
                type: String,
                default: 'DD/MM/YYYY'
            },
            formatInput: {
                type: String,
                default: 'YYYY-MM-DD'
            },
            max: {
                type: [Date, String],
                default: null
            },
            min: {
                type: [Date, String],
                default: null
            },
            disabled: {
                type: Boolean,
                default: false
            },
            hideDetails: {
                type: Boolean,
                default: false
            },
            appendIcon: {
                type: String,
                default: 'event'
            },
            prependIcon: {
                type: String,
                default: null
            },
            errorMessages: {
                default: null
            },
            classCss: {
                type: String,
                default: ''
            }
        },
        data() {
            return {
                menuDate: false,
                menuTime: false,
                selectedDate: null as any,
                valueMax: null,
                valueMin: null,
                x: 0,
                y: 0,
                dates: ['2019-09-10', '2019-09-20'] as any[]
            }
        },
        created() {

        },
        computed: {
            dateRangeText(): any {
                return this.dates.join(' ~ ');
            },
            //dateTime(): any {
            //    if (this.selectedDate != undefined && this.selectedDate != null) {
            //        return this.$moment(this.selectedDate).format();
            //    }
            //    else {
            //        return null;
            //    }
            //},
            //dateString(): any {
            //    return this.selectedDate.join(' ~ ');
            //}
        },
        methods: {
            //dateInput(val: any) {
            //    if (val.length == 8 && this.$moment(val, "DDMMYYYY").isValid()) {
            //        this.selectedDate = this.$moment(val, "DDMMYYYY").format('YYYY-MM-DD');
            //        if (this.max !== null && this.$moment(this.dateTime) > this.$moment(this.max)) {
            //            this.selectedDate = this.$moment(this.max).format('YYYY-MM-DD');
            //        }
            //        else {
            //            this.$emit('input', this.dateTime);
            //        }
            //    }
            //},
            showMenuDate() {
                this.menuDate = true;
            },
            //showMenuTime() {
            //    this.menuTime = true;
            //},
            //saveDate(date: any) {
            //    this.$emit('input', this.dateTime)
            //    this.menuDate = false
            //},
            //saveTime(time: any) {
            //    this.$emit('input', this.dateTime)
            //    this.menuTime = false
            //},
            //inputDate(val: any) {
            //    this.$emit('input', this.dateTime)
            //    this.menuDate = false
            //},
            //inputTime(val: any) {
            //    this.$emit('input', this.dateTime)
            //}
        },
        watch: {
            //max(val) {
            //    this.valueMax = this.max === null ? null : this.$moment(this.max, this.formatInput).format("YYYY-MM-DD");
            //},
            //min(val) {
            //    this.valueMin = this.min === null ? null : this.$moment(this.min, this.formatInput).format("YYYY-MM-DD");
            //},
            //value(val) {
            //    if (val != null && val != undefined && val != this.dateTime) {
            //        var date = val;
            //        this.selectedDate = this.$moment(date).format("YYYY-MM-DD");
            //    } else if (val != this.dateTime) {
            //        this.selectedDate = null;
            //    }
            //}
        },
        mounted() {
            //let val = this.value;
            //if (val != null && val != undefined && val != this.dateTime) {
            //    var date = val;
            //    this.selectedDate = this.$moment(date).format("YYYY-MM-DD");
            //} else if (val != this.dateTime) {
            //    this.selectedDate = null;
            //}
        }
    })
</script>