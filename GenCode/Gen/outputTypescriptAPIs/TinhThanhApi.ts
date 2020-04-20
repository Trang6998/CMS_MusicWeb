

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { TinhThanh } from '@/models/TinhThanh';
export interface TinhThanhApiSearchParams extends Pagination {
   keyworlds? : string
}

class TinhThanhApi extends BaseApi {
    
    search(tinhThanhsearchParams: TinhThanhApiSearchParams) : Promise<PaginatedResponse<TinhThanh>> {
        return new Promise<PaginatedResponse<TinhThanh>>((resolve: any, reject: any) => {
            HTTP.get(`api/tinhThanh`, { params: tinhThanhsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(tinhThanhID: number) : Promise<TinhThanh> {
        return new Promise<TinhThanh>((resolve: any, reject: any) => {
            HTTP.get(`api/tinhThanh/${tinhThanhID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(tinhThanh: TinhThanh) : Promise<TinhThanh> {
        return new Promise<TinhThanh>((resolve: any, reject: any) => {
            HTTP.post(`api/tinhThanh`,tinhThanh)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(tinhThanhID: number, tinhThanh: TinhThanh) : Promise<TinhThanh> {
        return new Promise<TinhThanh>((resolve: any, reject: any) => {
            HTTP.put(`api/tinhThanh/${tinhThanhID}`,tinhThanh)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(tinhThanhID: number) : Promise<TinhThanh> {
        return new Promise<TinhThanh>((resolve: any, reject: any) => {
            HTTP.delete(`api/tinhThanh/${tinhThanhID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new TinhThanhApi();
