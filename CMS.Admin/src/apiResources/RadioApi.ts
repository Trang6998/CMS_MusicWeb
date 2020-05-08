

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { Radio } from '@/models/Radio';
export interface RadioApiSearchParams extends Pagination {
    keyworlds?: string;
    nguoiDangID?: number;
    ngayDangTu?: Date;
    ngayDangDen?: Date;
}

class RadioApi extends BaseApi {
    
    search(radiosearchParams: RadioApiSearchParams) : Promise<PaginatedResponse<Radio>> {
        return new Promise<PaginatedResponse<Radio>>((resolve: any, reject: any) => {
            HTTP.get(`api/radio`, { params: radiosearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(radioID: number) : Promise<Radio> {
        return new Promise<Radio>((resolve: any, reject: any) => {
            HTTP.get(`api/radio/${radioID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(radio: Radio) : Promise<Radio> {
        return new Promise<Radio>((resolve: any, reject: any) => {
            HTTP.post(`api/radio`,radio)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(radioID: number, radio: Radio) : Promise<Radio> {
        return new Promise<Radio>((resolve: any, reject: any) => {
            HTTP.put(`api/radio/${radioID}`,radio)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(radioID: number) : Promise<Radio> {
        return new Promise<Radio>((resolve: any, reject: any) => {
            HTTP.delete(`api/radio/${radioID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new RadioApi();
