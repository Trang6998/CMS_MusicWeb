

import { NhanVien } from '@/models/NhanVien';
import { ChuyenMuc_BaiViet } from '@/models/ChuyenMuc_BaiViet'; 

export interface BaiViet { 
    BaiVietID: number;
    TieuDe: string;
    NoiDung: string;
    AnhDaiDien: string;
    TrangThai: boolean;
    NgayDang: Date;
    NhanVienID: number;
    LuotXem: number;
    LuotThich: number;
    MoTaNgan: string;
    NhanVien: NhanVien;
    ChuyenMuc_BaiViet: ChuyenMuc_BaiViet[];
}