

import { Album_BaiHat } from '@/models/Album_BaiHat'; 

export interface Album { 
    AlbumID: number;
    TenAlbum: string;
    AnhDaiDien: string;
    TrangThai: boolean;
    Album_BaiHat: Album_BaiHat[];
}