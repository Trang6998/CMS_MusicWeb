

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { CaSy_BaiHat } from '@/models/CaSy_BaiHat';
export interface CaSy_BaiHatApiSearchParams extends Pagination {
   keyworlds? : string
}

class CaSy_BaiHatApi extends BaseApi {
    
    search(caSy_BaiHatsearchParams: CaSy_BaiHatApiSearchParams) : Promise<PaginatedResponse<CaSy_BaiHat>> {
        return new Promise<PaginatedResponse<CaSy_BaiHat>>((resolve: any, reject: any) => {
            HTTP.get(`api/caSy_BaiHat`, { params: caSy_BaiHatsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(caSy_BaiHat: CaSy_BaiHat) : Promise<CaSy_BaiHat> {
        return new Promise<CaSy_BaiHat>((resolve: any, reject: any) => {
            HTTP.post(`api/caSy_BaiHat`,caSy_BaiHat)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new CaSy_BaiHatApi();
