using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanSach.Models
{
    public class GioHang
    {
        QuanLyBanSachModel db = new QuanLyBanSachModel();
        public int _MaSach { get; set; }
        public string _TenSach { get; set; }
        public string _AnhBia { get; set; }
        public double _DonGia { get; set; }
        public int _SoLuong { get; set; }
        public double _ThanhTien
        {
            get { return _SoLuong * _DonGia; }
        }
        public GioHang(int __MaSach)
        {
            _MaSach = __MaSach;
            Sach sach = db.Saches.Single(n => n.MaSach == _MaSach);
            _TenSach = sach.TenSach;
            _AnhBia = sach.AnhBia;
            _DonGia = Convert.ToDouble(sach.GiaBan);
            _SoLuong = 1;
        }
    }
}