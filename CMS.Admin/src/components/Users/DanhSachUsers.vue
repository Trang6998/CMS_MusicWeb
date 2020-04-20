<template>
    <v-flex xs12>
        <v-card>
            <v-card-text>
                <h2>Quản lý tài khoản</h2>
                <v-layout row nowrap>
                    <v-text-field v-model="searchParamsUsers.q"
                                  @input="getDataFromApi(searchParamsUsers)"
                                  hide-details
                                  append-icon="search"
                                  label="Tìm kiếm"
                                  placeholder="Tìm kiếm theo tên, email..."
                                  class="mb-2 ml-1"></v-text-field>
                    <v-spacer></v-spacer>
                    <v-btn @click="showModalThemSua(false, {})" color="teal lighten-2" style="margin-top: 15px" small><v-icon small class="px-0">add</v-icon> Thêm Mới</v-btn>
                </v-layout>
                <v-layout row wrap>
                    <v-flex xs12>
                        <v-data-table :headers="tableHeader"
                                      :items="dsUsers"
                                      @update:pagination="getDataFromApi" :pagination.sync="searchParamsUsers"
                                      :total-items="searchParamsUsers.totalItems"
                                      :loading="loadingTable"
                                      class="table-border table">
                            <template slot="items" slot-scope="props">
                                <td class="text-xs-center">{{ props.item.UserName }}</td>
                                <td class="text-xs-center">{{ props.item.Email }}</td>
                                <td class="text-xs-center">{{ props.item.CreatedTime | moment('DD/MM/YYYY, HH:mm') }}</td>
                                <td class="text-xs-center">{{ props.item.Active ? "Đang hoạt động" : "Dừng hoạt động" }}</td>
                                <td class="text-xs-center">
                                    <v-layout nowrap>
                                        <v-btn flat icon small  @click="showModalThemSua(true, props.item)" class="ma-0">
                                            <v-icon small>edit</v-icon>
                                        </v-btn>
                                        <v-btn flat color="red" icon small class="ma-0" @click="confirmDelete(props.item)">
                                            <v-icon small>delete</v-icon>
                                        </v-btn>
                                    </v-layout>
                                </td>
                            </template>
                        </v-data-table>
                    </v-flex>
                </v-layout>
            </v-card-text>
        </v-card>
        <v-dialog v-model="dialogConfirmDelete" max-width="290">
            <v-card>
                <v-card-title class="headline">Xác nhận xóa</v-card-title>
                <v-card-text class="pt-3">Bạn có chắc chắn muốn xóa?</v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn @click.native="dialogConfirmDelete=false" flat>Hủy</v-btn>
                    <v-btn color="red darken-1" @click.native="deleteUsers" flat>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-users ref="themSuaUsers" @save='getDataFromApi(searchParamsUsers)'></them-sua-users>
    </v-flex>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import UserApi, { UserApiSearchAsyncParams } from '@/apiResources/UsersApi';
    import { User } from '@/models/User';
    import ThemSuaUsers from './ThemSuaUsers.vue';
import UsersApi from '@/apiResources/UsersApi';

    export default Vue.extend({
        components: {
            ThemSuaUsers
        },
        data() {
            return {
                dsUsers: [] as User[],
                tableHeader: [
                    { text: 'Tài khoản', value: 'UserName', align: 'center', sortable: true },
                    { text: 'Email', value: 'Email', align: 'center', sortable: true },
                    { text: 'Ngày tạo', value: 'CreatedTime', align: 'center', sortable: true },
                    { text: 'Hoạt động', value: 'Active', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsUsers: { includeEntities: true, rowsPerPage: 10 } as UserApiSearchAsyncParams,
                loadingTable: false,
                selectedUsers: {} as User,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsUsers: UserApiSearchAsyncParams): void {
                this.loadingTable = true;
                UserApi.searchAsync(searchParamsUsers).then(res => {
                    this.dsUsers = res.Data;
                    this.searchParamsUsers.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaUsers as any).show(isUpdate, item);
            },
            confirmDelete(users: User): void {
                this.selectedUsers = users;
                this.dialogConfirmDelete = true;
            },
            deleteUsers(): void {
                UsersApi.deleteAsync(this.selectedUsers.UserId).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsUsers);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

