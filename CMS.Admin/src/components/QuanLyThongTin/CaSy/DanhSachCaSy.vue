<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách ca sỹ</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsCaSy.keyworlds"
                                      @input="getDataFromApi(searchParamsCaSy)"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo tên chuyên mục..."></v-text-field>
                    </v-col>
                    <v-col cols="6">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small>add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" class="pt-0">
                        <v-data-table :headers="tableHeader"
                                      :items="dsCaSy"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsCaSy"
                                      :server-items-length="searchParamsCaSy.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsCaSy" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.HoTen }}</td>
                                        <td class="text-center">{{ item.BietDanh }}</td>
                                        <td class="text-center">{{ item.SoBaiHat }}</td>
                                        <td class="text-center">{{ item.NgaySinh | moment("DD/MM/YYYY") }}</td>
                                        <td class="text-center">{{ item.SoDienThoai }}</td>
                                        <td class="text-center">{{ item.FaceBook }}</td>
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
                    <v-btn color="red darken-1" @click.native="deleteCaSy" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-ca-sy ref="themSuaCaSy" @save='getDataFromApi(searchParamsCaSy)'></them-sua-ca-sy>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import CaSyApi, { CaSyApiSearchParams } from '@/apiResources/CaSyApi';
    import { CaSy } from '@/models/CaSy';
    import ThemSuaCaSy from './ThemSuaCaSy.vue';

    export default Vue.extend({
        components: {
            ThemSuaCaSy
        },
        data() {
            return {
                dsCaSy: [] as CaSy[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Họ tên', value: 'HoTen', align: 'center', sortable: true },
                    { text: 'Biệt danh', value: 'TenCaSy', align: 'center', sortable: true },
                    { text: 'Số bài hát', value: 'TenCaSy', align: 'center', sortable: true },
                    { text: 'Ngày sinh', value: 'TrangThai', align: 'center', sortable: true },
                    { text: 'Số điện thoại', value: 'HienThiTrangChu', align: 'center', sortable: true },
                    { text: 'Facebook', value: 'HienThiTrangChu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsCaSy: { includeEntities: true, itemsPerPage: 10 } as CaSyApiSearchParams,
                loadingTable: false,
                selectedCaSy: {} as CaSy,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsCaSy: CaSyApiSearchParams): void {
                this.loadingTable = true;
                CaSyApi.search(searchParamsCaSy).then(res => {
                    this.dsCaSy = res.Data;
                    this.searchParamsCaSy.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaCaSy as any).show(isUpdate, item);
            },
            confirmDelete(chuyenMuc: CaSy): void {
                this.selectedCaSy = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteCaSy(): void {
                CaSyApi.delete(this.selectedCaSy.CaSyID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsCaSy);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

