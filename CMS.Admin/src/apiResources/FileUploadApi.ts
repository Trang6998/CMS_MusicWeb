import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
var qs = require('qs');
export interface FileUploadApiSearchParams extends Pagination {

}
class FileUploadApi extends BaseApi {

    upload(formData: FormData) {
        return new Promise<any>((resolve: any, reject: any) => {
            HTTP.post(`fileupload/upload`, formData, { headers: { 'Content-Type': 'multipart/form-data' } })
                .then((response) => {
                    resolve(response.data);
                }).catch((error) => {
                    reject(error);
                })
        });
    }
}
export default new FileUploadApi();
