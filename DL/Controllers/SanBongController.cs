using DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;

namespace DL.Controllers
{
    public class SanBongController : Controller
    {
        /*private const string CHUA_BAT_DAU_DA = "Chưa bắt đầu đá";
        private const string DANG_DA = "Đang đá";
        private const string DA_DA_XONG = "Đã đá xong";*/
        /*public ActionResult Login(string TaiKhoanUser, string MatKhauUser)
        {
            if(ModelState.IsValid)
            {
                var f_password = GetMD5(MatKhauUser);
                var data = Model.
            }
        }*/
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult NguoiQuanLi()
        {
            DBSanBong db = new DBSanBong();
            List<ThongTinSan> DanhSachSan = db.ThongTinSans.ToList();
            return View(DanhSachSan);
        }
        public ActionResult NguoiThue()
        {
            DBSanBong db = new DBSanBong();
            List<ThongTinSan> DanhSachSan = db.ThongTinSans.ToList();
            return View(DanhSachSan);
        }
        
        [HttpGet]
        public ActionResult MakeInvoice()
        {
            DBSanBong db = new DBSanBong();
            List<HoaDon> hoaDon = db.HoaDons.ToList();
            return View(hoaDon);
        }
        [HttpPost]
        public ActionResult MakeInvoice(HoaDon hoaDon)
        {
            return View(hoaDon);
        }

        
        [HttpPost]
        public ActionResult SuaThongTin(int SoSan)
        {
            DBSanBong db = new DBSanBong();
            ThongTinSan model = db.ThongTinSans.SingleOrDefault(m => m.MaSan == SoSan);
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaThongTin(ThongTinSan model)
        {
            DBSanBong db = new DBSanBong();
            var updateModel = db.ThongTinSans.Find(model.MaSan);
            updateModel.MaSan = model.MaSan;
            updateModel.TinhTrang = model.TinhTrang;
            updateModel.GiaThue60Phut = model.GiaThue60Phut;
            updateModel.GiaThue90phut = model.GiaThue90phut;
            db.ThongTinSans.Add(updateModel);
            db.SaveChanges();
            return RedirectToAction("NguoiQuanLi");
        }

        [HttpPost]
        public ActionResult DongTatCaSan()
        {
            using (DBSanBong db = new DBSanBong())
            {
                List<ThongTinSan> danhSachSan = db.ThongTinSans.ToList();
                foreach (var san in danhSachSan)
                {
                    san.TinhTrang = "Đóng Cửa";
                }
                db.SaveChanges();
            }
            return RedirectToAction("NguoiQuanLi");
        }
        [HttpPost]
        public ActionResult MoChoThueSan()
        {
            using (DBSanBong db = new DBSanBong())
            {
                List<ThongTinSan> danhSachSan = db.ThongTinSans.ToList();
                foreach (var san in danhSachSan)
                {
                    san.TinhTrang = "Trống";
                }
                db.SaveChanges();
            }
            return RedirectToAction("NguoiQuanLi");
        }
        [HttpGet]
        public ActionResult ThueSan()
        {
            var model = new ThongTinSan();
            return View(model);
        }

        [HttpPost]
        public ActionResult ThueSan(ThongTinSan thueSanModel)
        {
            if (ModelState.IsValid)
            {
                using (DBSanBong db = new DBSanBong())
                {
                    ThongTinSan san = db.ThongTinSans.SingleOrDefault(m => m.MaSan == thueSanModel.MaSan);

                    san.TinhTrang = "Đã thuê";
                    db.SaveChanges();
                }
                return RedirectToAction("NguoiThue");
            }

            return View(thueSanModel);
        }
        
        // GET: HoaDon
        private DBSanBong _dbContext = new DBSanBong();
        [HttpGet]
        public ActionResult Index()
        {
            var hoaDons = _dbContext.HoaDons.ToList();
            foreach (var hoaDon in hoaDons)
            {
                if (hoaDon.GioBatDau > DateTime.Now)
                {
                    hoaDon.TinhTrangHD = "Chưa bắt đầu đá";
                }
                else if (hoaDon.GioBatDau <= DateTime.Now && DateTime.Now <= hoaDon.GioKetThuc)
                {
                    hoaDon.TinhTrangHD = "Đang đá";
                }
                else if (DateTime.Now > hoaDon.GioKetThuc)
                {
                    hoaDon.TinhTrangHD = "Đã đá xong";
                }
            }
            _dbContext.SaveChanges();

            return View(hoaDons);
        }
        [HttpPost]
        public ActionResult HienThiHoaDon(string MaHD)
        {
            var hoaDon = _dbContext.HoaDons.Find(MaHD);
            return View(hoaDon);
        }

        public ActionResult ChinhSuaHoaDon(int id)
        {
            var hoaDon = _dbContext.HoaDons.Find(id);
            if (hoaDon.GioKetThuc >= DateTime.Now)
            {
                return View(hoaDon);
            }

            // Nếu thời gian đã quá khoảng thời gian thuê, không cho phép chỉnh sửa
            return RedirectToAction("NguoiQuanLi", new { id});
        }

        public ActionResult ChinhSuaHoaDon(int id, HoaDon hoaDon)
        {
            var hoaDonDaThue = _dbContext.HoaDons.Find(id);
            if (hoaDonDaThue.GioKetThuc >= DateTime.Now)
            {
                hoaDonDaThue.loaiSan = hoaDon.loaiSan;
                hoaDonDaThue.GioBatDau = hoaDon.GioBatDau;
                hoaDonDaThue.GioKetThuc = hoaDon.GioKetThuc;

                _dbContext.SaveChanges();
            }
            return RedirectToAction("NguoiQuanLi");
        }

        public ActionResult ChapNhanHoaDon(int id)
        {
            var hoaDon = _dbContext.HoaDons.Find(id);
            hoaDon.TinhTrangHD  = "Chấp nhận";
            _dbContext.HoaDons.Add(hoaDon);
            _dbContext.SaveChanges();

            return RedirectToAction("NguoiQuanLi");
        }

        public ActionResult HuyDon(int id)
        {
            var hoaDon = _dbContext.HoaDons.Find(id);
            hoaDon.TinhTrangHD = "Hủy";
            _dbContext.SaveChanges();

            return RedirectToAction("NguoiQuanLi");
        }
    }

}

