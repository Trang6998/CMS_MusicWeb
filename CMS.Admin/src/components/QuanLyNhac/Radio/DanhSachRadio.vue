<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách radio</h2>
                <v-row>
                    <v-col cols="6" class="pb-0">
                        <v-text-field v-model="searchParamsRadio.keyworlds"
                                      @input="getDataFromApi(searchParamsRadio)"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo tên chuyên mục..."></v-text-field>
                    </v-col>
                    <v-col cols="6" class="pb-0">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small>add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="3" class="pt-1">
                        <v-datepicker v-model="searchParamsRadio.ngayDangTu"
                                      hide-details
                                      label="Từ ngày"></v-datepicker>
                    </v-col>
                    <v-col cols="3" class="pt-1">
                        <v-datepicker v-model="searchParamsRadio.ngayDangDen"
                                      hide-details
                                      label="Đến ngày"></v-datepicker>
                    </v-col>
                    <v-col cols="4" class="pt-1">
                        <v-autocomplete v-model="searchParamsRadio.nguoiDangID"
                                        :items="dsNhanVien" clearable hide-details
                                        label="Người đăng"
                                        placeholder="Chọn người đăng..."
                                        item-text="HoTen"
                                        item-value="NhanVienID">
                        </v-autocomplete>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" class="pt-0">
                        <v-data-table :headers="tableHeader"
                                      :items="dsRadio"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsRadio"
                                      :server-items-length="searchParamsRadio.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsRadio" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.TieuDe }}</td>
                                        <td class="text-center">{{ item.NgayDang | moment("DD/MM/YYYY hh:mm") }}</td>
                                        <td class="text-center">{{ item.NhanVien ? item.NhanVien.HoTen : "" }}</td>
                                        <td class="text-center">{{ item.LuotXem }}</td>
                                        <td class="text-center"><a @click="capNhatTrangThai(item)">{{ item.TrangThai == true? "Hiện" : "Ẩn" }}</a></td>
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
                    <v-btn color="red darken-1" @click.native="deleteRadio" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-radio ref="themSuaRadio" @save='getDataFromApi(searchParamsRadio)'></them-sua-radio>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import RadioApi, { RadioApiSearchParams } from '@/apiResources/RadioApi';
    import { Radio } from '@/models/Radio';
    import ThemSuaRadio from './ThemSuaRadio.vue';
import NhanVienApi, { NhanVienApiSearchParams } from '../../../apiResources/NhanVienApi';
import { NhanVien } from '../../../models/NhanVien';

    export default Vue.extend({
        components: {
            ThemSuaRadio
        },
        data() {
            return {
                dsRadio: [] as Radio[],
                dsNhanVien: [] as NhanVien[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Tiêu đề', value: 'HoTen', align: 'center', sortable: true },
                    { text: 'Ngày đăng', value: 'TenRadio', align: 'center', sortable: true },
                    { text: 'Người đăng', value: 'TenRadio', align: 'center', sortable: true },
                    { text: 'Số lượt xem', value: 'TrangThai', align: 'center', sortable: true },
                    { text: 'Trạng thái', value: 'HienThiTrangChu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsRadio: { includeEntities: true, itemsPerPage: 10 } as RadioApiSearchParams,
                searchParamsNhanVien: { includeEntities: true, itemsPerPage: 0 } as NhanVienApiSearchParams,
                loadingTable: false,
                selectedRadio: {} as Radio,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
            this.getDanhSachNhanVien()
        },
        methods: {
            getDataFromApi(searchParamsRadio: RadioApiSearchParams): void {
                this.loadingTable = true;
                RadioApi.search(searchParamsRadio).then(res => {
                    this.dsRadio = res.Data;
                    this.searchParamsRadio.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaRadio as any).show(isUpdate, item);
            },
            capNhatTrangThai(item: Radio) {
                item.TrangThai = !item.TrangThai;
                this.loadingTable = true
                RadioApi.update(item.RadioID, item).then(res => {
                    this.loadingTable = false;
                    this.getDataFromApi(this.searchParamsRadio)
                }).catch(res => {
                    this.loadingTable = false;
                    this.$snotify.error('Cập nhật radio thất bại!');
                });
            },
            getDanhSachNhanVien() {
                NhanVienApi.search(this.searchParamsNhanVien).then(res => {
                    this.dsNhanVien = res.Data
                })
            },
            confirmDelete(chuyenMuc: Radio): void {
                this.selectedRadio = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteRadio(): void {
                RadioApi.delete(this.selectedRadio.RadioID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsRadio);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

