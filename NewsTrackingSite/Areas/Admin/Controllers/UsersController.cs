using NewsTrackingSite.Models;
using NewsTrackingSite.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private NewsTrackingDB db = new NewsTrackingDB();

        // GET: Admin/Users
        public ActionResult Index(int? page, string sorting)
        {
            var userList = db.User.ToList();


            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "LNameAsc";
            }
            ViewBag.RegisterDateSortParam = sorting == "RegisterDateAsc" ? "RegisterDateDesc" : "RegisterDateAsc";
            ViewBag.LNameSortParam = sorting == "LNameDesc" ? "LNameAsc" : "LNameDesc";

            switch (sorting)
            {
                case "RegisterDateAsc":
                    userList = userList.OrderBy(m => m.RegisterDate).ToList();
                    ViewBag.CurrentSortParam = "RegisterDateDesc";
                    ViewBag.CurrentDDLText = "Register Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "RegisterDateDesc":
                    userList = userList.OrderByDescending(m => m.RegisterDate).ToList();
                    ViewBag.CurrentSortParam = "RegisterDateAsc";
                    ViewBag.CurrentDDLText = "Register Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                case "LNameAsc":
                    userList = userList.OrderBy(m => m.LName).ToList();
                    ViewBag.CurrentSortParam = "LNameDesc";
                    ViewBag.CurrentDDLText = "Last Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "LNameDesc":
                    userList = userList.OrderByDescending(m => m.LName).ToList();
                    ViewBag.CurrentSortParam = "LNameAsc";
                    ViewBag.CurrentDDLText = "Last Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    userList = userList.OrderBy(m => m.LName).ToList();
                    ViewBag.CurrentSortParam = "LNameDesc";
                    ViewBag.CurrentDDLText = "Last Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View("Index", userList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Users/Details?userID=id
        public ActionResult Details(int? userID)
        {
            if (userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            User md = new User();
            return View(md);
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit?userID=id
        public ActionResult Edit(int? userID)
        {
            if (userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit?userID=id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete?userID=id
        public ActionResult Delete(int? userID)
        {
            if (userID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            db.User.Remove(user);
            db.SaveChanges();
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
