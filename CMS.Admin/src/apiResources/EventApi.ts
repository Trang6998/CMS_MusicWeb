

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { Event } from '@/models/Event';
export interface EventApiSearchParams extends Pagination {
   keyworlds? : string
}

class EventApi extends BaseApi {
    
    search(suKiensearchParams: EventApiSearchParams) : Promise<PaginatedResponse<Event>> {
        return new Promise<PaginatedResponse<Event>>((resolve: any, reject: any) => {
            HTTP.get(`api/suKien`, { params: suKiensearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(suKienID: number) : Promise<Event> {
        return new Promise<Event>((resolve: any, reject: any) => {
            HTTP.get(`api/suKien/${suKienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(suKien: Event) : Promise<Event> {
        return new Promise<Event>((resolve: any, reject: any) => {
            HTTP.post(`api/suKien`,suKien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(suKienID: number, suKien: Event) : Promise<Event> {
        return new Promise<Event>((resolve: any, reject: any) => {
            HTTP.put(`api/suKien/${suKienID}`,suKien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(suKienID: number) : Promise<Event> {
        return new Promise<Event>((resolve: any, reject: any) => {
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
