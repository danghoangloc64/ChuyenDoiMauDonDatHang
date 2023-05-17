using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuyenDoiMauDonDatHang
{
    public class DataOrderModel
    {
        public string MaVanDon { get; set; }
        public string SanPham { get; set; }
        public string PhanLoaiHang { get; set; }
        public string NgayDatHang { get; set; }
        public string TongTien { get; set; }
        public string MaDonHang { get; set; }
        public string SoLuong { get; set; }
        public string NguoiNhan { get; set; }
        public string DonViVanChuyen { get; set; }
    }

    public class SanPhamModel
    {
        public string SanPham { get; set; }
    }

    public class DataOrderReportModel
    {
        public Image ImageQRMaVanDon { get; set; }
        public List<SanPhamModel> ListSanPham { get; set; }
        public string NgayDatHang { get; set; }
        public string MaVanDon { get; set; }
        public string TongTien { get; set; }
        public string MaDonHang { get; set; }
        public string NguoiGui { get; set; }
        public string NguoiNhan { get; set; }
        public Image ImageBarCodeMaVanDon { get; set; }
        public string Note { get; set; }
        public string DonViVanChuyen { get; set; }
    }
}
