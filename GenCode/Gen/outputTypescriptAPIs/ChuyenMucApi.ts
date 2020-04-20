

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { ChuyenMuc } from '@/models/ChuyenMuc';
export interface ChuyenMucApiSearchParams extends Pagination {
   keyworlds? : string
}

class ChuyenMucApi extends BaseApi {
    
    search(chuyenMucsearchParams: ChuyenMuCApiSearchParams) : Promise<PaginatedResponse<ChuyenMuC>> {
        return new Promise<PaginatedResponse<ChuyenMuC>>((resolve: any, reject: any) => {
            HTTP.get(`api/chuyenMuc`, { params: chuyenMucsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(chuyenMucID: number) : Promise<ChuyenMuC> {
        return new Promise<ChuyenMuC>((resolve: any, reject: any) => {
            HTTP.get(`api/chuyenMuc/${chuyenMucID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(chuyenMuc: ChuyenMuc) : Promise<ChuyenMuC> {
        return new Promise<ChuyenMuC>((resolve: any, reject: any) => {
            HTTP.post(`api/chuyenMuc`,chuyenMuc)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(chuyenMucID: number, chuyenMuc: ChuyenMuc) : Promise<ChuyenMuC> {
        return new Promise<ChuyenMuC>((resolve: any, reject: any) => {
            HTTP.put(`api/chuyenMuc/${chuyenMucID}`,chuyenMuc)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(chuyenMucID: number) : Promise<ChuyenMuC> {
        return new Promise<ChuyenMuC>((resolve: any, reject: any) => {
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
