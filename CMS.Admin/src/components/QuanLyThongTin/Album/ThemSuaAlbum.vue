<template>
    <v-dialog v-model="isShow" width="800" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập album' : 'Thêm mới album' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" md="6" class="pt-5" v-if="album.AnhDaiDien" style="max-height: 300px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on" style="max-height: 300px;"
                                               :src="APIS.HOST + 'fileupload/download?key=' + album.AnhDaiDien"
                                               @click="$refs.inpFile.click()"
                                               aspect-ratio="0.75"
                                               contain></v-img>
                                    </template>
                                    <span>Click to change photo</span>
                                </v-tooltip>
                                <input type="file" accept=".jpg, .png, .jpeg" style="display:none;" ref="inpFile" id="File" @change="changePhoto">
                            </v-col>
                            <v-col cols="12" md="6" class="pt-5" v-else style="max-height: 300px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on"
                                               style="background-color: darkgray; max-height: 300px;"
                                               @click="$refs.inpFile.click()"
                                               aspect-ratio="0.75"
                                               contain></v-img>
                                    </template>
                                    <span>Click to change photo</span>
                                </v-tooltip>
                                <input type="file" accept=".jpg, .png, .jpeg" style="display:none;" ref="inpFile" id="File" @change="changePhoto">
                            </v-col>
                            <v-col cols="12" md="6">
                                <v-row>
                                    <v-col cols="12" class="py-0 px-1">
                                        <v-text-field v-model="album.TenAlbum"
                                                      label="Tên album"
                                                      type="text"
                                                      :error-messages="errors.collect('Tên album', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Tên album"></v-text-field>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-checkbox v-model="album.TrangThai" label="Hiển thị"
                                                    :error-messages="errors.collect('Hiển thị', 'frmAddEdit')"
                                                    v-validate="''"
                                                    data-vv-scope="frmAddEdit"
                                                    data-vv-name="Hiển thị"></v-checkbox>
                                    </v-col>
                                </v-row>
                            </v-col>
                        </v-row>
                        <v-row>

                            <v-col cols="12" class="pt-0" v-show="hien">
                                <v-row>
                                    <v-col cols="12"><h3>Danh sách bài hát thuộc album </h3></v-col>
                                </v-row>
                                <v-row>
                                    <v-col cols="6"  class="py-0">
                                        <v-autocomplete v-model="albumBaiHat.BaiHatID"
                                                        :items="dsBaiHat"
                                                        item-text="TenBaiHat"
                                                        item-value="BaiHatID"
                                                        label="Chọn bài"
                                                        :error-messages="errors.collect('Chọn bài', 'formAlbumBaiHat')"
                                                        v-validate="'required'"
                                                        data-vv-scope="formAlbumBaiHat"
                                                        data-vv-name="Chọn bài"
                                                        required></v-autocomplete>
                                    </v-col>
                                    <v-col cols="3" class="px-1 py-0">
                                        <v-text-field v-model.number="albumBaiHat.SoThuTu"
                                                      label="Số thứ tự hiển thị"
                                                      type="number"
                                                      :error-messages="errors.collect('Số thứ tự hiển thị', 'formAlbumBaiHat')"
                                                      v-validate="'required|numeric'"
                                                      data-vv-scope="formAlbumBaiHat"
                                                      data-vv-name="Số thứ tự hiển thị">
                                        </v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="pt-5">
                                        <v-btn color="orange lighten-2" style="float: right" fab dark small @click="reload()">
                                            <v-icon small class="px-0">autorenew</v-icon>
                                        </v-btn>
                                        <v-btn color="orange lighten-2" style="float: right; margin-right: 5px" fab dark small @click="saveAlbumBaiHat()">
                                            <v-icon small class="px-0">{{isUpdateChiTiet == false ? 'add' :'save'}}</v-icon>
                                        </v-btn>
                                    </v-col>

                                </v-row>
                                <v-row>
                                    <v-col cols="12" xs="12" md="12" class="pt-2">
                                        <v-data-table v-show="hien" :loading="loading"
                                                      :items="dsAlbumBaiHat"
                                                      :headers="tableHeader"
                                                      :items-per-page="10"
                                                      class="elevation-1">
                                            <template v-slot:body="{ item }">
                                                <tbody>
                                                    <tr v-for="item in dsAlbumBaiHat" :key="item.id" @click="selectedRow(item)" style="cursor: pointer">
                                                        <td class="text-center">
                                                            {{item.BaiHat.TenBaiHat}}
                                                        </td>
                                                        <td class="text-center">
                                                            {{item.SoThuTu}}
                                                        </td>
                                                        <td class="text-center">
                                                            <v-btn icon small class="mx-0" @click="confirmDelete(props.item)">
                                                                <v-icon small color="red">delete</v-icon>
                                                            </v-btn>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </template>
                                        </v-data-table>
                                    </v-col>

                                </v-row>
                            </v-col>
                        </v-row>
                    </v-container>
                </v-form>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn text @click.native="hide">Hủy</v-btn>
                <v-btn text @click.native="save" color="primary"
                       :loading="loading" :disabled="loading">Lưu</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import AlbumApi, { AlbumApiSearchParams } from '@/apiResources/AlbumApi';
    import { Album } from '@/models/Album';
    import FileUploadApi from '@/apiResources/FileUploadApi';
    import APIS from '@/apisServer';
    import { Album_BaiHat } from '../../../models/Album_BaiHat';
    import Album_BaiHatApi, { Album_BaiHatApiSearchParams } from '../../../apiResources/Album_BaiHatApi';
import { BaiHat } from '../../../models/BaiHat';
import BaiHatApi, { BaiHatApiSearchParams } from '../../../apiResources/BaiHatApi';

    export default Vue.extend({
        $_veeValidate: {
            validator: 'new'
        },
        components: {},
        data() {
            return {
                valid: false,
                isShow: false,
                isUpdate: false,
                album: {} as Album,
                loading: false,
                loadingSave: false,
                searchParamsBaiHat: { includeEntities: true, itemsPerPage: 0 } as BaiHatApiSearchParams,
                searchParamsAlbum_BaiHat: { includeEntities: true, itemsPerPage: 0 } as Album_BaiHatApiSearchParams,
                active: [] as any[],
                albumID: 0,
                APIS: APIS,

                isUpdateChiTiet: false,
                dialogConfirmDelete: false,
                hien: false,
                albumBaiHat: {} as Album_BaiHat,
                dsAlbumBaiHat: [] as Album_BaiHat[],
                loadingbtn: false,
                dsBaiHat: [] as BaiHat[],
                tableHeader: [
                    { text: 'Tên bài hát', value: 'mon', align: 'center', sortable: true },
                    { text: 'Thứ tự hiển thị', value: 'soThuTu', align: 'center', sortable: true },
                    { text: 'Thao tác', value: 'thaotac', align: 'center', sortable: true },
                ],
            }
        },
        watch: {
        },
        created() {
        },
        methods: {
            hide() {
                this.isShow = false
            },
            show(isUpdate: boolean, item: any) {
                this.$validator.errors.clear();
                this.$validator.reset();
                this.loadingSave = false;
                this.isShow = true;
                this.dsAlbumBaiHat = [] as Album_BaiHat[];
                this.albumBaiHat = {} as Album_BaiHat;
                this.getDSAlbumBaiHat();
                this.getDSBaiHat();
                this.isUpdate = isUpdate;
                if (isUpdate) {
                    this.hien = true;
                    this.albumID = item.AlbumID;
                    this.searchParamsAlbum_BaiHat.albumID = this.albumID
                    this.getDataFromApi(this.albumID);
                }
                else {
                    this.hien = false;
                    this.album = {} as Album;
                }
            },
            getDSBaiHat() {
                BaiHatApi.search(this.searchParamsBaiHat).then(res => {
                    this.dsBaiHat = res.Data
                })
            },
            getDSAlbumBaiHat(): void {
                this.searchParamsAlbum_BaiHat.albumID = this.album.AlbumID;
                Album_BaiHatApi.search(this.searchParamsAlbum_BaiHat).then(res => {
                    this.dsAlbumBaiHat = res.Data;
                }).catch(res => {
                    this.loading = false;
                    this.$snotify.error('Lấy dữ liệu bài hát thất bại!');
                });
            },

            reload() {
                this.isUpdateChiTiet = false;
                this.$validator.reset();
                this.albumBaiHat = {} as Album_BaiHat;
            },
            selectedRow(value: any) {
                this.albumBaiHat = value;
                this.isUpdateChiTiet = true;
            },
            saveAlbumBaiHat(): void {
                this.albumBaiHat.AlbumID = this.album.AlbumID;
                this.albumBaiHat.Album = undefined as any;
                this.$validator.validateAll('formAlbumBaiHat').then((res) => {
                    if (res) {
                        if (this.isUpdateChiTiet) {
                            this.loading = true;
                            Album_BaiHatApi.update(this.albumBaiHat.AlbumID, this.albumBaiHat.BaiHatID, this.albumBaiHat).then(res => {
                                this.loading = false;
                                this.isUpdateChiTiet = false;
                                this.$emit("save");
                                this.albumBaiHat = {} as Album_BaiHat;
                                this.getDSAlbumBaiHat();
                                this.$snotify.success('Cập nhật thành công!');
                            }).catch(res => {
                                this.loading = false;
                                this.$snotify.error('Cập nhật thất bại!');
                            });
                        } else {
                            this.loading = true;
                            Album_BaiHatApi.insert(this.albumBaiHat).then(res => {
                                this.albumBaiHat = res;
                                this.isUpdateChiTiet = false;
                                this.loading = false;
                                this.hien = true;
                                this.$emit("save");
                                this.albumBaiHat = {} as Album_BaiHat;
                                this.getDSAlbumBaiHat();
                                this.$snotify.success('Thêm mới thành công!');
                            }).catch(res => {
                                this.loading = false;
                                this.$snotify.error('Bài hát đã tồn tại trong album!');
                            });
                        }
                    }
                });
            },
            getDataFromApi(id: number): void {
                AlbumApi.get(id).then(res => {
                    this.album = res;
                });
            },
            changePhoto(): void {
                var formData = new FormData()
                let files = (document as any).querySelector('#File').files[0]
                var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
                formData.append('img', files);
                FileUploadApi.upload(formData).then(res => {
                    this.album.AnhDaiDien = res;
                }).catch(res => {
                    this.$snotify.error('Upload ảnh thất bại!');
                })
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        if (this.isUpdate) {
                            this.loading = true;
                            AlbumApi.update(this.albumID, this.album).then(res => {
                                this.loading = false;
                                this.$emit("save");
                                this.isShow = false;
                                this.$snotify.success('Cập nhật thành công!');
                            }).catch(res => {
                                this.loading = false;
                                this.$snotify.error('Cập nhật thất bại!');
                            });
                        } else {
                            this.loading = true;
                            AlbumApi.insert(this.album).then(res => {
                                this.album = res;
                                this.isShow = false;
                                this.$emit("save");
                                this.loading = false;
                                this.$snotify.success('Thêm mới thành công!');
                            }).catch(res => {
                                this.loading = false;
                                this.$snotify.error('Thêm mới thất bại!');
                            });
                        }
                    }
                });
            },
        }
    });
</script>
<style>
    .v-input--selection-controls {
        margin-top: 0px !important
    }

    .v-dialog > .v-card > .v-card__subtitle, .v-dialog > .v-card > .v-card__text {
        padding: 5px 15px;
    }

    .v-dialog > .v-card > .v-card__title {
        padding: 16px 15px 10px
    }
</style>