using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Dao;

namespace WebSiteBanSach.Models.Metadata
{
    public class DataConnect
    {
        public virtual List<ChiTietDonHang> ChiTietDonHangs { get { return APIHelper.SendGetRequest<List<ChiTietDonHang>>("chi-tiet-don-hang?number"); } }

        public virtual List<ChuDe> ChuDes { get { return APIHelper.SendGetRequest<List<ChuDe>>("chu-de?number"); } }

        public virtual List<DonHang> DonHangs { get { return APIHelper.SendGetRequest<List<DonHang>>("don-hang?number"); } }

        public virtual List<KhachHang> KhachHangs { get { return APIHelper.SendGetRequest<List<KhachHang>>("khach-hang?number"); } }

        public virtual List<NhaXuatBan> NhaXuatBans { get { return APIHelper.SendGetRequest<List<NhaXuatBan>>("nha-xuat-ban?number"); } }

        public virtual List<Sach> Saches { get { return APIHelper.SendGetRequest<List<Sach>>("sach?number"); } }

        public virtual List<TacGia> TacGias { get { return APIHelper.SendGetRequest<List<TacGia>>("tac-gia?number"); } }
    }
}