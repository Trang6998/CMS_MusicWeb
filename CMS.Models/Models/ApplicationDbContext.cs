namespace CMS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<AccessToken> AccessToken { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<Album_BaiHat> Album_BaiHat { get; set; }
        public virtual DbSet<BaiHat> BaiHat { get; set; }
        public virtual DbSet<BaiViet> BaiViet { get; set; }
        public virtual DbSet<CaSy> CaSy { get; set; }
        public virtual DbSet<CaSy_BaiHat> CaSy_BaiHat { get; set; }
        public virtual DbSet<ChuyenMuc> ChuyenMuc { get; set; }
        public virtual DbSet<ChuyenMuc_BaiViet> ChuyenMuc_BaiViet { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<FileUpload> FileUpload { get; set; }
        public virtual DbSet<LienHe> LienHe { get; set; }
        public virtual DbSet<LoaiNhanVien> LoaiNhanVien { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<QuanHuyen> QuanHuyen { get; set; }
        public virtual DbSet<Radio> Radio { get; set; }
        public virtual DbSet<TheLoai> TheLoai { get; set; }
        public virtual DbSet<TinhThanh> TinhThanh { get; set; }
        public virtual DbSet<XaPhuong> XaPhuong { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
