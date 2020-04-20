

import { AccessToken } from '@/models/AccessToken';
import { User } from '@/models/User';
import { Album } from '@/models/Album';
import { Album_BaiHat } from '@/models/Album_BaiHat';
import { BaiHat } from '@/models/BaiHat';
import { BaiViet } from '@/models/BaiViet';
import { CaSy } from '@/models/CaSy';
import { CaSy_BaiHat } from '@/models/CaSy_BaiHat';
import { ChuyenMuc } from '@/models/ChuyenMuc';
import { ChuyenMuc_BaiViet } from '@/models/ChuyenMuc_BaiViet';
import { Event } from '@/models/Event';
import { FileUpload } from '@/models/FileUpload';
import { LienHe } from '@/models/LienHe';
import { LoaiNhanVien } from '@/models/LoaiNhanVien';
import { NhanVien } from '@/models/NhanVien';
import { QuanHuyen } from '@/models/QuanHuyen';
import { Radio } from '@/models/Radio';
import { TheLoai } from '@/models/TheLoai';
import { TinhThanh } from '@/models/TinhThanh';
import { XaPhuong } from '@/models/XaPhuong'; 

export interface ApplicationDbContext { 
    AccessToken: AccessToken[];
    User: User[];
    Album: Album[];
    Album_BaiHat: Album_BaiHat[];
    BaiHat: BaiHat[];
    BaiViet: BaiViet[];
    CaSy: CaSy[];
    CaSy_BaiHat: CaSy_BaiHat[];
    ChuyenMuc: ChuyenMuc[];
    ChuyenMuc_BaiViet: ChuyenMuc_BaiViet[];
    Event: Event[];
    FileUpload: FileUpload[];
    LienHe: LienHe[];
    LoaiNhanVien: LoaiNhanVien[];
    NhanVien: NhanVien[];
    QuanHuyen: QuanHuyen[];
    Radio: Radio[];
    TheLoai: TheLoai[];
    TinhThanh: TinhThanh[];
    XaPhuong: XaPhuong[];
}