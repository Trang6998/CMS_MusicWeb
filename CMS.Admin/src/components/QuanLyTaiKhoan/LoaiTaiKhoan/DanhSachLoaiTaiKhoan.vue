<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách thể loại tài khoản</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsLoaiNhanVien.keyworlds"
                                      @input="getDataFromApi(searchParamsLoaiNhanVien)"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo tên loại..."></v-text-field>
                    </v-col>
                    <v-col cols="6">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small>add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" class="pt-0">
                        <v-data-table :headers="tableHeader"
                                      :items="dsLoaiNhanVien"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsLoaiNhanVien"
                                      :server-items-length="searchParamsLoaiNhanVien.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsLoaiNhanVien" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.TenLoai }}</td>
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
                    <v-btn color="red darken-1" @click.native="deleteLoaiNhanVien" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-loai-tai-khoan ref="themSuaLoaiNhanVien" @save='getDataFromApi(searchParamsLoaiNhanVien)'></them-sua-loai-tai-khoan>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import LoaiNhanVienApi, { LoaiNhanVienApiSearchParams } from '@/apiResources/LoaiNhanVienApi';
    import { LoaiNhanVien } from '@/models/LoaiNhanVien';
    import ThemSuaLoaiTaiKhoan from './ThemSuaLoaiTaiKhoan.vue';

    export default Vue.extend({
        components: {
            ThemSuaLoaiTaiKhoan
        },
        data() {
            return {
                dsLoaiNhanVien: [] as LoaiNhanVien[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Tên loại', value: 'TenLoaiNhanVien', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsLoaiNhanVien: { includeEntities: true, itemsPerPage: 10 } as LoaiNhanVienApiSearchParams,
                loadingTable: false,
                selectedLoaiNhanVien: {} as LoaiNhanVien,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsLoaiNhanVien: LoaiNhanVienApiSearchParams): void {
                this.loadingTable = true;
                LoaiNhanVienApi.search(searchParamsLoaiNhanVien).then(res => {
                    this.dsLoaiNhanVien = res.Data;
                    this.searchParamsLoaiNhanVien.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaLoaiNhanVien as any).show(isUpdate, item);
            },
            confirmDelete(chuyenMuc: LoaiNhanVien): void {
                this.selectedLoaiNhanVien = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteLoaiNhanVien(): void {
                LoaiNhanVienApi.delete(this.selectedLoaiNhanVien.LoaiNhanVienID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsLoaiNhanVien);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

