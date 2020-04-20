

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { ChuyenMuc_BaiViet } from '@/models/ChuyenMuc_BaiViet';
export interface ChuyenMuc_BaiVietApiSearchParams extends Pagination {
   keyworlds? : string
}

class ChuyenMuc_BaiVietApi extends BaseApi {
    
    search(chuyenMuc_BaiVietsearchParams: ChuyenMuC_BaiVietApiSearchParams) : Promise<PaginatedResponse<ChuyenMuC_BaiViet>> {
        return new Promise<PaginatedResponse<ChuyenMuC_BaiViet>>((resolve: any, reject: any) => {
            HTTP.get(`api/chuyenMuc_BaiViet`, { params: chuyenMuc_BaiVietsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(chuyenMuc_BaiViet: ChuyenMuc_BaiViet) : Promise<ChuyenMuC_BaiViet> {
        return new Promise<ChuyenMuC_BaiViet>((resolve: any, reject: any) => {
            HTTP.post(`api/chuyenMuc_BaiViet`,chuyenMuc_BaiViet)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new ChuyenMuc_BaiVietApi();
