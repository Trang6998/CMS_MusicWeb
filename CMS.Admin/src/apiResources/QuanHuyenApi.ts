

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { QuanHuyen } from '@/models/QuanHuyen';
export interface QuanHuyenApiSearchParams extends Pagination {
    keyworlds?: string;
    tinhThanhID?: number;
}

class QuanHuyenApi extends BaseApi {
    
    search(quanHuyensearchParams: QuanHuyenApiSearchParams) : Promise<PaginatedResponse<QuanHuyen>> {
        return new Promise<PaginatedResponse<QuanHuyen>>((resolve: any, reject: any) => {
            HTTP.get(`api/quanHuyen`, { params: quanHuyensearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(quanHuyenID: number) : Promise<QuanHuyen> {
        return new Promise<QuanHuyen>((resolve: any, reject: any) => {
            HTTP.get(`api/quanHuyen/${quanHuyenID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(quanHuyen: QuanHuyen) : Promise<QuanHuyen> {
        return new Promise<QuanHuyen>((resolve: any, reject: any) => {
            HTTP.post(`api/quanHuyen`,quanHuyen)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(quanHuyenID: number, quanHuyen: QuanHuyen) : Promise<QuanHuyen> {
        return new Promise<QuanHuyen>((resolve: any, reject: any) => {
            HTTP.put(`api/quanHuyen/${quanHuyenID}`,quanHuyen)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(quanHuyenID: number) : Promise<QuanHuyen> {
        return new Promise<QuanHuyen>((resolve: any, reject: any) => {
            HTTP.delete(`api/quanHuyen/${quanHuyenID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new QuanHuyenApi();
