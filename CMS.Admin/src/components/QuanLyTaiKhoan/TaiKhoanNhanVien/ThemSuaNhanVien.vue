<template>
    <v-dialog v-model="isShow" width="900" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập tài khoản nhân viên' : 'Thêm mới tài khoản nhân viên' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" md="4" class="pt-5" v-if="nhanVien.AnhDaiDien" style="max-height: 350px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on" style="max-height: 350px; width: 100%"
                                               :src="APIS.HOST + 'fileupload/download?key=' + nhanVien.AnhDaiDien"
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
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="nhanVien.HoTen"
                                                      label="Họ tên"
                                                      type="text"
                                                      :error-messages="errors.collect('Họ tên', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Họ tên"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="nhanVien.SoDienThoai"
                                                      label="Số điện thoại"
                                                      type="text"
                                                      :error-messages="errors.collect('Số điện thoại', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Số điện thoại"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-autocomplete :items="dsLoaiTaiKhoan"
                                                        v-model="nhanVien.LoaiNhanVienID"
                                                        label="Loại tài khoản"
                                                        item-text="TenLoai"
                                                        item-value="LoaiNhanVienID"
                                                        :error-messages="errors.collect('Loại tài khoản', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Loại tài khoản">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="nhanVien.Zalo"
                                                      label="Tài khoản zalo"
                                                      type="text"
                                                      :error-messages="errors.collect('Zalo', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Zalo"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="nhanVien.Facebook"
                                                      label="Tài khoản facebook"
                                                      type="text"
                                                      :error-messages="errors.collect('Facebook', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Facebook"></v-text-field>
                                    </v-col>
                                    <v-col cols="4" class="py-0 px-1">
                                        <v-text-field v-model="nhanVien.Instagram"
                                                      label="Tài khoản instagram"
                                                      type="text"
                                                      :error-messages="errors.collect('Instagram', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Instagram"></v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-text-field v-model="nhanVien.DiaChi"
                                                      label="Địa chỉ"
                                                      type="text"
                                                      :error-messages="errors.collect('Địa chỉ', 'frmAddEdit')"
                                                      v-validate="''"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Địa chỉ"></v-text-field>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-autocomplete :items="dsTinhThanh"
                                                        v-model="nhanVien.TinhThanhID"
                                                        label="Tỉnh thành"
                                                        item-text="TenTinhThanh"
                                                        item-value="Id"
                                                        @input="changeTinhThanh(nhanVien.TinhThanhID)"
                                                        :error-messages="errors.collect('Tỉnh thành', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Tỉnh thành">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-autocomplete :items="dsQuanHuyen"
                                                        v-model="nhanVien.QuanHuyenID"
                                                        label="Quận huyện"
                                                        item-text="TenQuanHuyen"
                                                        item-value="Id"
                                                        @input="changeQuanHuyen(nhanVien.QuanHuyenID)"
                                                        :error-messages="errors.collect('Quận huyện', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Quận huyện">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="3" class="py-0 px-1">
                                        <v-autocomplete :items="dsXaPhuong"
                                                        v-model="nhanVien.XaPhuongID"
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
                                        <v-textarea v-model="nhanVien.GioiThieu"
                                                    label="Giới thiệu"
                                                    type="text" rows="3"
                                                    :error-messages="errors.collect('Giới thiệu', 'frmAddEdit')"
                                                    v-validate="''"
                                                    data-vv-scope="frmAddEdit"
                                                    data-vv-name="Giới thiệu"></v-textarea>
                                    </v-col>
                                </v-layout>
                            </v-col>
                        </v-row>
                        <v-row>
                           
                            <v-col cols="4" class="py-0 px-2">
                                <v-text-field v-model="users.UserName"
                                              label="Tài khoản"
                                              type="text"
                                              :error-messages="errors.collect('Tài khoản', 'frmAddEdit')"
                                              v-validate="'required'"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Tài khoản"></v-text-field>
                            </v-col>
                            <v-col cols="4" class="py-0 px-2">
                                <v-text-field v-model="users.Password"
                                              label="Mật khẩu"
                                              type="password"
                                              :error-messages="errors.collect('Mật khẩu', 'frmAddEdit')"
                                              v-validate="'required'"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Mật khẩu"
                                              clearable></v-text-field>
                            </v-col>
                            <v-col cols="4" class="px-2" style="padding-top: 28px">
                                <v-checkbox v-model="users.Active" label="Hoạt động"
                                            :error-messages="errors.collect('Hoạt động', 'frmAddEdit')"
                                            v-validate="''"
                                            data-vv-scope="frmAddEdit"
                                            data-vv-name="Hoạt động"></v-checkbox>
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
    import NhanVienApi, { NhanVienApiSearchParams } from '@/apiResources/NhanVienApi';
    import { NhanVien } from '@/models/NhanVien';
    import { User } from '@/models/User';
    import FileUploadApi from '@/apiResources/FileUploadApi';
    import TinhThanhApi, { TinhThanhApiSearchParams } from '@/apiResources/TinhThanhApi';
    import QuanHuyenApi, { QuanHuyenApiSearchParams } from '@/apiResources/QuanHuyenApi';
    import XaPhuongApi, { XaPhuongApiSearchParams } from '@/apiResources/XaPhuongApi';
    import { TinhThanh } from '@/models/TinhThanh'
    import { QuanHuyen } from '@/models/QuanHuyen'
    import { XaPhuong } from '@/models/XaPhuong'
    import APIS from '@/apisServer';
import UsersApi from '../../../apiResources/UsersApi';
import { LoaiNhanVien } from '../../../models/LoaiNhanVien';
import LoaiNhanVienApi, { LoaiNhanVienApiSearchParams } from '../../../apiResources/LoaiNhanVienApi';


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
                nhanVien: {} as NhanVien,
                users: {} as User,
                loading: false,
                loadingSave: false,
                active: [] as any[],
                nhanVienID: 0,
                APIS: APIS,
                dsLoaiTaiKhoan: [] as LoaiNhanVien[],
                dsTinhThanh: [] as TinhThanh[],
                dsQuanHuyen: [] as QuanHuyen[],
                dsXaPhuong: [] as XaPhuong[],
                searchParamsTinhThanh: { itemsPerPage: 0 } as TinhThanhApiSearchParams,
                searchParamsQuanHuyen: { itemsPerPage: 0 } as QuanHuyenApiSearchParams,
                searchParamsXaPhuong: { itemsPerPage: 0 } as XaPhuongApiSearchParams,
                searchParamsLoaiNhanVien: { itemsPerPage: 0 } as LoaiNhanVienApiSearchParams,
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
                this.getTinhThanh()
                this.getLoaiTaiKhoan()
                if (isUpdate) {
                    this.nhanVienID = item.UserId;
                    this.getDataFromApi(this.nhanVienID);
                }
                else {
                    this.nhanVien = {} as NhanVien;
                    this.users = {} as User;
                }
            },
            getDataFromApi(id: number): void {
                UsersApi.getAsync(id).then(res => {
                    this.users = res;
                    this.nhanVien = res.NhanVien;
                    if (res.NhanVien.TinhThanhID != null)
                        this.getQuanHuyen(res.NhanVien.TinhThanhID)
                    if (res.NhanVien.QuanHuyenID != null)
                        this.getXaPhuong(res.NhanVien.QuanHuyenID)
                });
            },
            getLoaiTaiKhoan(): void {
                LoaiNhanVienApi.search(this.searchParamsLoaiNhanVien).then(res => {
                    this.dsLoaiTaiKhoan = res.Data;
                });
            },
            changeTinhThanh(id: number) {
                if (id !== undefined) {
                    this.nhanVien.QuanHuyenID = null as any;
                    this.nhanVien.XaPhuongID = null as any;
                    this.nhanVien.TinhThanhID = id;
                    this.getQuanHuyen(id);
                }
            },
            changeQuanHuyen(id: number) {
                if (id !== undefined) {
                    this.nhanVien.QuanHuyenID = id
                    this.nhanVien.XaPhuongID = null as any
                    this.getXaPhuong(id)
                }
            },
            changePhoto(): void {
                var formData = new FormData()
                let files = (document as any).querySelector('#File').files[0]
                var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
                formData.append('img', files);
                FileUploadApi.upload(formData).then(res => {
                    this.nhanVien.AnhDaiDien = res;
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
                        this.users.NhanVien = this.nhanVien
                        if (this.isUpdate) {
                            this.loading = true;
                            UsersApi.updateAsync(this.nhanVienID, this.users).then(res => {
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
                            UsersApi.insertAsync(this.users).then(res => {
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