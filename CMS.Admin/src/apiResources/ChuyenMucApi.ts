

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { ChuyenMuc } from '@/models/ChuyenMuc';
export interface ChuyenMucApiSearchParams extends Pagination {
   keyworlds? : string
}

class ChuyenMucApi extends BaseApi {
    
    search(chuyenMucsearchParams: ChuyenMucApiSearchParams) : Promise<PaginatedResponse<ChuyenMuc>> {
        return new Promise<PaginatedResponse<ChuyenMuc>>((resolve: any, reject: any) => {
            HTTP.get(`api/chuyenMuc`, { params: chuyenMucsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(chuyenMucID: number) : Promise<ChuyenMuc> {
        return new Promise<ChuyenMuc>((resolve: any, reject: any) => {
            HTTP.get(`api/chuyenMuc/${chuyenMucID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(chuyenMuc: ChuyenMuc) : Promise<ChuyenMuc> {
        return new Promise<ChuyenMuc>((resolve: any, reject: any) => {
            HTTP.post(`api/chuyenMuc`,chuyenMuc)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(chuyenMucID: number, chuyenMuc: ChuyenMuc) : Promise<ChuyenMuc> {
        return new Promise<ChuyenMuc>((resolve: any, reject: any) => {
            HTTP.put(`api/chuyenMuc/${chuyenMucID}`,chuyenMuc)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(chuyenMucID: number) : Promise<ChuyenMuc> {
        return new Promise<ChuyenMuc>((resolve: any, reject: any) => {
            HTTP.delete(`api/chuyenMuc/${chuyenMucID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new ChuyenMucApi();
