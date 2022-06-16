using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan5_NguyenVanTuan.Models;

namespace Tuan5_NguyenVanTuan.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        MyDataDataContext data = new MyDataDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(n => n.masach == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }

        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {

            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.masach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        //[HttpGet]
        //public ActionResult DatHang()
        //{
        //    if (Session["Giohang"] == null)
        //    {
        //        return RedirectToAction("Index", "Fashion");
        //    }
        //    List<Giohang> gioHangs = Laygiohang();
        //    ViewBag.TongSoLuong = TongSoLuong();
        //    ViewBag.TongTien = TongTien();

        //    return View(gioHangs);
        //}
        //[HttpPost]
        //public ActionResult DatHang(FormCollection collection)
        //{
        //    DonHang dONDATHANG = new DonHang();
        //    ChiTietDonHang CTDH = new ChiTietDonHang();
        //    KhachHang kh = (KhachHang)Session["Taikhoan"];
        //    List<Giohang> gioHangs = Laygiohang();
        //    dONDATHANG.makh = kh.makh;
        //    dONDATHANG.ngaydat = DateTime.Now;
        //    dONDATHANG.ngaygiao = DateTime.Parse(ngaygiao);
        //    dONDATHANG.thanhtoan = .Parse(TongTien().ToString());
        //    dONDATHANG.Tinhtranggiaohang = false;
        //    dONDATHANG.Dathanhtoan = false;
        //    data.DONDATHANGs.InsertOnSubmit(dONDATHANG);
        //    data.SubmitChanges();
        //    foreach (var item in gioHangs)
        //    {
        //        CHITIETDONTHANG CT = new CHITIETDONTHANG();
        //        //DONDATHANG dONDATHANG = new DONDATHANG();
        //        CT.MaDonHang = dONDATHANG.MaDonHang;
        //        CT.MaSP = item.masp;
        //        CT.Soluong = item.soluong;
        //        CT.Dongia = (decimal)item.dongia;
        //        CT.ThanhTien = (decimal)item.thanhtien;
        //        dONDATHANG.DiaChi = DiaChi;
        //        data.CHITIETDONTHANGs.InsertOnSubmit(CT);
        //    }
        //    data.SubmitChanges();          
        //    Session["Giohang"] = null;
        //    return RedirectToAction("XacNhanDonHang", "Giohang");
        
    }
}