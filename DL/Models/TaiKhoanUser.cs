namespace DL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoanUser")]
    public partial class TaiKhoanUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoanUser()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [Column("TaiKhoanUser")]
        [StringLength(10)]
        public string TaiKhoanUser1 { get; set; }

        [StringLength(10)]
        public string MatKhauUser { get; set; }

        public int? Tien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
