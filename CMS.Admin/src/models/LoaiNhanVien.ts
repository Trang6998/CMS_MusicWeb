

import { NhanVien } from '@/models/NhanVien'; 

export interface LoaiNhanVien { 
    LoaiNhanVienID: number;
    TenLoai: string;
    NhanViens: NhanVien[];
}