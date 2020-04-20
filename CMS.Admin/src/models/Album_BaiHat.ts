

import { Album } from '@/models/Album';
import { BaiHat } from '@/models/BaiHat'; 

export interface Album_BaiHat { 
    AlbumID: number;
    BaiHatID: number;
    SoThuTu: number;
    Album: Album;
    BaiHat: BaiHat;
}