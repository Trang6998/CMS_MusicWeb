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
                                <v-layout row wrap>
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
                                </v-layout>
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
                searchParamsAlbum: {} as AlbumApiSearchParams,
                active: [] as any[],
                albumID: 0,
                APIS: APIS,
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
                this.isUpdate = isUpdate;
                if (isUpdate) {
                    this.albumID = item.AlbumID;
                    this.getDataFromApi(this.albumID);
                }
                else {
                    this.album = {} as Album;
                }
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