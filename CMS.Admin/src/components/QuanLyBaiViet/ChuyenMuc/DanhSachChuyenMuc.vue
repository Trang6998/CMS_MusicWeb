<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Quản lý chuyên mục</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsChuyenMuc.keyworlds"
                                      @input="getDataFromApi(searchParamsChuyenMuc)"
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
                                      :items="dsChuyenMuc"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsChuyenMuc"
                                      :server-items-length="searchParamsChuyenMuc.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsChuyenMuc" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.TenChuyenMuc }}</td>
                                        <td class="text-center"><a @click="capNhat(item, 1)">{{ item.TrangThai ? "Đang hoạt động" : "Dừng hoạt động" }}</a></td>
                                        <td class="text-center"><a @click="capNhat(item, 2)">{{ item.HienThiTrangChu ? "Hiện" : "Ẩn" }}</a></td>
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
                    <v-btn color="red darken-1" @click.native="deleteChuyenMuc" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-chuyenMuc ref="themSuaChuyenMuc" @save='getDataFromApi(searchParamsChuyenMuc)'></them-sua-chuyenMuc>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import ChuyenMucApi, { ChuyenMucApiSearchParams } from '@/apiResources/ChuyenMucApi';
    import { ChuyenMuc } from '@/models/ChuyenMuc';
    import ThemSuaChuyenMuc from './ThemSuaChuyenMuc.vue';

    export default Vue.extend({
        components: {
            ThemSuaChuyenMuc
        },
        data() {
            return {
                dsChuyenMuc: [] as ChuyenMuc[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Tên chuyên mục', value: 'TenChuyenMuc', align: 'center', sortable: true },
                    { text: 'Trạng thái', value: 'TrangThai', align: 'center', sortable: true },
                    { text: 'Hiển thị trang chủ', value: 'HienThiTrangChu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsChuyenMuc: { includeEntities: true, itemsPerPage: 10 } as ChuyenMucApiSearchParams,
                loadingTable: false,
                selectedChuyenMuc: {} as ChuyenMuc,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsChuyenMuc: ChuyenMucApiSearchParams): void {
                this.loadingTable = true;
                ChuyenMucApi.search(searchParamsChuyenMuc).then(res => {
                    this.dsChuyenMuc = res.Data;
                    this.searchParamsChuyenMuc.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaChuyenMuc as any).show(isUpdate, item);
            },
            capNhat(item: ChuyenMuc, loai: number) {
                if (loai == 1)
                    item.TrangThai = !item.TrangThai
                else
                    item.HienThiTrangChu = !item.HienThiTrangChu
                this.loadingTable = true
                ChuyenMucApi.update(item.ChuyenMucID, item).then(res => {
                    this.loadingTable = false;
                    this.getDataFromApi(this.searchParamsChuyenMuc)
                }).catch(res => {
                    this.loadingTable = false;
                    this.$snotify.error('Cập nhật chuyên mục thất bại!');
                });
            },
            confirmDelete(chuyenMuc: ChuyenMuc): void {
                this.selectedChuyenMuc = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteChuyenMuc(): void {
                ChuyenMucApi.delete(this.selectedChuyenMuc.ChuyenMucID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsChuyenMuc);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

