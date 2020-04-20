

import { TinhThanh } from '@/models/TinhThanh';
import { XaPhuong } from '@/models/XaPhuong'; 

export interface QuanHuyen { 
    Id: number;
    TenQuanHuyen: string;
    TinhThanhId: number;
    TinhThanh: TinhThanh;
    XaPhuongs: XaPhuong[];
}