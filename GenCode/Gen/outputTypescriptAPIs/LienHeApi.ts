

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { LienHe } from '@/models/LienHe';
export interface LienHeApiSearchParams extends Pagination {
   keyworlds? : string
}

class LienHeApi extends BaseApi {
    
    search(lienHesearchParams: LienHeApiSearchParams) : Promise<PaginatedResponse<LienHe>> {
        return new Promise<PaginatedResponse<LienHe>>((resolve: any, reject: any) => {
            HTTP.get(`api/lienHe`, { params: lienHesearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(lienHeID: number) : Promise<LienHe> {
        return new Promise<LienHe>((resolve: any, reject: any) => {
            HTTP.get(`api/lienHe/${lienHeID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(lienHe: LienHe) : Promise<LienHe> {
        return new Promise<LienHe>((resolve: any, reject: any) => {
            HTTP.post(`api/lienHe`,lienHe)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(lienHeID: number, lienHe: LienHe) : Promise<LienHe> {
        return new Promise<LienHe>((resolve: any, reject: any) => {
            HTTP.put(`api/lienHe/${lienHeID}`,lienHe)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(lienHeID: number) : Promise<LienHe> {
        return new Promise<LienHe>((resolve: any, reject: any) => {
            HTTP.delete(`api/lienHe/${lienHeID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new LienHeApi();
