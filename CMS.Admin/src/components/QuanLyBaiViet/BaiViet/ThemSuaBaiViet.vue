<template>
    <v-dialog v-model="dialog" max-width="800" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập bài viết' : 'Thêm mới bài viết' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-row>
                    <v-form v-model="valid" scope="frmAddEdit">
                        <v-container class="py-0">
                            <v-row>
                                <v-col cols="12" md="4" class="pt-5" v-if="baiViet.AnhDaiDien" style="max-height: 300px">
                                    <v-tooltip bottom>
                                        <template v-slot:activator="{ on }">
                                            <v-img v-on="on" style="max-height: 300px"
                                                   :src="APIS.HOST + 'fileupload/download?key=' + baiViet.AnhDaiDien"
                                                   @click="$refs.inpFile.click()"
                                                   aspect-ratio="0.75"
                                                   contain></v-img>
                                        </template>
                                        <span>Click to change photo</span>
                                    </v-tooltip>
                                    <input type="file" accept=".jpg, .png, .jpeg" style="display:none;" ref="inpFile" id="File" @change="changePhoto">
                                </v-col>
                                <v-col cols="12" md="4" class="pt-5" v-else style="max-height: 300px">
                                    <v-tooltip bottom>
                                        <template v-slot:activator="{ on }">
                                            <v-img v-on="on" 
                                                   style="background-color: darkgray; height: 300px"
                                                   @click="$refs.inpFile.click()"
                                                   aspect-ratio="0.75"
                                                   contain></v-img>
                                        </template>
                                        <span>Click to change photo</span>
                                    </v-tooltip>
                                    <input type="file" accept=".jpg, .png, .jpeg" style="display:none;" ref="inpFile" id="File" @change="changePhoto">
                                </v-col>
                                <v-col cols="12" md="8">
                                    <v-layout row wrap>
                                        <v-col cols="12" class="py-0">
                                            <v-text-field v-model="baiViet.TieuDe"
                                                          label="Tiêu đề bài viết"
                                                          type="text"
                                                          :error-messages="errors.collect('Tiêu đề bài viết', 'frmAddEdit')"
                                                          v-validate="'required'"
                                                          data-vv-scope="frmAddEdit"
                                                          data-vv-name="Tiêu đề bài viết"></v-text-field>
                                        </v-col>
                                        <v-col cols="12" class="py-0">
                                            <v-autocomplete v-model="dsChuyenMucDaChon"
                                                            :items="dsChuyenMuc" chips
                                                            label="Chuyên mục bài viết"
                                                            item-text="TenChuyenMuc"
                                                            item-value="ChuyenMucID"
                                                            :error-messages="errors.collect('Chuyên mục bài viết', 'frmAddEdit')"
                                                            v-validate="'required'"
                                                            data-vv-scope="frmAddEdit"
                                                            data-vv-name="Chuyên mục bài viết"
                                                            multiple>
                                                <template v-slot:selection="data">
                                                    <v-chip v-bind="data.attrs"
                                                            :input-value="data.selected"
                                                            close
                                                            @click="data.select"
                                                            @click:close="remove(data.item)">
                                                        {{ data.item.TenChuyenMuc }}
                                                    </v-chip>
                                                </template>

                                            </v-autocomplete>
                                        </v-col>
                                        <v-col cols="12" class="py-0">
                                            <v-textarea v-model="baiViet.MoTaNgan"
                                                        label="Mô tả ngắn"
                                                        type="text"  rows="3"
                                                        :error-messages="errors.collect('Mô tả ngắn', 'frmAddEdit')"
                                                        v-validate="''"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Mô tả ngắn"></v-textarea>
                                        </v-col>
                                        <v-col cols="6" class="py-0">
                                            <v-checkbox v-model="baiViet.TrangThai"
                                                        label="Hiển thị"
                                                        type="text"  hide-details
                                                        :error-messages="errors.collect('Hiển thị', 'frmAddEdit')"
                                                        v-validate="''"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Hiển thị"></v-checkbox>
                                        </v-col>
                                        <v-col cols="6" class="py-0">
                                            <v-checkbox v-model="baiViet.HienThiBaiViet"
                                                        label="Là bài viết nổi bật"
                                                        type="text"   hide-details
                                                        :error-messages="errors.collect('Là bài viết nổi bật', 'frmAddEdit')"
                                                        v-validate="''"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Là bài viết nổi bật"></v-checkbox>
                                        </v-col>
                                    </v-layout>

                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col cols="12" class="pt-5">
                                    <h3 class="pb-2">Nội dung</h3>
                                    <v-ckeditor v-model="baiViet.NoiDung" :config="editorConfig"></v-ckeditor>
                                </v-col>
                            </v-row>
                        </v-container>
                    </v-form>
                </v-row>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions v-show="!readOnly">
                <v-spacer></v-spacer>
                <v-btn v-if="isUpdate" class="primary" small :disabled="loading" :loading="loading" @click.native="save">
                    <v-icon small class="mr-1">save</v-icon> Cập nhật
                </v-btn>
                <v-btn v-else class="primary" small :disabled="loading" :loading="loading" @click.native="save">
                    <v-icon small class="mr-1">add</v-icon> Thêm mới
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script lang="ts">
    import Vue from 'vue';
    import BaiVietApi from '@/apiResources/BaiVietApi';
    import APIS from '@/apisServer';
    import FileUploadApi from '@/apiResources/FileUploadApi';
    import ChuyenMucApi, { ChuyenMucApiSearchParams } from '@/apiResources/ChuyenMucApi';
    import { BaiViet } from '@/models/BaiViet';
    import { ChuyenMuc } from '@/models/ChuyenMuc';
    import { ChuyenMuc_BaiViet } from '@/models/ChuyenMuc_BaiViet';
    export default Vue.extend({
        components: {},
        props: { readOnly: Boolean },
        data() {
            return {
                valid: false,
                loading: false,
                dialog: false,
                isUpdate: false,
                baiViet: {
                    ChuyenMuc_BaiViet: [] as ChuyenMuc_BaiViet[]
                } as BaiViet,
                id: 0 as number,
                dsChuyenMuc: [] as ChuyenMuc[],
                dsChuyenMucDaChon: [] as number[],
                chuyenMucSearchParams: { itemsPerPage: 0 } as ChuyenMucApiSearchParams,
                APIS: APIS,
                editorConfig: {
                    height: 300,
                    language: 'vi',
                    toolbarGroups: [
                        { name: 'document', groups: ['mode', 'document', 'doctools'] },
                        { name: 'clipboard', groups: ['clipboard', 'undo'] },
                        { name: 'forms' },
                        { name: 'basicstyles', groups: ['basicstyles'] },
                        { name: 'colors' },
                        { name: 'tools' },
                        { name: 'others' },
                        { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'] },
                        { name: 'links' },
                        { name: 'insert' },
                        { name: 'styles' },
                        { name: 'about' },

                    ]
                }
            }
        },
        methods: {
            hide() {
                this.dialog = false;
            },
            show(id: number) {
                this.$validator.errors.clear();
                this.$validator.reset();
                this.dsChuyenMucDaChon = [] as number[];
                this.dialog = true;
                this.getDanhSachChuyenMuc();
                this.id = id;
                if (id > 0) {
                    this.isUpdate = true;
                    this.baiViet.NoiDung = ' '
                    this.getBaiViet(id);
                } else {
                    this.isUpdate = false;
                    this.baiViet = {
                        ChuyenMuc_BaiViet: [] as ChuyenMuc_BaiViet[]
                    } as BaiViet;
                    this.baiViet.NoiDung = ' '
                }
            },
            getDanhSachChuyenMuc() {
                ChuyenMucApi.search(this.chuyenMucSearchParams).then(res => {
                    this.dsChuyenMuc = res.Data
                })
            },
            remove(item: any) {
                const index = this.dsChuyenMucDaChon.indexOf(item.ChuyenMucID)
                if (index >= 0) this.dsChuyenMucDaChon.splice(index, 1)
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        this.baiViet.ChuyenMuc_BaiViet = [] as any;
                        for (let i = 0; i < this.dsChuyenMucDaChon.length; i++) {
                            var cmt = {} as ChuyenMuc_BaiViet
                            cmt.ChuyenMucID = this.dsChuyenMucDaChon[i];
                            cmt.BaiVietID = this.baiViet.BaiVietID;
                            this.baiViet.ChuyenMuc_BaiViet.push(cmt);
                        };
                        this.loading = true;
                        if (this.isUpdate) {
                            BaiVietApi.update(this.id, this.baiViet).then(res => {
                                //this.baiViet = res;
                                this.isUpdate = true;
                                this.loading = false;
                                this.$snotify.success('Cập nhật bài viết thành công!');
                                this.hide();
                                this.$emit('save');
                            }).catch(res => {
                                this.loading = false;
                                this.$snotify.error('Cập nhật bài viết thất bại!');
                            });
                        }
                        else {
                            BaiVietApi.insert(this.baiViet).then(res => {
                                //this.baiViet = res;
                                this.isUpdate = true;
                                this.loading = false;
                                this.$snotify.success('Thêm mới bài viết thành công!');
                                this.hide();
                                this.$emit('save');
                            }).catch(res => {
                                this.loading = false;
                                this.$snotify.error('Thêm mới bài viết thất bại!');
                            });
                        }
                    }
                });
            },
            getBaiViet(id: number) {
                BaiVietApi.get(id).then(res => {
                    if (res.ChuyenMuc_BaiViet != null) {
                        res.ChuyenMuc_BaiViet.forEach((x: ChuyenMuc_BaiViet) => {
                            this.dsChuyenMucDaChon.push(x.ChuyenMucID);
                        })
                    }
                    this.baiViet = res;
                }).catch(res => {
                    this.$snotify.error('Lấy dữ liệu bài viết thất bại!');
                });
            },

            changePhoto(): void {
                var formData = new FormData()
                let files = (document as any).querySelector('#File').files[0]
                var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
                formData.append('img', files);
                FileUploadApi.upload(formData).then(res => {
                    this.baiViet.AnhDaiDien = res;
                }).catch(res => {
                    this.$snotify.error('Upload ảnh thất bại!');
                })
            },
        },
    })
</script>
<style>
    .v-select.v-select--chips .v-select__selections{
        min-height: 30px
    }
</style>