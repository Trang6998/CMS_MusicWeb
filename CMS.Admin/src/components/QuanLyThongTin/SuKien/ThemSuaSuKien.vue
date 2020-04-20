<template>
    <v-dialog v-model="isShow" width="900" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập sự kiện' : 'Thêm mới sự kiện' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" md="4" class="pt-5" v-if="suKien.AnhDaiDien" style="max-height: 300px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on" style="max-height: 300px;"
                                               :src="APIS.HOST + 'fileupload/download?key=' + suKien.AnhDaiDien"
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
                                               style="background-color: darkgray; max-height: 300px;"
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
                                    <v-col cols="12" class="py-0 px-1">
                                        <v-text-field v-model="suKien.TieuDe"
                                                      label="Tiêu đề"
                                                      type="text"
                                                      :error-messages="errors.collect('Tiêu đề', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Tiêu đề"></v-text-field>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-datepicker v-model="suKien.ThoiGianTu"
                                                      label="Thời gian từ"
                                                      type="text"
                                                      :error-messages="errors.collect('Thời gian từ', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Thời gian từ"></v-datepicker>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-datepicker v-model="suKien.ThoiGianDen"
                                                      label="Thời gian đến"
                                                      type="text"
                                                      :error-messages="errors.collect('Thời gian đến', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Thời gian đến"></v-datepicker>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-text-field v-model="suKien.DiaDiem"
                                                      label="Địa chỉ"
                                                      type="text"
                                                      :error-messages="errors.collect('Địa chỉ', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Địa chỉ"></v-text-field>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-autocomplete :items="dsTinhThanh"
                                                        v-model="suKien.TinhThanhID"
                                                        label="Tỉnh thành"
                                                        item-text="TenTinhThanh"
                                                        item-value="Id"
                                                        @input="changeTinhThanh(suKien.TinhThanhID)"
                                                        :error-messages="errors.collect('Tỉnh thành', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Tỉnh thành">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-autocomplete :items="dsQuanHuyen"
                                                        v-model="suKien.QuanHuyenID"
                                                        label="Quận huyện"
                                                        item-text="TenQuanHuyen"
                                                        item-value="Id"
                                                        @input="changeQuanHuyen(suKien.QuanHuyenID)"
                                                        :error-messages="errors.collect('Quận huyện', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Quận huyện">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-autocomplete :items="dsXaPhuong"
                                                        v-model="suKien.XaPhuongID"
                                                        label="Xã phường"
                                                        item-text="TenXaPhuong"
                                                        item-value="Id"
                                                        :error-messages="errors.collect('Xã phường', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Xã phường">
                                        </v-autocomplete>
                                    </v-col>
                                </v-layout>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols="12" class="pt-1 pr-1 pb-0">
                                <v-textarea v-model="suKien.MoTaNgan"
                                              label="Mô tả ngắn"
                                              type="text" rows="2"
                                              :error-messages="errors.collect('Mô tả ngắn', 'frmAddEdit')"
                                              v-validate="'required'" 
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Mô tả ngắn"></v-textarea>
                            </v-col>
                            <v-col cols="12">
                                <h3 class="pb-2">Nội dung</h3>
                                <v-ckeditor v-model="suKien.NoiDung" :config="editorConfig"></v-ckeditor>
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
    import EventApi, { EventApiSearchParams } from '@/apiResources/EventApi';
    import { Event } from '@/models/Event';
    import FileUploadApi from '@/apiResources/FileUploadApi';
    import TinhThanhApi, { TinhThanhApiSearchParams } from '@/apiResources/TinhThanhApi';
    import QuanHuyenApi, { QuanHuyenApiSearchParams } from '@/apiResources/QuanHuyenApi';
    import XaPhuongApi, { XaPhuongApiSearchParams } from '@/apiResources/XaPhuongApi';
    import { TinhThanh } from '@/models/TinhThanh'
    import { QuanHuyen } from '@/models/QuanHuyen'
    import { XaPhuong } from '@/models/XaPhuong'
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
                suKien: {} as Event,
                loading: false,
                loadingSave: false,
                searchParamsEvent: {} as EventApiSearchParams,
                active: [] as any[],
                dsTinhThanh: [] as TinhThanh[],
                dsQuanHuyen: [] as QuanHuyen[],
                dsXaPhuong: [] as XaPhuong[],
                searchParamsTinhThanh: { itemsPerPage: 0 } as TinhThanhApiSearchParams,
                searchParamsQuanHuyen: { itemsPerPage: 0 } as QuanHuyenApiSearchParams,
                searchParamsXaPhuong: { itemsPerPage: 0 } as XaPhuongApiSearchParams,
                suKienID: 0,
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
                this.getTinhThanh();
                if (isUpdate) {
                    this.suKienID = item.EventID;
                    this.getDataFromApi(this.suKienID);
                }
                else {
                    this.suKien = {} as Event;
                }
            },
            getDataFromApi(id: number): void {
                EventApi.get(id).then(res => {
                    this.suKien = res;
                });
            },
            changeTinhThanh(id: number) {
                if (id !== undefined) {
                    this.suKien.QuanHuyenID = null as any;
                    this.suKien.XaPhuongID = null as any;
                    this.suKien.TinhThanhID = id;
                    this.getQuanHuyen(id);
                }
            },
            changeQuanHuyen(id: number) {
                if (id !== undefined) {
                    this.suKien.QuanHuyenID = id
                    this.suKien.XaPhuongID = null as any
                    this.getXaPhuong(id)
                }
            },
            changePhoto(): void {
                var formData = new FormData()
                let files = (document as any).querySelector('#File').files[0]
                var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
                formData.append('img', files);
                FileUploadApi.upload(formData).then(res => {
                    this.suKien.AnhDaiDien = res;
                }).catch(res => {
                    this.$snotify.error('Upload ảnh thất bại!');
                })
            },
            getTinhThanh() {
                TinhThanhApi.search(this.searchParamsTinhThanh).then(response => {
                    this.dsTinhThanh = response.Data
                })
                    .catch(err => {
                        this.$snotify.error('Lây dữ liệu thất bại!');
                    })
            },
            getQuanHuyen(id: number) {
                this.searchParamsQuanHuyen.tinhThanhID = id;
                QuanHuyenApi.search(this.searchParamsQuanHuyen).then(response => {
                    this.dsQuanHuyen = response.Data
                })
                    .catch(err => {
                        this.$snotify.error('Lây dữ liệu thất bại!');
                    })
            },
            getXaPhuong(id: number) {
                this.searchParamsXaPhuong.quanHuyenID = id;
                XaPhuongApi.search(this.searchParamsXaPhuong).then(response => {
                    this.dsXaPhuong = response.Data
                })
                    .catch(err => {
                        this.$snotify.error('Lây dữ liệu thất bại!');
                    })
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        if (this.isUpdate) {
                            this.loading = true;
                            EventApi.update(this.suKienID, this.suKien).then(res => {
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
                            EventApi.insert(this.suKien).then(res => {
                                this.suKien = res;
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