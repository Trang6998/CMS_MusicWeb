<template>
    <v-dialog v-model="isShow" width="1000" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập bài hát' : 'Thêm mới bài hát' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" md="4" class="pt-5" v-if="baiHat.AnhDaiDien" style="max-height: 300px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on" style="max-height: 300px; width: 100%"
                                               :src="APIS.HOST + 'fileupload/download?key=' + baiHat.AnhDaiDien"
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
                                               style="background-color: darkgray; max-height: 300px; width: 100%"
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
                                        <v-file-input v-model="files"
                                                      counter
                                                      label="Tệp tải lên"
                                                      @change="addFile(files)"
                                                      placeholder="Chọn tệp mới"
                                                      :show-size="1000">
                                        </v-file-input>
                                    </v-col>
                                    <v-col cols="12" class="py-0 px-1">
                                        <v-text-field v-model="baiHat.TenBaiHat"
                                                      label="Tiêu đề"
                                                      type="text"
                                                      :error-messages="errors.collect('Tiêu đề', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Tiêu đề"></v-text-field>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-autocomplete :items="dsTheLoai"
                                                        v-model="baiHat.TheLoaiID"
                                                        label="Thể loại"
                                                        item-text="TenTheLoai"
                                                        item-value="TheLoaiID"
                                                        :error-messages="errors.collect('Thể loại', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Thể loại">
                                        </v-autocomplete>
                                    </v-col>
                                    <v-col cols="6" class="py-0 pt-7">
                                        <v-checkbox v-model="baiHat.TrangThai"
                                                    label="Hiển thị"
                                                    type="text" rows="2"
                                                    :error-messages="errors.collect('Hiển thị', 'frmAddEdit')"
                                                    v-validate="''"
                                                    data-vv-scope="frmAddEdit"
                                                    data-vv-name="Hiển thị"></v-checkbox>
                                    </v-col>
                                    <v-col cols="12" class="py-0 px-1">
                                        <v-autocomplete v-model="dsCaSyDaChon"
                                                        :items="dsCaSy" chips
                                                        label="Ca sỹ thể hiện"
                                                        item-text="BietDanh"
                                                        item-value="CaSyID"
                                                        :error-messages="errors.collect('Ca sỹ thể hiện', 'frmAddEdit')"
                                                        v-validate="'required'"
                                                        data-vv-scope="frmAddEdit"
                                                        data-vv-name="Ca sỹ thể hiện"
                                                        multiple>
                                            <template v-slot:selection="data">
                                                <v-chip v-bind="data.attrs"
                                                        :input-value="data.selected"
                                                        close
                                                        @click="data.select"
                                                        @click:close="remove(data.item)">
                                                    {{ data.item.BietDanh }}
                                                </v-chip>
                                            </template>
                                        </v-autocomplete>
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
    import BaiHatApi, { BaiHatApiSearchParams } from '@/apiResources/BaiHatApi';
    import { BaiHat } from '@/models/BaiHat';
    import FileUploadApi from '@/apiResources/FileUploadApi';
    import APIS from '@/apisServer';
    import { HTTP } from '../../../HTTPServices';
    import CaSyApi, { CaSyApiSearchParams } from '../../../apiResources/CaSyApi';
    import { CaSy } from '../../../models/CaSy';
    import { CaSy_BaiHat } from '../../../models/CaSy_BaiHat';
    import TheLoaiApi, { TheLoaiApiSearchParams} from '../../../apiResources/TheLoaiApi';
import { TheLoai } from '../../../models/TheLoai';
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
                baiHat: { CaSy_BaiHat: [] as CaSy_BaiHat[] } as BaiHat,
                loading: false,
                loadingSave: false,
                searchParamsBaiHat: { includeEntities: true, itemsPerPage: 10 } as BaiHatApiSearchParams,
                searchParamsCaSy: { includeEntities: true, itemsPerPage: 0 } as CaSyApiSearchParams,
                searchParamsTheLoai: { includeEntities: true, itemsPerPage: 0 } as TheLoaiApiSearchParams,
                active: [] as any[],
                baiHatID: 0,
                APIS: APIS,
                files: null as any,
                dsCaSy: [] as CaSy[],
                dsTheLoai: [] as TheLoai[],
                dsCaSyDaChon: [] as number[],
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
                this.files = null as any
                this.loadingSave = false;
                this.getDanhSachCaSy();
                this.getDanhSachTheLoai();
                this.dsCaSyDaChon = [] as number[];
                this.isShow = true;
                this.isUpdate = isUpdate;
                if (isUpdate) {
                    this.baiHatID = item.BaiHatID;
                    this.getDataFromApi(this.baiHatID);
                }
                else {
                    this.baiHat = {
                        CaSy_BaiHat: [] as CaSy_BaiHat[]
                    } as BaiHat;
                }
            },
            getDataFromApi(id: number): void {
                BaiHatApi.get(id).then(res => {
                    if (res.CaSy_BaiHat != null) {
                        res.CaSy_BaiHat.forEach((x: CaSy_BaiHat) => {
                            this.dsCaSyDaChon.push(x.CaSyID);
                        })
                    }
                    this.baiHat = res;
                });
            },
            addFile(link: any): void {
                var formData = new FormData()
                let files = link
                formData.append('file', files);
                FileUploadApi.upload(formData).then(res => {
                    this.baiHat.LinkFileNhac = res
                }).catch(res => {
                    this.$snotify.error('Upload file thất bại!');
                })
            },
            changePhoto(): void {
                var formData = new FormData()
                let files = (document as any).querySelector('#File').files[0]
                var re = /^\w+\.(?:jpg|png|gif|jpeg)$/g
                formData.append('img', files);
                FileUploadApi.upload(formData).then(res => {
                    this.baiHat.AnhDaiDien = res;
                }).catch(res => {
                    this.$snotify.error('Upload ảnh thất bại!');
                })
            },
            getDanhSachCaSy() {
                CaSyApi.search(this.searchParamsCaSy).then(res => {
                    this.dsCaSy = res.Data
                })
            },
            getDanhSachTheLoai() {
                TheLoaiApi.search(this.searchParamsTheLoai).then(res => {
                    this.dsTheLoai = res.Data
                })
            },
            remove(item: any) {
                const index = this.dsCaSyDaChon.indexOf(item.CaSyID)
                if (index >= 0) this.dsCaSyDaChon.splice(index, 1)
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        this.baiHat.CaSy_BaiHat = [] as any;
                        for (let i = 0; i < this.dsCaSyDaChon.length; i++) {
                            var cmt = {} as CaSy_BaiHat
                            cmt.CaSyID = this.dsCaSyDaChon[i];
                            cmt.BaiHatID = this.baiHat.BaiHatID;
                            this.baiHat.CaSy_BaiHat.push(cmt);
                        };
                        if (this.isUpdate) {
                            this.loading = true;
                            BaiHatApi.update(this.baiHatID, this.baiHat).then(res => {
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
                            BaiHatApi.insert(this.baiHat).then(res => {
                                this.baiHat = res;
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
<style scoped>
    .theme--light.v-file-input .v-file-input__text--placeholder{
        padding-left: 5px
    }
</style>