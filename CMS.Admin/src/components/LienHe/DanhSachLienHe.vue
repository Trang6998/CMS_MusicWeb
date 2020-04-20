<template>
    <v-col cols="12" class="pa-0">
        <v-card>
            <v-card-text>
                <h2>Danh sách liên hệ</h2>
                <v-row>
                    <v-col cols="6">
                        <v-text-field v-model="searchParamsLienHe.keyworlds"
                                      @input="getDataFromApi(searchParamsLienHe)"
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
                                      :items="dsLienHe"
                                      @update:options="getDataFromApi"
                                      :options.sync="searchParamsLienHe"
                                      :server-items-length="searchParamsLienHe.totalItems"
                                      :loading="loadingTable"
                                      class="elevation-1">
                            <v-progress-linear height="2" slot="progress" color="blue" indeterminate></v-progress-linear>
                            <template v-slot:body="{ item }">
                                <tbody>
                                    <tr v-for="(item, index) in dsLienHe" :key="index">
                                        <td class="text-center">{{ index + 1 }}</td>
                                        <td class="text-center">{{ item.HoTen }}</td>
                                        <td class="text-center">{{ item.SoDienThoai }}</td>
                                        <td class="text-center">{{ item.NoiDung }}</td>
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
                    <v-btn color="red darken-1" @click.native="deleteLienHe" text>Xác Nhận</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <them-sua-lien-he ref="themSuaLienHe" @save='getDataFromApi(searchParamsLienHe)'></them-sua-lien-he>
    </v-col>
</template>
<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import LienHeApi, { LienHeApiSearchParams } from '@/apiResources/LienHeApi';
    import { LienHe } from '@/models/LienHe';
    import ThemSuaLienHe from './ThemSuaLienHe.vue';

    export default Vue.extend({
        components: {
            ThemSuaLienHe
        },
        data() {
            return {
                dsLienHe: [] as LienHe[],
                tableHeader: [
                    { text: 'STT', value: '#', align: 'center', sortable: true },
                    { text: 'Họ tên', value: 'TenLienHe', align: 'center', sortable: true },
                    { text: 'Số điện thoại', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Nội dung', value: 'GioiThieu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: '#', align: 'center', sortable: false },
                ],
                searchParamsLienHe: { includeEntities: true, itemsPerPage: 10 } as LienHeApiSearchParams,
                loadingTable: false,
                selectedLienHe: {} as LienHe,
                dialogConfirmDelete: false,
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            getDataFromApi(searchParamsLienHe: LienHeApiSearchParams): void {
                this.loadingTable = true;
                LienHeApi.search(searchParamsLienHe).then(res => {
                    this.dsLienHe = res.Data;
                    this.searchParamsLienHe.totalItems = res.Pagination.totalItems;
                    this.loadingTable = false;
                });
            },
            showModalThemSua(isUpdate: boolean, item: any) {
                (this.$refs.themSuaLienHe as any).show(isUpdate, item);
            },
            confirmDelete(chuyenMuc: LienHe): void {
                this.selectedLienHe = chuyenMuc;
                this.dialogConfirmDelete = true;
            },
            deleteLienHe(): void {
                LienHeApi.delete(this.selectedLienHe.LienHeID).then(res => {
                    this.$snotify.success('Xóa thành công!');
                    this.getDataFromApi(this.searchParamsLienHe);
                    this.dialogConfirmDelete = false;
                }).catch(res => {
                    this.$snotify.error('Xóa thất bại!');
                });
            },
        }
    });
</script>

