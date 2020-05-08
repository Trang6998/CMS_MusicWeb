<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Quản lý bài viết</h2>
                <v-row>
                    <v-col cols="6" class="pb-0">
                        <v-text-field v-model="searchParamsBaiViet.keyworlds"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo tiêu đề..."></v-text-field>
                    </v-col>
                    <v-col cols="4" class="pb-0">
                        <v-autocomplete v-model="searchParamsBaiViet.chuyenMucID"
                                        :items="dsChuyenMuc" clearable hide-details
                                        label="Chuyên mục bài viết"
                                        placeholder="Chọn chuyên mục..."
                                        item-text="TenChuyenMuc"
                                        item-value="ChuyenMucID">
                        </v-autocomplete>
                    </v-col>
                    <v-col cols="2" class="pb-0 pt-2">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="3" class="pt-1">
                        <v-datepicker v-model="searchParamsBaiViet.ngayDangTu"
                                      hide-details
                                      label="Từ ngày"></v-datepicker>
                    </v-col>
                    <v-col cols="3" class="pt-1">
                        <v-datepicker v-model="searchParamsBaiViet.ngayDangDen"
                                      hide-details
                                      label="Đến ngày"></v-datepicker>
                    </v-col>
                    <v-col cols="4" class="pt-1">
                        <v-autocomplete v-model="searchParamsBaiViet.nguoiDangID"
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
                                      :items="dsBaiViet"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsBaiViet"
                                      :server-items-length="searchParamsBaiViet.totalItems"
                                      :loading="loadingTable"
                                      class="table-border table">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsBaiViet" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">
                                            <span v-if="item.ChuyenMuc != undefined && item.ChuyenMuc.length > 0" v-for="(cd, id) in item.ChuyenMuc" :key="id">
                                                {{cd.TenChuyenMuc}} <span v-if="id != item.ChuyenMuc.length-1"> ,</span>
                                            </span>
                                        </td>
                                        <td class="text-center">{{ item.TieuDe }}</td>
                                        <td class="text-center">{{ item.NgayDang | moment("DD/MM/YYYY hh:mm") }}</td>
                                        <td class="text-center">{{ item.HoTen }}</td>
                                        <td class="text-center"><a @click="capNhatTrangThai(item)">{{ item.TrangThai ? "Hiện" : "Ẩn" }}</a></td>
                                        <td class="text-center">
                                            <v-layout nowrap>
                                                <v-btn text icon small @click="showModalThemSua(true, item)" class="ma-0">
                                                    <v-icon small>edit</v-icon>
                                                </v-btn>
                                                <v-btn text color="red" icon small class="ma-0" @click="confirmDelete(item)">
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
                    <v-btn color="red darken-1" @click.native="deleteBaiViet" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-bai-viet ref="themSuaBaiViet" @save='getDataFromApi(searchParamsBaiViet)'></them-sua-bai-viet>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import BaiVietApi, { BaiVietApiSearchParams } from '@/apiResources/BaiVietApi';
    import { BaiViet } from '@/models/BaiViet';
    import ChuyenMucApi, { ChuyenMucApiSearchParams } from '@/apiResources/ChuyenMucApi';
    import { ChuyenMuc } from '@/models/ChuyenMuc';
    import ThemSuaBaiViet from './ThemSuaBaiViet.vue';
import { NhanVien } from '../../../models/NhanVien';
import NhanVienApi, { NhanVienApiSearchParams } from '../../../apiResources/NhanVienApi';

    export default Vue.extend({
        components: {
            ThemSuaBaiViet
        },
        data() {
            return {
                dsBaiViet: [] as BaiViet[],
                dsChuyenMuc: [] as ChuyenMuc[],
                dsNhanVien: [] as NhanVien[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Chuyên đề', value: 'TenBaiViet', align: 'center', sortable: true },
                    { text: 'Tiêu đề', value: 'TenBaiViet', align: 'center', sortable: true },
                    { text: 'Ngày đăng', value: 'TrangThai', align: 'center', sortable: true },
                    { text: 'Người đăng', value: 'TrangThai', align: 'center', sortable: true },
                    { text: 'Trạng thái', value: 'TrangThai', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsBaiViet: { includeEntities: true, itemsPerPage: 10 } as BaiVietApiSearchParams,
                searchParamsChuyenMuc: { includeEntities: true, itemsPerPage: 0 } as ChuyenMucApiSearchParams,
                searchParamsNhanVien: { includeEntities: true, itemsPerPage: 0 } as NhanVienApiSearchParams,
                loadingTable: false,
                selectedBaiViet: {} as BaiViet,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
            this.getDanhSachChuyenMuc()
            this.getDanhSachNhanVien()
        },
        methods: {
            getDataFromApi(searchParamsBaiViet: BaiVietApiSearchParams): void {
                this.loadingTable = true;
                BaiVietApi.search(searchParamsBaiViet).then(res => {
                    this.dsBaiViet = res.Data;
                    this.searchParamsBaiViet.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            getDanhSachChuyenMuc() {
                ChuyenMucApi.search(this.searchParamsChuyenMuc).then(res => {
                    this.dsChuyenMuc = res.Data
                })
            },
            getDanhSachNhanVien() {
                NhanVienApi.search(this.searchParamsNhanVien).then(res => {
                    this.dsNhanVien = res.Data
                })
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaBaiViet as any).show(item.BaiVietID);
            },
            capNhatTrangThai(item: BaiViet) {
                item.TrangThai = !item.TrangThai
                this.loadingTable = true
                BaiVietApi.update(item.BaiVietID, item).then(res => {
                    this.loadingTable = false;
                    this.getDataFromApi(this.searchParamsBaiViet)
                }).catch(res => {
                    this.loadingTable = false;
                    this.$snotify.error('Cập nhật bài viết thất bại!');
                });
            },
            confirmDelete(baiViet: BaiViet): void {
                this.selectedBaiViet = baiViet;
                this.dialogConfirmDelete = true;
            },
            deleteBaiViet(): void {
                BaiVietApi.delete(this.selectedBaiViet.BaiVietID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsBaiViet);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

