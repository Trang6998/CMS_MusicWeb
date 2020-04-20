

import { CaSy_BaiHat } from '@/models/CaSy_BaiHat'; 

export interface CaSy { 
    CaSyID: number;
    HoTen: string;
    BietDanh: string;
    AnhDaiDien: string;
    NgaySinh: Date;
    SoDienThoai: string;
    DiaChi: string;
    Gmail: string;
    FaceBook: string;
    Instagram: string;
    SoThich: string;
    TinhCach: string;
    TieuSu: string;
    XaPhuongID: number;
    QuanHuyenID: number;
    TinhThanhID: number;
    CaSy_BaiHat: CaSy_BaiHat[];
}