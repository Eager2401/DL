namespace DL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoanAd")]
    public partial class TaiKhoanAd
    {
        [Key]
        [StringLength(10)]
        public string TaiKhoanAdmin { get; set; }

        [StringLength(10)]
        public string MatKhauAdmin { get; set; }
    }
}
