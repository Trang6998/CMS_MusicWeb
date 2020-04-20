

import { BaiViet } from '@/models/BaiViet';
import { ChuyenMuc } from '@/models/ChuyenMuc'; 

export interface ChuyenMuc_BaiViet { 
    ChuyenMucID: number;
    BaiVietID: number;
    TenChuyenMuc: string;
    BaiViet: BaiViet;
    ChuyenMuc: ChuyenMuc;
}