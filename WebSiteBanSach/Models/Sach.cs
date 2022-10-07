namespace WebSiteBanSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ThamGias = new HashSet<ThamGia>();
        }

        [Display(Name="Mã sách")]
        [Key]
        public int MaSach { get; set; }

        [Display(Name = "Tên sách")]
        [StringLength(50)]
        public string TenSach { get; set; }

        [Display(Name = "Giá bán")]
        public decimal? GiaBan { get; set; }

        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Display(Name = "Ảnh bìa")]
        [StringLength(50)]
        public string AnhBia { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? NgayCapNhat { get; set; }

        [Display(Name = "Số lượng")]
        public int? SoLuongTon { get; set; }

        [Display(Name = "Nhà xuất bản")]
        public int? MaNXB { get; set; }

        [Display(Name = "Chủ đề")]
        public int? MaChuDe { get; set; }

        [Display(Name = "Tình trạng")]
        public int? Moi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual ChuDe ChuDe { get; set; }

        public virtual NhaXuatBan NhaXuatBan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThamGia> ThamGias { get; set; }
    }
}
