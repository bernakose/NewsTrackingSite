using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsTrackingSite.Repositories;
using NewsTrackingSite.Models;

namespace NewsTrackingSite.Areas.Admin.Controllers
{
    public class GenresController : Controller
    {
        private NewsTrackingDB db = new NewsTrackingDB();

        // GET: Admin/Genres
        public ActionResult Index(int? page, string sorting)
        {
            var genreList = db.Genre.ToList();


            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "GenreNameDesc";
            }
            ViewBag.GenreNameSortParam = sorting == "GenreNameAsc" ? "GenreNameDesc" : "GenreNameAsc";


            switch (sorting)
            {
                case "GenreNameAsc":
                    genreList = genreList.OrderBy(m => m.GenreName).ToList();
                    ViewBag.CurrentSortParam = "GenreNameDesc";
                    ViewBag.CurrentDDLText = "Genre Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "GenreNameDesc":
                    genreList = genreList.OrderByDescending(m => m.GenreName).ToList();
                    ViewBag.CurrentSortParam = "GenreNameAsc";
                    ViewBag.CurrentDDLText = "Genre Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    genreList = genreList.OrderByDescending(m => m.GenreName).ToList();
                    ViewBag.CurrentSortParam = "GenreNameAsc";
                    ViewBag.CurrentDDLText = "Genre Name";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View("Index", genreList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Genres/Details?genreID=id
        public ActionResult Details(int? genreID)
        {
            if (genreID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genre.Find(genreID);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Admin/Genres/Create
        public ActionResult Create()
        {
            Genre md = new Genre();
            return View(md);
        }

        // POST: Admin/Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genre.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        // GET: Admin/Genres/Edit?genreID=id
        public ActionResult Edit(int? genreID)
        {
            if (genreID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genre.Find(genreID);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Admin/Genres/Edit?genreID=id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        // GET: Admin/Genres/Delete?genreID=id
        public ActionResult Delete(int? genreID)
        {
            if (genreID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genre.Find(genreID);
            if (genre == null)
            {
                return HttpNotFound();
            }
            db.Genre.Remove(genre);
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
