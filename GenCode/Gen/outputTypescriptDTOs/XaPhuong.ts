

import { QuanHuyen } from '@/models/QuanHuyen'; 

export interface XaPhuong { 
    Id: number;
    QuanHuyenId: number;
    TenXaPhuong: string;
    QuanHuyen: QuanHuyen;
}