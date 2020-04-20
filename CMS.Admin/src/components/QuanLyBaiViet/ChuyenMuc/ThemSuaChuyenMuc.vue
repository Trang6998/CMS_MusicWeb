<template>
    <v-dialog v-model="isShow" width="600" scrollable persistent>
        <v-card>
            <v-card-title class="primary white--text py-1 px-3">
                <span class="title">{{ isUpdate? 'Cập nhập chuyên mục' : 'Thêm mới chuyên mục' }}</span>
                <v-spacer></v-spacer>
                <v-btn class="white--text ma-0" small icon @click="hide">
                    <v-icon>close</v-icon>
                </v-btn>
            </v-card-title>
            <v-card-text>
                <v-form scope="frmAddEdit">
                    <v-row>
                        <v-col cols="12">
                            <v-text-field v-model="chuyenMuc.TenChuyenMuc"
                                          label="Tên chuyên mục *"
                                          type="text"
                                          :error-messages="errors.collect('Tên chuyên mục', 'frmAddEdit')"
                                          v-validate="'required'" class="ml-1 mr-1"
                                          data-vv-scope="frmAddEdit"
                                          data-vv-name="Tên chuyên mục"
                                          clearable></v-text-field>
                        </v-col>

                        <v-col cols="6" class="pt-0">
                            <v-checkbox v-model="chuyenMuc.TrangThai" label="Trạng thái hoạt động"
                                        :error-messages="errors.collect('Trạng thái hoạt động', 'frmAddEdit')"
                                        v-validate="''" hide-details
                                        data-vv-scope="frmAddEdit"
                                        data-vv-name="Trạng thái hoạt động"></v-checkbox>
                        </v-col>
                        <v-col cols="6" class="pt-0">
                            <v-checkbox v-model="chuyenMuc.HienThiTrangChu" label="Hiển thị trang chủ"
                                        :error-messages="errors.collect('Hiển thị trang chủ', 'frmAddEdit')"
                                        v-validate="''"  hide-details
                                        data-vv-scope="frmAddEdit"
                                        data-vv-name="Hiển thị trang chủ"></v-checkbox>
                        </v-col>

                    </v-row>
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
    import ChuyenMucApi, { ChuyenMucApiSearchParams } from '@/apiResources/ChuyenMucApi';
    import { ChuyenMuc } from '@/models/ChuyenMuc';
    export default Vue.extend({
        $_veeValidate: {
            validator: 'new'
        },
        components: {},
        data() {
            return {
                isShow: false,
                isUpdate: false,
                chuyenMuc: {} as ChuyenMuc,
                loading: false,
                loadingSave: false,
                searchParamsChuyenMuc: {} as ChuyenMucApiSearchParams,
                active: [] as any[],
                open: [],
                chuyenMucID: 0,
            }
        },
        watch: {
        },
        methods: {
            hide() {
                this.isShow = false
            },
            show(isUpdate: boolean, item: any) {
                this.$validator.reset();
                this.loadingSave = false;
                this.isShow = true;
                this.isUpdate = isUpdate;
                if (isUpdate) {
                    this.chuyenMucID = item.ChuyenMucID;
                    this.getDataFromApi(this.chuyenMucID);
                }
                else {
                    this.chuyenMuc = {} as ChuyenMuc;
                }
            },
            getDataFromApi(id: number): void {
                ChuyenMucApi.get(id).then(res => {
                    this.chuyenMuc = res;
                });
            },
            save(): void {
                this.$validator.validateAll('frmAddEdit').then((res) => {
                    if (res) {
                        if (this.isUpdate) {
                            this.loading = true;
                            ChuyenMucApi.update(this.chuyenMucID, this.chuyenMuc).then(res => {
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
                            ChuyenMucApi.insert(this.chuyenMuc).then(res => {
                                this.chuyenMuc = res;
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