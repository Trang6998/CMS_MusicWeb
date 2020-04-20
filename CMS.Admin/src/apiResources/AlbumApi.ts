

import { HTTP } from '@/HTTPServices';
import { BaseApi } from '@/apiResources/BaseApi';
import { PaginatedResponse, Pagination } from '@/apiResources/PaginatedResponse';
import { Album } from '@/models/Album';
export interface AlbumApiSearchParams extends Pagination {
   keyworlds? : string
}

class AlbumApi extends BaseApi {
    
    search(albumsearchParams: AlbumApiSearchParams) : Promise<PaginatedResponse<Album>> {
        return new Promise<PaginatedResponse<Album>>((resolve: any, reject: any) => {
            HTTP.get(`api/album`, { params: albumsearchParams })
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    get(albumID: number) : Promise<Album> {
        return new Promise<Album>((resolve: any, reject: any) => {
            HTTP.get(`api/album/${albumID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    insert(album: Album) : Promise<Album> {
        return new Promise<Album>((resolve: any, reject: any) => {
            HTTP.post(`api/album`,album)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    update(albumID: number, album: Album) : Promise<Album> {
        return new Promise<Album>((resolve: any, reject: any) => {
            HTTP.put(`api/album/${albumID}`,album)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
    delete(albumID: number) : Promise<Album> {
        return new Promise<Album>((resolve: any, reject: any) => {
            HTTP.delete(`api/album/${albumID}`)
            .then((response) => {
                resolve(response.data);
            }).catch((error) => {
                reject(error);
            })
        });
    }
}
export default new AlbumApi();
