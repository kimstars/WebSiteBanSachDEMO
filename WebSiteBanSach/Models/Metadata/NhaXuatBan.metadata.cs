using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSiteBanSach.Models
{
        [MetadataTypeAttribute(typeof(NhaXuatBanMetadata))]
    public partial class NhaXuatBan//noi voi Class NhaXuatBan trong Models
    {
        internal sealed class NhaXuatBanMetadata
        {
            [Key]
            public int MaNXB { get; set; }

            [Display(Name = "Tên Nhà xuất bản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(50,ErrorMessage="{0} không quá 50 kí tự")]
            public string TenNXB { get; set; }

            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(200,ErrorMessage="{0} không quá 200 kí tự")]
            public string DiaChi { get; set; }

            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(50,ErrorMessage="{0} không quá 50 kí tự")]
            public string DienThoai { get; set; }
        }
    }
}