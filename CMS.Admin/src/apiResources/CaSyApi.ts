

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { CaSy } from '@/models/CaSy';
export interface CaSyApiSearchParams extends Pagination {
   keyworlds? : string
}

class CaSyApi extends BaseApi {
    
    search(caSysearchParams: CaSyApiSearchParams) : Promise<PaginatedResponse<CaSy>> {
        return new Promise<PaginatedResponse<CaSy>>((resolve: any, reject: any) => {
            HTTP.get(`api/caSy`, { params: caSysearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(caSyID: number) : Promise<CaSy> {
        return new Promise<CaSy>((resolve: any, reject: any) => {
            HTTP.get(`api/caSy/${caSyID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(caSy: CaSy) : Promise<CaSy> {
        return new Promise<CaSy>((resolve: any, reject: any) => {
            HTTP.post(`api/caSy`,caSy)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(caSyID: number, caSy: CaSy) : Promise<CaSy> {
        return new Promise<CaSy>((resolve: any, reject: any) => {
            HTTP.put(`api/caSy/${caSyID}`,caSy)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(caSyID: number) : Promise<CaSy> {
        return new Promise<CaSy>((resolve: any, reject: any) => {
            HTTP.delete(`api/caSy/${caSyID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new CaSyApi();
