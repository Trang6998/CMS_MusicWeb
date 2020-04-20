import store from '../store/store';
export function ToFriendlyUrl(t: string) {
    if (null == t) return "x";
    for (var e, i = 80, a = t.length, r = !1, n = new Array, s = 0; a > s; s++) {
        if (e = t[s], e >= "a" && "z" >= e || e >= "0" && "9" >= e) n.push(e), r = !1;
        else if (e >= "A" && "Z" >= e) n.push(e.toLowerCase()), r = !1;
        else if (" " == e || "," == e || "." == e || "/" == e || "\\" == e || "-" == e || "_" == e || "=" == e) !r && n.length > 0 && (n.push("-"), r = !0);
        else if (e.charCodeAt(0) >= 128) {
            var o = n.length;
            n.push(RemapInternationalCharToAscii(e)), o != n.length && (r = !1)
        }
        if (s == i) break
    }
    return r ? "" == n.toString().substr(0, n.length - 1) ? "x" : n.join("").substr(0, n.length - 1) : "" == n.toString() ? "x" : n.join("").replace(",", "")
}

export function RemapInternationalCharToAscii(t: string) {
    var e = t.toLowerCase();
    return -1 != "àåáâäãåąạậấầẫẩắằẵẳảăặ".indexOf(e) ? "a" : -1 != "èéêëęệếềệẹẻể".indexOf(e) ? "e" : -1 != "ìíîïıịỉ".indexOf(e) ? "i" : -1 != "òóôõöøőðơờớợốộồỗổởọỏỡ".indexOf(e) ? "o" : -1 != "ùúûüŭůưứừựụủửữũ".indexOf(e) ? "u" : -1 != "çćčĉ".indexOf(e) ? "c" : -1 != "żźž".indexOf(e) ? "z" : -1 != "śşšŝ".indexOf(e) ? "s" : -1 != "ñń".indexOf(e) ? "n" : -1 != "ýÿỹýỳỵ".indexOf(e) ? "y" : -1 != "ğĝ".indexOf(e) ? "g" : -1 != "đđ".indexOf(e) ? "d" : "ř" == t ? "r" : "ł" == t ? "l" : "ß" == t ? "ss" : "Þ" == t ? "th" : "ĥ" == t ? "h" : "ĵ" == t ? "j" : ""
}
export default {
    install(Vue: any) {
        Vue.prototype.$commonFunctions = new CommonFunctions();
    }
}
export declare class CommonFunctionsService {
    checkRole(roleId: number): boolean;
    genPassword(): string;
}
export class CommonFunctions implements CommonFunctionsService {
    checkRole(roleId: number | any): boolean {
        return store.state.user.Profile.UserRole.indexOf(roleId) != -1;
    }
    shuffle(str: string) {
        var array = str.split('');
        var tmp, current, top = array.length;

        if (top) while (--top) {
            current = Math.floor(Math.random() * (top + 1));
            tmp = array[current];
            array[current] = array[top];
            array[top] = tmp;
        }

        return array.join('');
    }
    pick(str: string, min: number, max?: number) {
        var n, chars = '';

        if (typeof max === 'undefined') {
            n = min;
        } else {
            n = min + Math.floor(Math.random() * (max - min));
        }

        for (var i = 0; i < n; i++) {
            chars += str.charAt(Math.floor(Math.random() * str.length));
        }

        return chars;
    }
    genPassword(): string {
        let specials = '!@#$%^&*()_+{}:"<>?\|[];\',./`~';
        let lowercase = 'abcdefghijklmnopqrstuvwxyz';
        let uppercase = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        let numbers = '0123456789';

        let all = specials + lowercase + uppercase + numbers;

        let password = (this.pick(specials, 1) + this.pick(lowercase, 4) + this.pick(numbers, 1) + this.pick(uppercase, 1));
        password = this.shuffle(password);
        return password;
    }
}