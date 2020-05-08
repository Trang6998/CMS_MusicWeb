

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { BaiHat } from '@/models/BaiHat';
export interface BaiHatApiSearchParams extends Pagination {
    keyworlds?: string;
    caSyID?: number;
    theLoaiID?: number;
    albumID?: number;
    ngayDangTu?: Date;
    ngayDangDen?: Date;
}

class BaiHatApi extends BaseApi {
    
    search(baiHatsearchParams: BaiHatApiSearchParams) : Promise<PaginatedResponse<BaiHat>> {
        return new Promise<PaginatedResponse<BaiHat>>((resolve: any, reject: any) => {
            HTTP.get(`api/baiHat`, { params: baiHatsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(baiHatID: number) : Promise<BaiHat> {
        return new Promise<BaiHat>((resolve: any, reject: any) => {
            HTTP.get(`api/baiHat/${baiHatID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(baiHat: BaiHat) : Promise<BaiHat> {
        return new Promise<BaiHat>((resolve: any, reject: any) => {
            HTTP.post(`api/baiHat`,baiHat)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(baiHatID: number, baiHat: BaiHat) : Promise<BaiHat> {
        return new Promise<BaiHat>((resolve: any, reject: any) => {
            HTTP.put(`api/baiHat/${baiHatID}`,baiHat)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(baiHatID: number) : Promise<BaiHat> {
        return new Promise<BaiHat>((resolve: any, reject: any) => {
            HTTP.delete(`api/baiHat/${baiHatID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new BaiHatApi();
