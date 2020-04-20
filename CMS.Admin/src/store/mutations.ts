export default {
    UPDATE_APP(state: any, app: any) {
        state.app = app
    },
    UPDATE_USER(state: any, user: any) {
        state.user = user
    },
    UPDATE_DAYSPAN(state: any, daySpanState: any) {
        state.daySpanState = daySpanState
    },
    CLEAR_ALL_DATA(state: any) {
        state.user = {
            AccessToken: {
                IsAuthenticated: false,
                Token: null,
                UserName: null,
                RefreshToken: null,
                EffectiveTime: null,
                ExpiresTime: null
            },
            Profile: {
                UserId: null,
                Username: null,
                Email: null,
                UserRole: []
            }
        }
    }
}
