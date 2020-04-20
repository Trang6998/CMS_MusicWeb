

import { NhanVien } from '@/models/NhanVien'; 

export interface Radio { 
    RadioID: number;
    TieuDe: string;
    NoiDung: string;
    AnhDaiDien: string;
    LinkFile: string;
    NgayDang: Date;
    LuotXem: number;
    LuotThich: number;
    NhanVienID: number;
    TrangThai: boolean;
    NhanVien: NhanVien;
}