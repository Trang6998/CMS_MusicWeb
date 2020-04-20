

import { BaiViet } from '@/models/BaiViet';
import { Event } from '@/models/Event';
import { LoaiNhanVien } from '@/models/LoaiNhanVien';
import { Radio } from '@/models/Radio';
import { User } from '@/models/User'; 

export interface NhanVien { 
    NhanVienID: number;
    HoTen: string;
    SoDienThoai: string;
    DiaChi: string;
    Facebook: string;
    Zalo: string;
    Instagram: string;
    AnhDaiDien: string;
    GioiThieu: string;
    LoaiNhanVienID: number;
    XaPhuongID: number;
    QuanHuyenID: number;
    TinhThanhID: number;
    BaiViets: BaiViet[];
    Events: Event[];
    LoaiNhanVien: LoaiNhanVien;
    Radios: Radio[];
    Users: User[];
}