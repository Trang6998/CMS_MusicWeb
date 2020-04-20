export const STORAGE_KEY = 'jdow344dsmbd$e&5';
let Cookies = require('js-cookie');

let syncedData = {
    app: {
        clipped: false,
        drawer: true,
        fixed: true,
        miniVariant: false,
        right: false,
        rightDrawer: false,
        title: 'Title'
    },
    dayspanState: {},
    user: {
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
            UserRole: [] as any[]
        }
    },
    auth: {} as any
}

const notSyncedData = {
}

// Sync with local storage.
if (Cookies.get(STORAGE_KEY)) {
    var data = Cookies.get(STORAGE_KEY)
    if (data === undefined) {
        data = ''
    } else {
        syncedData = JSON.parse(data)
    }
}

// Merge data and export it.
export const state = Object.assign(syncedData, notSyncedData)
