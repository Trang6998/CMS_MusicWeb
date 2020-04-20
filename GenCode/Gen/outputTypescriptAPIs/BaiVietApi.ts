

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { BaiViet } from '@/models/BaiViet';
export interface BaiVietApiSearchParams extends Pagination {
   keyworlds? : string
}

class BaiVietApi extends BaseApi {
    
    search(baiVietsearchParams: BaiVietApiSearchParams) : Promise<PaginatedResponse<BaiViet>> {
        return new Promise<PaginatedResponse<BaiViet>>((resolve: any, reject: any) => {
            HTTP.get(`api/baiViet`, { params: baiVietsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(baiVietID: number) : Promise<BaiViet> {
        return new Promise<BaiViet>((resolve: any, reject: any) => {
            HTTP.get(`api/baiViet/${baiVietID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(baiViet: BaiViet) : Promise<BaiViet> {
        return new Promise<BaiViet>((resolve: any, reject: any) => {
            HTTP.post(`api/baiViet`,baiViet)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(baiVietID: number, baiViet: BaiViet) : Promise<BaiViet> {
        return new Promise<BaiViet>((resolve: any, reject: any) => {
            HTTP.put(`api/baiViet/${baiVietID}`,baiViet)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(baiVietID: number) : Promise<BaiViet> {
        return new Promise<BaiViet>((resolve: any, reject: any) => {
            HTTP.delete(`api/baiViet/${baiVietID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new BaiVietApi();
