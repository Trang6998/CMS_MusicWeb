

import { ChuyenMuc_BaiViet } from '@/models/ChuyenMuc_BaiViet'; 

export interface ChuyenMuc { 
    ChuyenMucID: number;
    TenChuyenMuc: string;
    TrangThai: boolean;
    HienThiTrangChu: boolean;
    ChuyenMuc_BaiViet: ChuyenMuc_BaiViet[];
}