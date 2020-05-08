<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách tài khoản nhân viên</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsUser.q"
                                      @input="getDataFromApi(searchParamsUser)"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo họ tên, số điện thoại..."></v-text-field>
                    </v-col>
                    <v-col cols="6">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small>add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" class="pt-0">
                        <v-data-table :headers="tableHeader"
                                      :items="dsUser"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsUser"
                                      :server-items-length="searchParamsUser.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsUser" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.NhanVien ?  item.NhanVien.HoTen : ""}}</td>
                                        <td class="text-center">{{ item.NhanVien ?  item.NhanVien.SoDienThoai : ""}}</td>
                                        <td class="text-center">{{ item.NhanVien.LoaiNhanVien ?  item.NhanVien.LoaiNhanVien.TenLoai : "" }}</td>
                                        <td class="text-center">{{ item.UserName }}</td>
                                        <td class="text-center">{{ item.Active ?  "Đang hoạt động" : "Dừng hoạt động"}}</td>
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
                    <v-btn color="red darken-1" @click.native="deleteUser" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-nhan-vien ref="themSuaUser" @save='getDataFromApi(searchParamsUser)'></them-sua-nhan-vien>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import UsersApi, { UserApiSearchAsyncParams } from '@/apiResources/UsersApi';
    import { User } from '@/models/User';
    import ThemSuaNhanVien from './ThemSuaNhanVien.vue';

    export default Vue.extend({
        components: {
            ThemSuaNhanVien
        },
        data() {
            return {
                dsUser: [] as User[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Họ tên', value: 'TenUser', align: 'center', sortable: true },
                    { text: 'Số điện thoại', value: 'TenUser', align: 'center', sortable: true },
                    { text: 'Loại nhân viên', value: 'TenUser', align: 'center', sortable: true },
                    { text: 'Tài khoản', value: 'TenUser', align: 'center', sortable: true },
                    { text: 'Trạng thái', value: 'TenUser', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsUser: { includeEntities: true, itemsPerPage: 10 } as UserApiSearchAsyncParams,
                loadingTable: false,
                selectedUser: {} as User,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsUser: UserApiSearchAsyncParams): void {
                this.loadingTable = true;
                UsersApi.searchAsync(searchParamsUser).then(res => {
                    this.dsUser = res.Data;
                    this.searchParamsUser.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaUser as any).show(isUpdate, item);
            },
            confirmDelete(chuyenMuc: User): void {
                this.selectedUser = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteUser(): void {
                UsersApi.deleteAsync(this.selectedUser.UserId).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsUser);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

