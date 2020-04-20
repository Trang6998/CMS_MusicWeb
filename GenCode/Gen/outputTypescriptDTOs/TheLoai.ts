

import { BaiHat } from '@/models/BaiHat'; 

export interface TheLoai { 
    TheLoaiID: number;
    TenTheLoai: string;
    GioiThieu: string;
    BaiHats: BaiHat[];
}