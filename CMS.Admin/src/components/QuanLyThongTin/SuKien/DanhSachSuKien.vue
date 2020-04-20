<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách sự kiện</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsEvent.keyworlds"
                                      @input="getDataFromApi(searchParamsEvent)"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo tiêu đề..."></v-text-field>
                    </v-col>
                    <v-col cols="6">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small>add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" class="pt-0">
                        <v-data-table :headers="tableHeader"
                                      :items="dsEvent"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsEvent"
                                      :server-items-length="searchParamsEvent.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsEvent" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.TieuDe }}</td>
                                        <td class="text-center">{{ item.ThoiGianTu | moment("DD/MM/YYYY") }}</td>
                                        <td class="text-center">{{ item.ThoiGianTu | moment("LT") }} - {{ item.ThoiGianDen | moment("LT") }}</td>
                                        <td class="text-center">{{ item.DiaDiem }}, {{item.XaPhuong.TenXaPhuong}}, {{item.QuanHuyen.TenQuanHuyen}}, {{item.TinhThanh.TenTinhThanh}}</td>
                                        <td>
                                            <v-layout nowrap style="place-content: center">
                                                <v-btn text icon small @click="showModalThemSua(true, item)" class="ma-0">
                                                    <v-icon small>edit</v-icon>
                                                </v-btn>
                                                <v-btn text icon small color="red" class="ma-0" @click="confirmDelete(item)">
                                                    <v-icon small>delete</v-icon>
                                                </v-btn>
                                            </v-layout>
                                        </td>
                                    </tr>
                                </tbody>

                            </template>
                        </v-data-table>
                    </v-col>
                </v-row>
            </v-card-text>
        </v-card>
        <v-dialog v-model="dialogConfirmDelete" max-width="290">
            <v-card>
                <v-card-title class="headline">Xác nhận xóa</v-card-title>
                <v-card-text class="pt-3">Bạn có chắc chắn muốn xóa?</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn @click.native="dialogConfirmDelete=false" text>Hủy</v-btn>
                    <v-btn color="red darken-1" @click.native="deleteEvent" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-su-kien ref="themSuaEvent" @save='getDataFromApi(searchParamsEvent)'></them-sua-su-kien>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import EventApi, { EventApiSearchParams } from '@/apiResources/EventApi';
    import { Event } from '@/models/Event';
    import ThemSuaSuKien from './ThemSuaSuKien.vue';

    export default Vue.extend({
        components: {
            ThemSuaSuKien
        },
        data() {
            return {
                dsEvent: [] as Event[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Tiêu đề', value: 'TenEvent', align: 'center', sortable: true },
                    { text: 'Ngày tổ chức', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Thời gian', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Địa điểm', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsEvent: { includeEntities: true, itemsPerPage: 10 } as EventApiSearchParams,
                loadingTable: false,
                selectedEvent: {} as Event,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsEvent: EventApiSearchParams): void {
                this.loadingTable = true;
                EventApi.search(searchParamsEvent).then(res => {
                    this.dsEvent = res.Data;
                    this.searchParamsEvent.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaEvent as any).show(isUpdate, item);
            },
            confirmDelete(chuyenMuc: Event): void {
                this.selectedEvent = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteEvent(): void {
                EventApi.delete(this.selectedEvent.EventID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsEvent);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

