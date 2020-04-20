

import { NhanVien } from '@/models/NhanVien'; 

export interface Event { 
    EventID: number;
    TieuDe: string;
    NoiDung: string;
    AnhDaiDien: string;
    DiaDiem: string;
    ThoiGianTu: Date;
    ThoiGianDen: Date;
    NhanVienID: number;
    XaPhuongID: number;
    QuanHuyenID: number;
    TinhThanhID: number;
    NhanVien: NhanVien;
}