import Vue from 'vue';
import Router, { Route } from 'vue-router';
import store from './store/store';
import { HTTP } from '@/HTTPServices';
import Login from './components/Login/Login.vue';
import DanhSachChuyenMuc from './components/QuanLyBaiViet/ChuyenMuc/DanhSachChuyenMuc.vue';
import DanhSachBaiViet from './components/QuanLyBaiViet/BaiViet/DanhSachBaiViet.vue';
import DanhSachCaSy from './components/QuanLyThongTin/CaSy/DanhSachCaSy.vue';
import DanhSachTheLoai from './components/QuanLyThongTin/TheLoaiNhac/DanhSachTheLoai.vue';
import DanhSachSuKien from './components/QuanLyThongTin/SuKien/DanhSachSuKien.vue';
import DanhSachUsers from './components/Users/DanhSachUsers.vue';
import DanhSachAlbum from './components/QuanLyThongTin/Album/DanhSachAlbum.vue';
import DanhSachLoaiTaiKhoan from './components/QuanLyTaiKhoan/LoaiTaiKhoan/DanhSachLoaiTaiKhoan.vue';
import DanhSachLienHe from './components/LienHe/DanhSachLienHe.vue';
import DanhSachRadio from './components/QuanLyNhac/Radio/DanhSachRadio.vue';
import Home from './views/Home.vue';

Vue.use(Router);
export default new Router({
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/login',
            name: 'login',
            component: Login
        },
        {
            path: '/chuyenmuc',
            name: 'chuyenmuc',
            component: DanhSachChuyenMuc
        },
        {
            path: '/baiviet',
            name: 'baiviet',
            component: DanhSachBaiViet
        },
        {
            path: '/casy',
            name: 'casy',
            component: DanhSachCaSy
        },
        {
            path: '/theloainhac',
            name: 'theloainhac',
            component: DanhSachTheLoai
        },
        {
            path: '/sukien',
            name: 'sukien',
            component: DanhSachSuKien
        },
        {
            path: '/album',
            name: 'album',
            component: DanhSachAlbum
        },
        {
            path: '/loainhanvien',
            name: 'loaitaikhoan',
            component: DanhSachLoaiTaiKhoan
        },
        {
            path: '/lienhe',
            name: 'lienhe',
            component: DanhSachLienHe
        },
        {
            path: '/radio',
            name: 'radio',
            component: DanhSachRadio
        },
        {
            path: '/users',
            name: 'users',
            component: DanhSachUsers
        }

    ],
});


function guardRoute(to: Route, from: Route, next: any): void {
    // work-around to get to the Vuex store (as of Vue 2.0)
    const isAuthenticated = store.state.user && store.state.user.AccessToken ? store.state.user.AccessToken.IsAuthenticated : false;
    if (!isAuthenticated) {
        next({
            path: '/login',
            query: {
                redirect: to.fullPath
            }
        });
    } else {
        HTTP.get('/auth/validate-token/')
            .then(response => {
                next();
            })
            .catch(e => {
                store.commit('CLEAR_ALL_DATA');
                next({
                    path: '/login',
                    query: {
                        redirect: to.fullPath
                    }
                });
            });
    }
}