<template>
 <v-container fluid fill-height>
  <v-layout  align-center justify-center class="login_page_wrapper">
        <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
              <v-card-text>
                <p v-if="msg" class="error--text text-xs-center">{{msg}}</p>
                <v-form>
                  <v-text-field v-model="credentials.username" @keypress.enter="submit" prepend-icon="person" name="login" label="Tài khoản" type="text" required></v-text-field>
                  <v-text-field v-model="credentials.password" @keypress.enter="submit" prepend-icon="lock" name="password" label="Mật khẩu" id="password" type="password" required></v-text-field>
                </v-form>
                <v-btn block color="primary" :loading="loggingIn" :disabled="loggingIn" @click="submit">Đăng nhập</v-btn>
                <v-layout row wrap>
                  <v-flex xs6>
                        <v-checkbox
                          label="Ghi nhớ?"
                          v-model="credentials.remember" hide-details></v-checkbox>
                  </v-flex>
                  <v-flex xs6 class="text-xs-right">
                        <!-- <router-link to="/request-change-password">Quên mật khẩu ?</router-link> -->
                  </v-flex>
                </v-layout>
              </v-card-text>
            </v-card>
          </v-flex>
  </v-layout>
 </v-container>
</template>

<script lang="ts">
    import { Vue } from 'vue-property-decorator';
    import auth from '@/auth';

    export default Vue.extend({
        components: {},
        data() {
            return {
                credentials: {
                    username: '',
                    remember: false,
                    password: ''
                },
                loggingIn: false,
                msg: ''
            };
        },
        methods: {
            submit() {
                this.loggingIn = true;
                auth.login(this.credentials, 'home')
                    .then((response: any) => {
                    this.loggingIn = false;
                        if (!this.$store.state.user.AccessToken.IsAuthenticated) {
                            this.$snotify.error('Sai tên đăng nhập hoặc mật khẩu.');
                        }
                        else {
                            this.$emit('onLoggedIn');
                        }
                })
                    .catch((errorResponse: any) => {
                        this.loggingIn = false;
                        if (errorResponse !== 'Login failed!') {
                            this.msg = 'Có lỗi xảy ra.';
                        }
                        else {
                            this.msg = 'Tài khoản hoặc mật khẩu không chính xác.'
                        }
                        this.$snotify.error(this.msg);
                    });
            }
        }
    });
</script>
