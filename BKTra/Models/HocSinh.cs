namespace BKTra.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocSinh")]
    public partial class HocSinh
    {
        [Key]
        [Required(ErrorMessage ="Số báo danh không được để trống")]
        [DisplayName("Số báo danh")]
        [StringLength(10)]
        public string sbd { get; set; }

        [DisplayName("Họ Tên")]
        [StringLength(50)]
        public string hoten { get; set; }

        [DisplayName("Ảnh dự thi")]
        [StringLength(50)]
        public string anhduthi { get; set; }

        [DisplayName("Mã lớp")]
        public int? malop { get; set; }

        [DisplayName("Điểm thi")]
        public double? diemthi { get; set; }

        public virtual LopHoc LopHoc { get; set; }
    }
}
