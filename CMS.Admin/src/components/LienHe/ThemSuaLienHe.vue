<template>
    <v-dialog v-model="isShow" width="600" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập liên hệ' : 'Thêm mới liên hệ' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="6" class="py-0 px-1">
                                <v-text-field v-model="lienHe.HoTen"
                                              label="Họ tên"
                                              type="text"
                                              :error-messages="errors.collect('Họ tên', 'frmAddEdit')"
                                              v-validate="'required'"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Họ tên"></v-text-field>
                            </v-col>
                            <v-col cols="6" class="py-0 px-1">
                                <v-text-field v-model="lienHe.SoDienThoai"
                                              label="Số điện thoại"
                                              type="text"
                                              :error-messages="errors.collect('Số điện thoại', 'frmAddEdit')"
                                              v-validate="'required'"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Số điện thoại"></v-text-field>
                            </v-col>
                            <v-col cols="6" class="py-0 px-1">
                                <v-text-field v-model="lienHe.Email"
                                              label="Email"
                                              type="text"
                                              :error-messages="errors.collect('Email', 'frmAddEdit')"
                                              v-validate="''"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Email"></v-text-field>
                            </v-col>
                            <v-col cols="6" class="pt-7 pb-0 px-1">
                                <v-checkbox v-model="lienHe.HienThi"
                                              label="Hiển thị"
                                              :error-messages="errors.collect('Hiển thị', 'frmAddEdit')"
                                              v-validate="''"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Hiển thị"></v-checkbox>
                            </v-col>
                            <v-col cols="12" class="py-0 px-1">
                                <v-textarea v-model="lienHe.NoiDung"
                                              label="Nội dung"
                                              type="text" rows="5"
                                              :error-messages="errors.collect('Nội dung', 'frmAddEdit')"
                                              v-validate="'required'"
                                              data-vv-scope="frmAddEdit"
                                              data-vv-name="Nội dung"></v-textarea>
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
    import LienHeApi, { LienHeApiSearchParams } from '@/apiResources/LienHeApi';
    import { LienHe } from '@/models/LienHe';
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
                lienHe: {} as LienHe,
                loading: false,
                loadingSave: false,
                searchParamsLienHe: {} as LienHeApiSearchParams,
                active: [] as any[],
                lienHeID: 0,
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
                if (isUpdate) {
                    this.lienHeID = item.LienHeID;
                    this.getDataFromApi(this.lienHeID);
                }
                else {
                    this.lienHe = {} as LienHe;
                }
            },
            getDataFromApi(id: number): void {
                LienHeApi.get(id).then(res => {
                    this.lienHe = res;
                });
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        if (this.isUpdate) {
                            this.loading = true;
                            LienHeApi.update(this.lienHeID, this.lienHe).then(res => {
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
                            LienHeApi.insert(this.lienHe).then(res => {
                                this.lienHe = res;
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