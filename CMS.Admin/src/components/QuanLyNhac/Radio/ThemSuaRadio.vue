<template>
    <v-dialog v-model="isShow" width="1000" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập radio' : 'Thêm mới radio' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" md="4" class="pt-5" v-if="radio.AnhDaiDien" style="max-height: 250px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on" style="max-height: 250px; width: 100%"
                                               :src="APIS.HOST + 'fileupload/download?key=' + radio.AnhDaiDien"
                                               @click="$refs.inpFile.click()"
                                               aspect-ratio="0.75"
                                               contain></v-img>
                                    </template>
                                    <span>Click to change photo</span>
                                </v-tooltip>
                                <input type="file" accept=".jpg, .png, .jpeg" style="display:none;" ref="inpFile" id="File" @change="changePhoto">
                            </v-col>
                            <v-col cols="12" md="4" class="pt-5" v-else style="max-height: 250px">
                                <v-tooltip bottom>
                                    <template v-slot:activator="{ on }">
                                        <v-img v-on="on"
                                               style="background-color: darkgray; max-height: 250px; width: 100%"
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
                                        <v-text-field v-model="radio.TieuDe"
                                                      label="Tiêu đề"
                                                      type="text"
                                                      :error-messages="errors.collect('Tiêu đề', 'frmAddEdit')"
                                                      v-validate="'required'"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Tiêu đề"></v-text-field>
                                    </v-col>
                                    <v-col cols="6" class="py-0 px-1">
                                        <v-text-field v-model="radio.LuotXem"
                                                      label="Lượt xem"
                                                      type="text"
                                                      :error-messages="errors.collect('Lượt xem', 'frmAddEdit')"
                                                      v-validate="''" :readonly="true"
                                                      data-vv-scope="frmAddEdit"
                                                      data-vv-name="Lượt xem"></v-text-field>
                                    </v-col>
                                    <v-col cols="6" class="py-0 pt-7">
                                        <v-checkbox v-model="radio.TrangThai"
                                                    label="Hiển thị"
                                                    type="text" rows="2"
                                                    :error-messages="errors.collect('Hiển thị', 'frmAddEdit')"
                                                    v-validate="''"
                                                    data-vv-scope="frmAddEdit"
                                                    data-vv-name="Hiển thị"></v-checkbox>
                                    </v-col>
                                </v-layout>
                            </v-col>
                        </v-row>
                        <v-row>
                            <v-col cols="12">
                                <h3 class="py-2">Nội dung</h3>
                                <v-ckeditor v-model="radio.NoiDung" :config="editorConfig"></v-ckeditor>
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
    import RadioApi, { RadioApiSearchParams } from '@/apiResources/RadioApi';
    import { Radio } from '@/models/Radio';
    import FileUploadApi from '@/apiResources/FileUploadApi';
    import APIS from '@/apisServer';
import { HTTP } from '../../../HTTPServices';

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
                radio: {} as Radio,
                loading: false,
                loadingSave: false,
                searchParamsRadio: {} as RadioApiSearchParams,
                active: [] as any[],
                radioID: 0,
                APIS: APIS,
                files: null as any,
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
                this.files = null as any;
                this.isUpdate = isUpdate;
                if (isUpdate) {
                    this.radioID = item.RadioID;
                    this.getDataFromApi(this.radioID);
                }
                else {
                    this.radio = {} as Radio;
                }
            },
            getDataFromApi(id: number): void {
                RadioApi.get(id).then(res => {
                    this.radio = res;
                });
            },
            addFile(link: any): void {
                var formData = new FormData()
                let files = link
                formData.append('file', files);
                FileUploadApi.upload(formData).then(res => {
                    this.radio.LinkFile = res
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
                    this.radio.AnhDaiDien = res;
                }).catch(res => {
                    this.$snotify.error('Upload ảnh thất bại!');
                })
            },
            
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        if (this.isUpdate) {
                            this.loading = true;
                            RadioApi.update(this.radioID, this.radio).then(res => {
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
                            RadioApi.insert(this.radio).then(res => {
                                this.radio = res;
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