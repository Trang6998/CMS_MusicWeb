

import { NhanVien } from '@/models/NhanVien'; 

export interface User { 
    UserId: number;
    UserName: string;
    Email: string;
    PasswordSalt: string;
    Password: string;
    CreatedTime: Date;
    LastLogin: Date;
    Lang: string;
    Active: boolean;
    NhanVienID: number;
    NhanVien: NhanVien;
}