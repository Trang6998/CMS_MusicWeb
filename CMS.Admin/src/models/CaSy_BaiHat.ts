

import { BaiHat } from '@/models/BaiHat';
import { CaSy } from '@/models/CaSy'; 

export interface CaSy_BaiHat { 
    CaSyID: number;
    BaiHatID: number;
    VaiTro: string;
    SoThuTu: number;
    BaiHat: BaiHat;
    CaSy: CaSy;
}