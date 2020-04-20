<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách Album</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsAlbum.keyworlds"
                                      @input="getDataFromApi(searchParamsAlbum)"
                                      hide-details
                                      append-icon="search"
                                      label="Tìm kiếm"
                                      placeholder="Tìm kiếm theo tên..."></v-text-field>
                    </v-col>
                    <v-col cols="6">
                        <v-btn @click="showModalThemSua(false, {})" color="primary" style="margin-top: 30px; float: right" small><v-icon small>add</v-icon> Thêm Mới</v-btn>
                    </v-col>
                </v-row>
                <v-row>
                    <v-col cols="12" class="pt-0">
                        <v-data-table :headers="tableHeader"
                                      :items="dsAlbum"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsAlbum"
                                      :server-items-length="searchParamsAlbum.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsAlbum" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.TenAlbum }}</td>
                                        <td class="text-center">{{ item.SoBaiHat }}</td>
                                        <td class="text-center">{{ item.TrangThai == true? "Hiển thị" : "Ẩn" }}</td>
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
                    <v-btn color="red darken-1" @click.native="deleteAlbum" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-album ref="themSuaAlbum" @save='getDataFromApi(searchParamsAlbum)'></them-sua-album>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import AlbumApi, { AlbumApiSearchParams } from '@/apiResources/AlbumApi';
    import { Album } from '@/models/Album';
    import ThemSuaAlbum from './ThemSuaAlbum.vue';

    export default Vue.extend({
        components: {
            ThemSuaAlbum
        },
        data() {
            return {
                dsAlbum: [] as Album[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Tên album', value: 'TenAlbum', align: 'center', sortable: true },
                    { text: 'Số bài hát', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Trạng thái', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsAlbum: { includeEntities: true, itemsPerPage: 10 } as AlbumApiSearchParams,
                loadingTable: false,
                selectedAlbum: {} as Album,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsAlbum: AlbumApiSearchParams): void {
                this.loadingTable = true;
                AlbumApi.search(searchParamsAlbum).then(res => {
                    this.dsAlbum = res.Data;
                    this.searchParamsAlbum.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaAlbum as any).show(isUpdate, item);
            },
            confirmDelete(chuyenMuc: Album): void {
                this.selectedAlbum = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteAlbum(): void {
                AlbumApi.delete(this.selectedAlbum.AlbumID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsAlbum);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

