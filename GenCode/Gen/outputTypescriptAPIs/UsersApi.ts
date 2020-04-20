

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { User } from '@/models/User';
import { ChangePassDto } from '@/models/ChangePassDto';
export interface UserApiSearchAsyncParams extends Pagination {
   q? : string
}

class UserApi extends BaseApi {
    
    searchAsync(userssearchAsyncParams: UsersApiSearchAsyncParams) : Promise<PaginatedResponse<Users>> {
        return new Promise<PaginatedResponse<Users>>((resolve: any, reject: any) => {
            HTTP.get(`api/users`, { params: userssearchAsyncParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    getAsync(userId: number) : Promise<Users> {
        return new Promise<Users>((resolve: any, reject: any) => {
            HTTP.get(`api/users/${userId}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insertAsync(users: User) : Promise<Users> {
        return new Promise<Users>((resolve: any, reject: any) => {
            HTTP.post(`api/users`,users)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    updateAsync(userId: number, users: User) : Promise<Users> {
        return new Promise<Users>((resolve: any, reject: any) => {
            HTTP.put(`api/users/${userId}`,users)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    updatePasswordAsync(userId: number, changePass: ChangePassDto) : Promise<Users> {
        return new Promise<Users>((resolve: any, reject: any) => {
            HTTP.put(`api/users/${userId}/changepass`,changePass)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    deleteAsync(userId: number) : Promise<Users> {
        return new Promise<Users>((resolve: any, reject: any) => {
            HTTP.delete(`api/users/${userId}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new UserApi();
