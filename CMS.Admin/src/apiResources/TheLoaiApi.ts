

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { TheLoai } from '@/models/TheLoai';
export interface TheLoaiApiSearchParams extends Pagination {
   keyworlds? : string
}

class TheLoaiApi extends BaseApi {
    
    search(theLoaisearchParams: TheLoaiApiSearchParams) : Promise<PaginatedResponse<TheLoai>> {
        return new Promise<PaginatedResponse<TheLoai>>((resolve: any, reject: any) => {
            HTTP.get(`api/theLoai`, { params: theLoaisearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(theLoaiID: number) : Promise<TheLoai> {
        return new Promise<TheLoai>((resolve: any, reject: any) => {
            HTTP.get(`api/theLoai/${theLoaiID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(theLoai: TheLoai) : Promise<TheLoai> {
        return new Promise<TheLoai>((resolve: any, reject: any) => {
            HTTP.post(`api/theLoai`,theLoai)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(theLoaiID: number, theLoai: TheLoai) : Promise<TheLoai> {
        return new Promise<TheLoai>((resolve: any, reject: any) => {
            HTTP.put(`api/theLoai/${theLoaiID}`,theLoai)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(theLoaiID: number) : Promise<TheLoai> {
        return new Promise<TheLoai>((resolve: any, reject: any) => {
            HTTP.delete(`api/theLoai/${theLoaiID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new TheLoaiApi();
