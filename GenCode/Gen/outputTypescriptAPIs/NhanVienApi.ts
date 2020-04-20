

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { NhanVien } from '@/models/NhanVien';
export interface NhanVienApiSearchParams extends Pagination {
   keyworlds? : string
}

class NhanVienApi extends BaseApi {
    
    search(nhanViensearchParams: NhaNVieNApiSearchParams) : Promise<PaginatedResponse<NhaNVieN>> {
        return new Promise<PaginatedResponse<NhaNVieN>>((resolve: any, reject: any) => {
            HTTP.get(`api/nhanVien`, { params: nhanViensearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(nhanVienID: number) : Promise<NhaNVieN> {
        return new Promise<NhaNVieN>((resolve: any, reject: any) => {
            HTTP.get(`api/nhanVien/${nhanVienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(nhanVien: NhanVien) : Promise<NhaNVieN> {
        return new Promise<NhaNVieN>((resolve: any, reject: any) => {
            HTTP.post(`api/nhanVien`,nhanVien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(nhanVienID: number, nhanVien: NhanVien) : Promise<NhaNVieN> {
        return new Promise<NhaNVieN>((resolve: any, reject: any) => {
            HTTP.put(`api/nhanVien/${nhanVienID}`,nhanVien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(nhanVienID: number) : Promise<NhaNVieN> {
        return new Promise<NhaNVieN>((resolve: any, reject: any) => {
            HTTP.delete(`api/nhanVien/${nhanVienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new NhanVienApi();
