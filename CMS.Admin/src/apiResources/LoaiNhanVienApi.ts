

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { LoaiNhanVien } from '@/models/LoaiNhanVien';
export interface LoaiNhanVienApiSearchParams extends Pagination {
   keyworlds? : string
}

class LoaiNhanVienApi extends BaseApi {
    
    search(loaiNhanViensearchParams: LoaiNhanVienApiSearchParams) : Promise<PaginatedResponse<LoaiNhanVien>> {
        return new Promise<PaginatedResponse<LoaiNhanVien>>((resolve: any, reject: any) => {
            HTTP.get(`api/loaiNhanVien`, { params: loaiNhanViensearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(loaiNhanVienID: number) : Promise<LoaiNhanVien> {
        return new Promise<LoaiNhanVien>((resolve: any, reject: any) => {
            HTTP.get(`api/loaiNhanVien/${loaiNhanVienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(loaiNhanVien: LoaiNhanVien) : Promise<LoaiNhanVien> {
        return new Promise<LoaiNhanVien>((resolve: any, reject: any) => {
            HTTP.post(`api/loaiNhanVien`,loaiNhanVien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(loaiNhanVienID: number, loaiNhanVien: LoaiNhanVien) : Promise<LoaiNhanVien> {
        return new Promise<LoaiNhanVien>((resolve: any, reject: any) => {
            HTTP.put(`api/loaiNhanVien/${loaiNhanVienID}`,loaiNhanVien)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(loaiNhanVienID: number) : Promise<LoaiNhanVien> {
        return new Promise<LoaiNhanVien>((resolve: any, reject: any) => {
            HTTP.delete(`api/loaiNhanVien/${loaiNhanVienID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new LoaiNhanVienApi();
