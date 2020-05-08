<template>
    <v-navigation-drawer id="appDrawer" persistent
                         v-model="$store.state.app.drawer"
                         fixed
                         width="220"
                         app
                         enable-resize-watcher>
        <v-toolbar color="primary lighten-0" dark
                   height="50"
                   to="/">
            <a href="/">
                <img v-bind:src="'http://localhost:8888/img/core-img/logo.png'" height="30" alt="Insight">
            </a>
            <v-toolbar-title class="ml-0 pl-3">
                <span class="hidden-sm-and-down" style="font-size:16px;"></span>
            </v-toolbar-title>
        </v-toolbar>
        <v-list id="leftSideBar">
            <vue-perfect-scrollbar class="drawer-menu--scroll" :settings="scrollSettings">
                <v-list dense expand>
                    <template v-if="dsMenu.length != 0" v-for="(item, i) in dsMenu">
                        <!--group with subitems-->
                        <v-list-group v-if="item.items && item.show" :key="item.name" :group="item.group" :prepend-icon="item.icon"  v-model="item.active" no-action="no-action">
                            <v-list-item slot="activator" ripple="ripple" v-if="item.link">
                                <v-list-item-content>
                                    <v-list-item-title>{{ item.title }}</v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                            <template v-for="(subItem, i) in item.items">
                                <!--sub group-->
                                <v-list-group v-if="subItem.items  && subItem.show" :key="subItem.name" :group="subItem.group" sub-group="sub-group">
                                    <v-list-item class="pa-0" slot="activator" ripple="ripple">
                                        <v-list-item-content>
                                            <v-list-item-title style="padding-left: 30px">{{ subItem.title }}</v-list-item-title>
                                        </v-list-item-content>
                                    </v-list-item>
                                    <v-list-item v-for="(grand, i) in subItem.children" :key="i" :to="grand.link" :href="grand.href" ripple="ripple">
                                        <v-list-item-content>
                                            <v-list-item-title>{{ grand.title }}</v-list-item-title>
                                        </v-list-item-content>
                                    </v-list-item>
                                </v-list-group>
                                <!--child item-->
                                <v-list-item v-else-if="subItem.show" :key="i" :to="subItem.link" :href="subItem.href" :disabled="subItem.disabled" :target="subItem.target" ripple="ripple">
                                    <v-list-item-content>
                                        <v-list-item-title style="padding-left: 30px"><span>{{ subItem.title }}</span></v-list-item-title>
                                    </v-list-item-content>
                                    <!-- <v-circle class="white--text pa-0 circle-pill" v-if="subItem.badge" color="red" disabled="disabled">{{ subItem.badge }}</v-circle> -->
                                    <v-list-item-action v-if="subItem.action">
                                        <v-icon :class="[subItem.actionClass || 'success--text']">{{ subItem.action }}</v-icon>
                                    </v-list-item-action>
                                </v-list-item>
                            </template>
                        </v-list-group>
                        <v-subheader v-else-if="item.header && item.show" :key="i">{{ item.header }}</v-subheader>
                        <v-divider v-else-if="item.divider && item.show" :key="i"></v-divider>
                        <!--top-level link-->
                        <v-list-item v-else-if="item.show" :to="item.link" :href="item.href" ripple="ripple" :disabled="item.disabled" :target="item.target" rel="noopener" :key="item.name">
                            <v-list-item-action v-if="item.icon">
                                <v-icon>{{ item.icon }}</v-icon>
                            </v-list-item-action>
                            <v-list-item-content v-if="item.link">
                                <v-list-item-title>{{ item.title }}</v-list-item-title>
                            </v-list-item-content>
                            <v-list-item-content v-if="item.href">
                                <a style="text-decoration:none;color:#401111" :href="item.href" target="_blank"><v-list-item-title>{{ item.title }}</v-list-item-title></a>
                            </v-list-item-content>
                            <!-- <v-circle class="white--text pa-0 chip--x-small" v-if="item.badge" :color="item.color || 'primary'" disabled="disabled">{{ item.badge }}</v-circle> -->
                            <v-list-item-action v-if="item.subAction">
                                <v-icon class="success--text">{{ item.subAction }}</v-icon>
                            </v-list-item-action>
                            <!-- <v-circle class="caption blue lighten-2 white--text mx-0" v-else-if="item.chip" label="label" small="small">{{ item.chip }}</v-circle> -->
                        </v-list-item>
                    </template>
                </v-list>
            </vue-perfect-scrollbar>
        </v-list>
    </v-navigation-drawer>
</template>

<script>
    import VuePerfectScrollbar from 'vue-perfect-scrollbar';

    export default {
        name: 'App',
        components: { VuePerfectScrollbar },
        data() {
            return {
                scrollSettings: {
                    maxScrollbarLength: 160
                },
                clipped: false,
                drawer: true,
                fixed: true,
                miniVariant: false,
                right: true,
                rightDrawer: false,
                title: '',
                user: this.$store.state.user.Profile,
                dsMenu: []
            };
        },
        computed: {
            isLoggedIn() {
                this.user = this.$store.state.user.Profile
                return this.$store.state.user
                    && this.$store.state.user.AccessToken
                    && this.$store.state.user.AccessToken.IsAuthenticated;
            }
        },
        mounted() {
            this.dsMenu = this.getDSMenu();
        },
        methods: {
            getDSMenu() {
                return [
                    {
                        icon: 'chat',
                        title: 'Quản lý bài viết',
                        active: true,
                        show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 4,
                        link: '#',
                        items: [
                            {
                                icon: 'fas fa-clipboard-list',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 4,
                                title: 'Chuyên mục bài viết',
                                link: '/chuyenmuc'
                            },
                            {
                                icon: 'fas fa-clipboard-list',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 4,
                                title: 'Bài viết',
                                link: '/baiviet'
                            }
                        ]
                    },
                    {
                        icon: 'play_circle_filled',
                        title: 'Quản lý âm thanh',
                        active: true,
                        show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 5,
                        link: '#',
                        items: [

                            {
                                icon: 'view_list',
                                title: 'Bài hát',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 5,
                                link: '/baihat'
                            },

                            {
                                icon: 'supervisor_account',
                                title: 'Radio',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 5,
                                link: '/radio'
                            }
                        ]
                    },
                    {
                        icon: 'view_list',
                        title: 'Quản lý thông tin',
                        active: true,
                        show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 6 || this.user.LoaiTaiKhoanID == 5,
                        link: '#',
                        items: [
                            {
                                icon: 'monetization_on',
                                title: 'Thể loại nhạc',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 6 || this.user.LoaiTaiKhoanID == 5,
                                link: '/theloainhac'
                            },
                            {
                                icon: 'flag',
                                title: 'Ca sỹ',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 6,
                                link: '/casy'
                            },
                            {
                                icon: 'mode_edit',
                                title: 'Album',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 6 || this.user.LoaiTaiKhoanID == 5,
                                link: '/album'
                            },
                            {
                                icon: 'mood',
                                title: 'Sự kiện',
                                show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 6,
                                link: '/sukien'
                            },
                        ]
                    },

                    {
                        icon: 'supervisor_account',
                        title: 'Quản lý tài khoản',
                        active: true,
                        show: this.user.LoaiTaiKhoanID == 1,
                        link: '#',
                        items: [
                            {
                                icon: 'insert_chart',
                                title: 'Loại nhân viên',
                                show: this.user.LoaiTaiKhoanID == 1,
                                link: '/loainhanvien'
                            },
                            {
                                icon: 'account_circle',
                                title: 'Tài khoản nhân viên',
                                show: this.user.LoaiTaiKhoanID == 1,
                                link: '/nhanvien'
                            },
                        ]
                    },
                    {
                        icon: 'feedback',
                        title: 'Liên hệ bạn đọc',
                        show: this.user.LoaiTaiKhoanID == 1 || this.user.LoaiTaiKhoanID == 4,
                        link: '/lienhe'
                    }
                ];
            },
        }
    };
</script>

<style>

    #leftSideBar .v-list-item__action:first-child, #appDrawer .v-list-item__icon:first-child {
        margin-right: 5px !important;
    }

    #leftSideBar .v-list-group .v-list-group__header .v-list-item__icon.v-list-group__header__append-icon {
        min-width: 25px !important;
    }

    #leftSideBar .v-list-group__header > .v-list-item {
        padding-left: 0 !important;
    }

    #leftSideBar .v-list-group--no-action > .v-list-group__items > div > .v-list-item {
        padding-left: 36px !important;
    }

    #leftSideBar .v-list-item {
        padding: 0 8px;
    }

    #leftSideBar .v-list--dense .v-list-item .v-list-item__content, #leftSideBar .v-list-item--dense .v-list-item__content {
        padding: 8px 5px !important;
    }
</style>