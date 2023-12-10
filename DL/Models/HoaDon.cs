namespace DL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {

        [Key]
        [StringLength(20)]
        public string MaHD { get; set; }

        [StringLength(10)]
        public string TaiKhoanUser { get; set; }

        [StringLength(10)]
        public string loaiSan { get; set; }

        [StringLength(50)]
        public string TinhTien { get; set; }

        public int? MaSan { get; set; }

        [StringLength(50)]
        public string TinhTrangHD { get; set; }

        public DateTime? GioBatDau { get; set; }

        public DateTime? GioKetThuc { get; set; }

        public virtual ThongTinSan ThongTinSan { get; set; }

        public virtual TaiKhoanUser TaiKhoanUser1 { get; set; }
    }
}
