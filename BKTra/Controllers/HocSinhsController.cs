using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using BKTra.Models;

namespace BKTra.Controllers
{
    public class HocSinhsController : Controller
    {
        private HocSinhsDB db = new HocSinhsDB();

        // GET: HocSinhs
        public ActionResult Index(string search, string sortOrder)
        {
            //ViewBag.SapTen = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SapTen = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.SapDiem = sortOrder == "diemthi" ? "diemthi_desc" : "diemthi";
            var hocSinhs = db.HocSinhs.Select(p => p);
            if (!String.IsNullOrEmpty(search))
            {
                hocSinhs = hocSinhs.Where(p => p.sbd.Contains(search) || p.hoten.Contains(search));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    hocSinhs = hocSinhs.OrderByDescending(p => p.hoten);
                    break;
                case "diemthi":
                    hocSinhs = hocSinhs.OrderByDescending(p => p.diemthi);
                    break;
                case "diemthi_desc":
                    hocSinhs = hocSinhs.OrderBy(p => p.diemthi);
                    break;
                default:
                    hocSinhs = hocSinhs.OrderBy(p => p.hoten);
                    break;
            }
            return View(hocSinhs.ToList());
        }

        //public ActionResult Index(string search, string sortOrder)
        //{
        //    ViewBag.SapTen = sortOrder == "name" ? "name_desc" : "name";
        //    ViewBag.SapDiem = sortOrder == "diemthi" ? "diemthi_desc" : "diemthi";

        //    var hocSinhs = db.HocSinhs.Select(p => p);

        //    if (!String.IsNullOrEmpty(search))
        //    {
        //        hocSinhs = hocSinhs.Where(p => p.sbd.Contains(search) || p.hoten.Contains(search));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            hocSinhs = hocSinhs.OrderByDescending(p => p.hoten);
        //            break;
        //        case "name":
        //            hocSinhs = hocSinhs.OrderBy(p => p.hoten);
        //            break;
        //        case "diemthi":
        //            hocSinhs = hocSinhs.OrderBy(p => p.diemthi);
        //            break;
        //        case "diemthi_desc":
        //            hocSinhs = hocSinhs.OrderByDescending(p => p.diemthi);
        //            break;
        //        default:
        //            hocSinhs = hocSinhs.OrderByDescending(p => p.hoten);
        //            break;
        //    }

        //    return View(hocSinhs.ToList());
        //}


        // GET: HocSinhs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // GET: HocSinhs/Create
        public ActionResult Create()
        {
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop");
            return View();
        }

        // POST: HocSinhs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HocSinh hocSinh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files["ImageFile"] != null && Request.Files["ImageFile"].ContentLength > 0)
                    {
                        var file = Request.Files["ImageFile"];
                        string fileName = System.IO.Path.GetFileName(file.FileName);
                        string uploadPath = Server.MapPath("~/Anh/Images/") + fileName;
                        file.SaveAs(uploadPath);
                        hocSinh.anhduthi = fileName;
                    }

                    db.HocSinhs.Add(hocSinh);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu: " + ex.Message;
            }
            return View(hocSinh);
        }


        // GET: HocSinhs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hocSinh.malop);
            return View(hocSinh);
        }

        // POST: HocSinhs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sbd,hoten,anhduthi,malop,diemthi")] HocSinh hocSinh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files["ImageFile"] != null && Request.Files["ImageFile"].ContentLength > 0)
                    {
                        var file = Request.Files["ImageFile"];
                        string fileName = System.IO.Path.GetFileName(file.FileName);
                        string uploadPath = Server.MapPath("~/Anh/Images/") + fileName;
                        file.SaveAs(uploadPath);
                        hocSinh.anhduthi = fileName;
                    }
                    db.Entry(hocSinh).State = EntityState.Modified;
                    db.SaveChanges();  
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu: " + ex.Message;
            }
            return View(hocSinh);
        }

        // GET: HocSinhs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: HocSinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HocSinh hocSinh = db.HocSinhs.Find(id);
            db.HocSinhs.Remove(hocSinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: HocSinhs/Xoadulieu/5
        public ActionResult Xoadulieu(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh == null)
            {
                return HttpNotFound();
            }
            return View(hocSinh);
        }

        // POST: HocSinhs/Xoadulieu/5
        [HttpPost, ActionName("Xoadulieu")]
        [ValidateAntiForgeryToken]
        public ActionResult XoadulieuConfirmed(string id)
        {
            HocSinh hocSinh = db.HocSinhs.Find(id);
            if (hocSinh != null)
            {
                db.HocSinhs.Remove(hocSinh);
                db.SaveChanges();
                TempData["Message"] = "Xóa thành công!";
            }
            else
            {
                TempData["Error"] = "Xóa thất bại!";
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
