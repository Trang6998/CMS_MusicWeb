<template>
    <v-dialog v-model="isShow" width="600" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập loại tài khoản' : 'Thêm mới loại tài khoản' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form v-model="valid" scope="frmAddEdit">
                    <v-container class="py-0">
                        <v-row>
                            <v-col cols="12" class="py-0 px-1">
                                <v-text-field v-model="theLoai.TenLoai"
                                                label="Tên loại"
                                                type="text"
                                                :error-messages="errors.collect('Tên loại', 'frmAddEdit')"
                                                v-validate="'required'"
                                                data-vv-scope="frmAddEdit"
                                                data-vv-name="Tên loại"></v-text-field>
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
    import LoaiNhanVienApi, { LoaiNhanVienApiSearchParams } from '@/apiResources/LoaiNhanVienApi';
    import { LoaiNhanVien } from '@/models/LoaiNhanVien';
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
                theLoai: {} as LoaiNhanVien,
                loading: false,
                loadingSave: false,
                searchParamsLoaiNhanVien: {} as LoaiNhanVienApiSearchParams,
                active: [] as any[],
                theLoaiID: 0,
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
                    this.theLoaiID = item.LoaiNhanVienID;
                    this.getDataFromApi(this.theLoaiID);
                }
                else {
                    this.theLoai = {} as LoaiNhanVien;
                }
            },
            getDataFromApi(id: number): void {
                LoaiNhanVienApi.get(id).then(res => {
                    this.theLoai = res;
                });
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        if (this.isUpdate) {
                            this.loading = true;
                            LoaiNhanVienApi.update(this.theLoaiID, this.theLoai).then(res => {
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
                            LoaiNhanVienApi.insert(this.theLoai).then(res => {
                                this.theLoai = res;
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