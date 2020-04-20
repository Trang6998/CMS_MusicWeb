

import { QuanHuyen } from '@/models/QuanHuyen'; 

export interface TinhThanh { 
    Id: number;
    TenTinhThanh: string;
    QuanHuyens: QuanHuyen[];
}