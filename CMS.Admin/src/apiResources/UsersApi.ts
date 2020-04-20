

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { User } from '@/models/User';
export interface UserApiSearchAsyncParams extends Pagination {
   q? : string
}

class UserApi extends BaseApi {
    
    searchAsync(usersearchAsyncParams: UserApiSearchAsyncParams) : Promise<PaginatedResponse<User>> {
        return new Promise<PaginatedResponse<User>>((resolve: any, reject: any) => {
            HTTP.get(`api/user`, { params: usersearchAsyncParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    getAsync(userId: number) : Promise<User> {
        return new Promise<User>((resolve: any, reject: any) => {
            HTTP.get(`api/user/${userId}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insertAsync(user: User) : Promise<User> {
        return new Promise<User>((resolve: any, reject: any) => {
            HTTP.post(`api/user`,user)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    updateAsync(userId: number, user: User) : Promise<User> {
        return new Promise<User>((resolve: any, reject: any) => {
            HTTP.put(`api/user/${userId}`,user)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    deleteAsync(userId: number) : Promise<User> {
        return new Promise<User>((resolve: any, reject: any) => {
            HTTP.delete(`api/user/${userId}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new UserApi();
