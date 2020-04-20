

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { LoginForm } from '@/models/LoginForm';

class AuthApi extends BaseApi {
    
    login(loginForm: LoginForm) : Promise<Auth> {
        return new Promise<Auth>((resolve: any, reject: any) => {
            HTTP.post(`api/auth/login`,loginForm)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    validateToken() : Promise<Auth> {
        return new Promise<Auth>((resolve: any, reject: any) => {
            HTTP.get(`api/auth/validate-token`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new AuthApi();
