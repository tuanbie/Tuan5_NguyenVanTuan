using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan5_NguyenVanTuan.Models;

namespace Tuan5_NguyenVanTuan.Controllers
{
    public class TheLoaiController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: TheLoai
        public ActionResult Index()
        {
            var all_theloai = from tt in data.TheLoais select tt;
            return View(all_theloai);
        }
        //-------------Detail-------------------
        public ActionResult Detail(int id)
        {
            
            var D_theloai = data.TheLoais.Where(m => m.maloai == id).First();
            if (id == null)
            {
                return RedirectToAction("Sach", "ListSach");
            }
            return View(D_theloai);
        }
        //-------------Create-------------------
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, TheLoai tl)
        {
            var ten = collection["tenloai"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.tenloai = ten;
                data.TheLoais.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        //-------------Edit-------------------
        public ActionResult Edit(int id)
        {
            if(id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var E_category = data.TheLoais.First(m => m.maloai == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theloai = data.TheLoais.First(m => m.maloai == id);
            var E_tenloai = collection["tenloai"];
            theloai.maloai = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                theloai.tenloai = E_tenloai;
                UpdateModel(theloai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-------------Delete-------------------
        public ActionResult Delete(int id)
        {
            var D_theloai = data.TheLoais.First(m => m.maloai == id);
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_theloai = data.TheLoais.Where(m => m.maloai == id).First();
            data.TheLoais.DeleteOnSubmit(D_theloai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

    }
}