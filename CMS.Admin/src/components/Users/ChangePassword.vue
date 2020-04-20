<template>
    <v-layout>
        <v-flex xs12 sm4 offset-sm4>
            <v-card xs4 offset-xs4>
                <form scope="formChangePassword">
                    <v-container grid-list-md>
                        <v-flex>
                            <v-layout wrap style="padding: 5px 20px;">
                                <v-flex xs12>
                                    <v-text-field v-model="change.OldPassword"
                                                  label="Mật khẩu hiện tại"
                                                  required
                                                  name="Mật khẩu hiện tại"
                                                  data-vv-scope="formChangePassword"
                                                  :error-messages="errors.collect('Mật khẩu hiện tại', 'formChangePassword')"
                                                  v-validate="'required'"
                                                  :append-icon="e1 ? 'visibility' : 'visibility_off'"
                                                  :append-icon-cb="() => (e1 = !e1)"
                                                  :type="e1 ? 'password' : 'text'">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12>
                                    <v-text-field v-model="change.NewPassword"
                                                  label="Mật khẩu mới"
                                                  required
                                                  :error-messages="errors.collect('Mật khẩu mới', 'formChangePassword')"
                                                  v-validate="'required'"
                                                  name="Mật khẩu mới"
                                                  data-vv-scope="formChangePassword"
                                                  :append-icon="e2 ? 'visibility' : 'visibility_off'"
                                                  :append-icon-cb="() => (e2 = !e2)"
                                                  :type="e2 ? 'password' : 'text'">
                                    </v-text-field>
                                </v-flex>
                                <v-flex xs12>
                                    <v-text-field v-model="repassword"
                                                  label="Nhập lại mật khẩu mới"
                                                  required
                                                  :error-messages="errors.collect('Nhập lại mật khẩu mới', 'formChangePassword')"
                                                  v-validate="'required'"
                                                  name="Nhập lại mật khẩu mới"
                                                  data-vv-scope="formChangePassword"
                                                  :append-icon="e3 ? 'visibility' : 'visibility_off'"
                                                  :append-icon-cb="() => (e3 = !e3)"
                                                  :type="e3 ? 'password' : 'text'">
                                    </v-text-field>
                                </v-flex>
                                <v-layout>
                                    <v-spacer></v-spacer>
                                    <v-btn class="primary" :loading="loading" :disabled="loading" flat @click.native="save">Đổi mật khẩu</v-btn>
                                </v-layout>
                            </v-layout>
                        </v-flex>
                    </v-container>
                </form>
            </v-card>
        </v-flex>
    </v-layout>
</template>

<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import UsersApi from '@/apiResources/UsersApi';

    export default Vue.extend({
        data() {
            return {
                e1: true,
                e2: true,
                e3: true,
                dialog: false,
                change: {
                    UserName: this.$store.state.user.Profile.UserName,
                    NewPassword: '',
                    OldPassword: ''
                },
                repassword: '',
                loading: false
            }
        },
        methods: {
            save() {
                //this.$validator.validateAll('formChangePassword')
                //    .then(res => {
                //        if (res && (this.repassword === this.change.NewPassword) && (this.change.NewPassword != this.change.OldPassword)) {
                //            let id = this.$store.state.user.Profile.UserId as number;
                //            let userName = this.$store.state.user.Profile.Username;
                //            UsersApi.changePass(id, {
                //                Password: this.change.OldPassword,
                //                NewPassword: this.change.NewPassword
                //            }).then(res => {
                //                this.loading = false;
                //                this.$snotify.success('Đổi mật khẩu thành công')
                //            }).catch(res => {
                //                this.loading = false;
                //                this.$snotify.error(res.response.data.Message);
                //            });
                //        } else if (this.change.NewPassword == this.change.OldPassword) {
                //            this.$snotify.error('Mật khẩu mới không được trùng với mật khẩu hiện tại!')
                //        } else if (this.repassword !== this.change.NewPassword) {
                //            this.$snotify.error('Nhập lại mật không khớp!')
                //        } else {
                //            this.$snotify.error('Vui lòng điền đủ thông tin!')
                //        }
                //    })
            }
        }
    });
</script>
