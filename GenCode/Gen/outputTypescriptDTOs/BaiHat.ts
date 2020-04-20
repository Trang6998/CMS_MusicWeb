

import { Album_BaiHat } from '@/models/Album_BaiHat';
import { TheLoai } from '@/models/TheLoai';
import { CaSy_BaiHat } from '@/models/CaSy_BaiHat'; 

export interface BaiHat { 
    BaiHatID: number;
    TenBaiHat: string;
    LinkFileNhac: string;
    AnhDaiDien: string;
    TieuDe: string;
    NgayDang: Date;
    TheLoaiID: number;
    HienThiTrangChu: boolean;
    Album_BaiHat: Album_BaiHat[];
    TheLoai: TheLoai;
    CaSy_BaiHat: CaSy_BaiHat[];
}