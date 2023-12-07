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
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult NguoiQuanLi()
        {
            QuanLiSanBongEntities db = new QuanLiSanBongEntities();
            List<ThongTinSan> DanhSachSan = db.ThongTinSans.ToList();
            return View(DanhSachSan);
        }
        public ActionResult NguoiThue()
        {
            QuanLiSanBongEntities db = new QuanLiSanBongEntities();
            List<ThongTinSan> DanhSachSan = db.ThongTinSans.ToList();
            return View(DanhSachSan);
        }
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(ThongTinSan model)
        {
            QuanLiSanBongEntities db = new QuanLiSanBongEntities();
            db.ThongTinSans.Add(model);
            db.SaveChanges();
            return RedirectToAction("NguoiQuanLi");
        }
        public ActionResult SuaThongTin(int SoSan)
        {
            QuanLiSanBongEntities db = new QuanLiSanBongEntities();
            ThongTinSan model = db.ThongTinSans.SingleOrDefault(m => m.SoSan == SoSan);
            return View(model);
        }
        [HttpPost]
        public ActionResult SuaThongTin(ThongTinSan model)
        {
            QuanLiSanBongEntities db = new QuanLiSanBongEntities();
            var updateModel = db.ThongTinSans.Find(model.SoSan);
            updateModel.SoSan = model.SoSan;
            updateModel.TinhTrang = model.TinhTrang;
            updateModel.GiaThue60Phut = model.GiaThue60Phut;
            updateModel.GiaThue90phut = model.GiaThue90phut;
            db.SaveChanges();
            return RedirectToAction("NguoiQuanLi");
        }
        public ActionResult XoaSan(int SoSan)
        {
            QuanLiSanBongEntities db = new QuanLiSanBongEntities();
            var updateModel = db.ThongTinSans.Find(SoSan);
            db.ThongTinSans.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("NguoiQuanLi");
        }
        [HttpPost]
        public ActionResult DongTatCaSan()
        {
            using (QuanLiSanBongEntities db = new QuanLiSanBongEntities())
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
            using (QuanLiSanBongEntities db = new QuanLiSanBongEntities())
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
                using (QuanLiSanBongEntities db = new QuanLiSanBongEntities())
                {
                    ThongTinSan san = db.ThongTinSans.SingleOrDefault(m => m.SoSan == thueSanModel.SoSan);

                    if (san != null)
                    {
                        san.TinhTrang = "Đã thuê";
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("NguoiThue");
            }

            return View(thueSanModel);
        }


    }
}
