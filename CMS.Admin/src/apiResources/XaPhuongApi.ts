

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { XaPhuong } from '@/models/XaPhuong';
export interface XaPhuongApiSearchParams extends Pagination {
    keyworlds?: string;
    quanHuyenID?: number;
}

class XaPhuongApi extends BaseApi {
    
    search(xaPhuongsearchParams: XaPhuongApiSearchParams) : Promise<PaginatedResponse<XaPhuong>> {
        return new Promise<PaginatedResponse<XaPhuong>>((resolve: any, reject: any) => {
            HTTP.get(`api/xaPhuong`, { params: xaPhuongsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(xaPhuongID: number) : Promise<XaPhuong> {
        return new Promise<XaPhuong>((resolve: any, reject: any) => {
            HTTP.get(`api/xaPhuong/${xaPhuongID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(xaPhuong: XaPhuong) : Promise<XaPhuong> {
        return new Promise<XaPhuong>((resolve: any, reject: any) => {
            HTTP.post(`api/xaPhuong`,xaPhuong)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(xaPhuongID: number, xaPhuong: XaPhuong) : Promise<XaPhuong> {
        return new Promise<XaPhuong>((resolve: any, reject: any) => {
            HTTP.put(`api/xaPhuong/${xaPhuongID}`,xaPhuong)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(xaPhuongID: number) : Promise<XaPhuong> {
        return new Promise<XaPhuong>((resolve: any, reject: any) => {
            HTTP.delete(`api/xaPhuong/${xaPhuongID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new XaPhuongApi();
