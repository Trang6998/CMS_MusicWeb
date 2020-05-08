<template>
    <v-dialog v-model="isShow" width="1000" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập ca sỹ' : 'Thêm mới ca sỹ' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" md="4" class="pt-5" v-if="caSy.AnhDaiDien" style="max-height: 350px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on" style="max-height: 350px; width: 100%"
                                               :src="APIS.HOST + 'fileupload/download?key=' + caSy.AnhDaiDien"
                                               @click="$refs.inpFile.click()"
                                               aspect-ratio="0.75"
                                               contain></v-img>
                                    </template>
                                    <span>Click to change photo</span>
                                </v-tooltip>
                                <input type="file" accept=".jpg, .png, .jpeg" style="display:none;" ref="inpFile" id="File" @change="changePhoto">
                            </v-col>
                            <v-col cols="12" md="4" class="pt-5" v-else style="max-height: 350px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on"
                                               style="background-color: darkgray; max-height: 350px; width: 100%"
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
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-text-field v-model="caSy.HoTen"
                                                      label="Họ tên"
                                                      type="text"
                                                      :error-messages="errors.collect('Họ tên', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Họ tên"></v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-text-field v-model="caSy.BietDanh"
                                                      label="Biệt danh"
                                                      type="text"
                                                      :error-messages="errors.collect('Biệt danh', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Biệt danh"></v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-datepicker v-model="caSy.NgaySinh"
                                                      label="Ngày sinh"
                                                      type="text"
                                                      :error-messages="errors.collect('Ngày sinh', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Ngày sinh"></v-datepicker>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-text-field v-model="caSy.SoDienThoai"
                                                      label="Số điện thoại"
                                                      type="text"
                                                      :error-messages="errors.collect('Số điện thoại', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Số điện thoại"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="caSy.Gmail"
                                                      label="Tài khoản gmail"
                                                      type="text"
                                                      :error-messages="errors.collect('Gmail', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Gmail"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="caSy.FaceBook"
                                                      label="Tài khoản facebook"
                                                      type="text"
                                                      :error-messages="errors.collect('Facebook', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Facebook"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="caSy.Instagram"
                                                      label="Tài khoản instagram"
                                                      type="text"
                                                      :error-messages="errors.collect('Instagram', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Instagram"></v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-text-field v-model="caSy.DiaChi"
                                                      label="Địa chỉ"
                                                      type="text"
                                                      :error-messages="errors.collect('Địa chỉ', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Địa chỉ"></v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-autocomplete :items="dsTinhThanh"
                                                        v-model="caSy.TinhThanhID"
                                                        label="Tỉnh thành"
                                                        item-text="TenTinhThanh"
                                                        item-value="Id"
                                                        @input="changeTinhThanh(caSy.TinhThanhID)"
                                                        :error-messages="errors.collect('Tỉnh thành', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Tỉnh thành">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-autocomplete :items="dsQuanHuyen"
                                                        v-model="caSy.QuanHuyenID"
                                                        label="Quận huyện"
                                                        item-text="TenQuanHuyen"
                                                        item-value="Id"
                                                        @input="changeQuanHuyen(caSy.QuanHuyenID)"
                                                        :error-messages="errors.collect('Quận huyện', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Quận huyện">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-autocomplete :items="dsXaPhuong"
                                                        v-model="caSy.XaPhuongID"
                                                        label="Xã phường"
                                                        item-text="TenXaPhuong"
                                                        item-value="Id"
                                                        :error-messages="errors.collect('Xã phường', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Xã phường">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="12" class="py-0 px-1">
                                        <v-textarea v-model="caSy.SoThich"
                                                    label="Sở thích"
                                                    type="text" rows="2"
                                                    :error-messages="errors.collect('Sở thích', 'frmAddEdit')"
                                                    v-validate="''"
                                                    data-vv-scope="frmAddEdit"
                                                    data-vv-name="Sở thích"></v-textarea>
                                    </v-col>
                                </v-layout>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols="12" class="px-2">
                                <v-textarea v-model="caSy.TinhCach"
                                            label="Tính cách"
                                            type="text" rows="2"
                                            :error-messages="errors.collect('Tính cách', 'frmAddEdit')"
                                            v-validate="''" hide-details
                                            data-vv-scope="frmAddEdit"
                                            data-vv-name="Tính cách"></v-textarea>
                            </v-col>
                            <v-col cols="12" class="py-0 px-2">
                                <v-textarea v-model="caSy.TieuSu"
                                            label="Tiểu sử hoạt động cá nhân"
                                            type="text" rows="3" hide-details
                                            :error-messages="errors.collect('Tiểu sử', 'frmAddEdit')"
                                            v-validate="''"
                                            data-vv-scope="frmAddEdit"
                                            data-vv-name="Tiểu sử"></v-textarea>
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
    import CaSyApi, { CaSyApiSearchParams } from '@/apiResources/CaSyApi';
    import { CaSy } from '@/models/CaSy';
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
                caSy: {} as CaSy,
                loading: false,
                loadingSave: false,
                searchParamsCaSy: {} as CaSyApiSearchParams,
                active: [] as any[],
                dsTinhThanh: [] as TinhThanh[],
                dsQuanHuyen: [] as QuanHuyen[],
                dsXaPhuong: [] as XaPhuong[],
                searchParamsTinhThanh: { itemsPerPage: 0 } as TinhThanhApiSearchParams,
                searchParamsQuanHuyen: { itemsPerPage: 0 } as QuanHuyenApiSearchParams,
                searchParamsXaPhuong: { itemsPerPage: 0 } as XaPhuongApiSearchParams,
                caSyID: 0,
                APIS: APIS
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
                    this.caSyID = item.CaSyID;
                    this.getDataFromApi(this.caSyID);
                }
                else {
                    this.caSy = {} as CaSy;
                }
            },
            getDataFromApi(id: number): void {
                CaSyApi.get(id).then(res => {
                    this.caSy = res;
                    if (res.TinhThanhID != null)
                        this.getQuanHuyen(res.TinhThanhID)
                    if (res.QuanHuyenID != null)
                        this.getXaPhuong(res.QuanHuyenID)
                });
            },
            changeTinhThanh(id: number) {
                if (id !== undefined) {
                    this.caSy.QuanHuyenID = null as any;
                    this.caSy.XaPhuongID = null as any;
                    this.caSy.TinhThanhID = id;
                    this.getQuanHuyen(id);
                }
            },
            changeQuanHuyen(id: number) {
                if (id !== undefined) {
                    this.caSy.QuanHuyenID = id
                    this.caSy.XaPhuongID = null as any
                    this.getXaPhuong(id)
                }
            },
            changePhoto(): void {
                var formData = new FormData()
                let files = (document as any).querySelector('#File').files[0]
                var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
                formData.append('img', files);
                FileUploadApi.upload(formData).then(res => {
                    this.caSy.AnhDaiDien = res;
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
                            CaSyApi.update(this.caSyID, this.caSy).then(res => {
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
                            CaSyApi.insert(this.caSy).then(res => {
                                this.caSy = res;
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
    .v-input--selection-controls{
        margin-top: 0px !important
    }
    .v-dialog > .v-card > .v-card__subtitle, .v-dialog > .v-card > .v-card__text {
        padding: 5px 15px;
    }
    .v-dialog > .v-card > .v-card__title {
        padding: 16px 15px 10px
    }
</style>