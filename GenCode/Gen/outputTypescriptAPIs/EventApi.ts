

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { Event } from '@/models/Event';
export interface EventApiSearchParams extends Pagination {
   keyworlds? : string
}

class EventApi extends BaseApi {
    
    search(suKiensearchParams: SuKienApiSearchParams) : Promise<PaginatedResponse<SuKien>> {
        return new Promise<PaginatedResponse<SuKien>>((resolve: any, reject: any) => {
            HTTP.get(`api/suKien`, { params: suKiensearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(suKienID: number) : Promise<SuKien> {
        return new Promise<SuKien>((resolve: any, reject: any) => {
            HTTP.get(`api/suKien/${suKienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(suKien: Event) : Promise<SuKien> {
        return new Promise<SuKien>((resolve: any, reject: any) => {
            HTTP.post(`api/suKien`,suKien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(suKienID: number, suKien: Event) : Promise<SuKien> {
        return new Promise<SuKien>((resolve: any, reject: any) => {
            HTTP.put(`api/suKien/${suKienID}`,suKien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(suKienID: number) : Promise<SuKien> {
        return new Promise<SuKien>((resolve: any, reject: any) => {
            HTTP.delete(`api/suKien/${suKienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new EventApi();
