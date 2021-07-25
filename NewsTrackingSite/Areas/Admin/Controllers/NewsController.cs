using PagedList;
using NewsTrackingSite.Helpers;
using NewsTrackingSite.Models;
using NewsTrackingSite.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NewsTrackingSite.Repositories;

namespace NewsTrackingSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "Uye")]
    public class NewsController : Controller
    {
        NewsRepository _repository = new NewsRepository();
        NewsTrackingDB db = new NewsTrackingDB();

        public ActionResult Index(int? page, string sorting)    //Sayfada haberlerin sıralanma işlemi yapılır.
        {
            var news = _repository.GetirTumu();
            if (!news.IsSuccessful)
                return View();


            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            List<News> contentNews = news.Data;

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            ViewBag.RaitingSortParam = sorting == "RaitingDesc" ? "RaitingAsc" : "RaitingDesc";


            switch (sorting)
            {
                case "ReleaseDateAsc":
                    contentNews = contentNews.OrderBy(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateDesc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "ReleaseDateDesc":
                    contentNews = contentNews.OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                //case "RaitingAsc":
                //    contentNews = contentNews.OrderBy(m => m.Raiting).ToList();
                //    ViewBag.CurrentSortParam = "RaitingDesc";
                //    ViewBag.CurrentDDLText = "Raiting";
                //    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                //    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                //    break;
                //case "RaitingDesc":
                //    contentNews = contentNews.OrderByDescending(m => m.Raiting).ToList();
                //    ViewBag.CurrentSortParam = "RaitingAsc";
                //    ViewBag.CurrentDDLText = "Raiting";
                //    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                //    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                //    break;
                default:
                    contentNews = contentNews.OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View("Index", contentNews.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Details(int newsID)    //Burada haber ile ilgili haberin ismi,açıklaması,resmi,yayınlanma tarihi ve linki gibi bilgiler alınır.
        {
            var newsWrapper = _repository.Getir(newsID);
            if (!newsWrapper.IsSuccessful)
                return RedirectToAction("Index", "News");

            var news = newsWrapper.Data;
            var newsDetail = new NNewsDetails()
            {
                News = new News()
                {
                    Title = news.Title,
                    Description = news.Description,
                    Poster = news.Poster,
                    ReleaseDate = news.ReleaseDate,
                    ReleaseCountry = news.ReleaseCountry,
                    TrailerLink = news.TrailerLink
                },
                //Directors = new DirectorRepository().GetDirector(newsID).Data,
                //Actors = new ActorRepository().GetActor(newsID).Data,
                Genres = new KategoriTipRepository().GetGenre(newsID).Data
            };

            return View(newsDetail);
        }


        public ActionResult Create()        //Yeni bir haber eklenmek istendiğinde boş bırakılan yerlerin hata kontrolü yapılır.
        {
            var genreList = new KategoriTipRepository().GetirTumu();
            //var directorList = new DirectorRepository().GetAllDirector();
            //var actorList = new ActorRepository().GetAllActor();

            #region [HATA KONTROL]
            if (!genreList.IsSuccessful)
            {
                ModelState.AddModelError("", genreList.Message);
            }
            //if (!directorList.IsSuccessful)
            //{
            //    ModelState.AddModelError("", directorList.Message);
            //}
            //if (!actorList.IsSuccessful)
            //{
            //    ModelState.AddModelError("", actorList.Message);
            //}
            //if (!(genreList.IsSuccessful && directorList.IsSuccessful && actorList.IsSuccessful))
            //{
            //    return Create();
            //}
            #endregion


            MultiSelectList genreSelectList = new MultiSelectList(genreList.Data, "GenreID", "GenreName");
            //MultiSelectList directorSelectList = new MultiSelectList(directorList.Data, "DirectorID", "DirectorName");
            //MultiSelectList actorSelectList = new MultiSelectList(actorList.Data, "ActorID", "ActorName");

            ViewData["GenreList"] = genreSelectList;
            //ViewData["DirectorList"] = directorSelectList;
            //ViewData["ActorList"] = actorSelectList;

            News md = new News();

            return View(md);
        }

        [HttpPost]
        public ActionResult Create(News news, HttpPostedFileBase file, FormCollection formColl)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }


            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                news.Poster = memoryStream.ToArray();
            }

            var islemSonuc = _repository.Kaydet(news);
            if (!islemSonuc.IsSuccessful)
            {
                ModelState.AddModelError("", islemSonuc.Message);

                return Create();
            }

            NewsTrackingDB db = new NewsTrackingDB();
            var tempSelectedGenres = (formColl["GenreList"] as string).Split(',').ToList();
            //var tempSelectedDirectors = (formColl["DirectorList"] as string).Split(',').ToList();
            //var tempSelectedActors = (formColl["ActorList"] as string).Split(',').ToList();

            foreach (var item in tempSelectedGenres)
            {
                var tempGenreID = Convert.ToInt32(item);
                var tempNews = db.News.Where(m => m.NewsID == islemSonuc.Data).FirstOrDefault();
                var tempGenre = db.Genre.Where(g => g.GenreID == tempGenreID).FirstOrDefault();
                tempGenre.News.Add(tempNews);
                tempNews.Genre.Add(tempGenre);
            }

            //foreach (var item in tempSelectedDirectors)
            //{
            //    var tempDirectorID = Convert.ToInt32(item);
            //    var tempNews = db.News.Where(m => m.NewsID == islemSonuc.Data).FirstOrDefault();
            //    //var tempDirector = db.Director.Where(d => d.DirectorID == tempDirectorID).FirstOrDefault();
            //    //tempDirector.News.Add(tempNews);
            //    //tempNews.Director.Add(tempDirector);
            //}

            //foreach (var item in tempSelectedActors)
            //{
            //    var tempActorID = Convert.ToInt32(item);
            //    var tempNews = db.News.Where(m => m.NewsID == islemSonuc.Data).FirstOrDefault();
            //    //var tempActor = db.Actor.Where(a => a.ActorID == tempActorID).FirstOrDefault();
            //    //tempActor.News.Add(tempNews);
            //    //tempNews.Actor.Add(tempActor);
            //}

            db.SaveChanges();


            return RedirectToAction("Index", "News");
        }


        public ActionResult Edit(int newsID)       //Burası haber bilgileri düzenlenmek istenildiğinde çalışır. Gerekli hata kontrolü yapılır.
        {
            NewsTrackingDB db = new NewsTrackingDB();

            var newsWrapper = _repository.Getir(newsID);
            var genreList = new KategoriTipRepository().GetirTumu();
            //var directorList = new DirectorRepository().GetAllDirector();
            //var actorList = new ActorRepository().GetAllActor();

            #region [HATA KONTROL]
            if (!newsWrapper.IsSuccessful)
            {
                ModelState.AddModelError("", newsWrapper.Message);
            }
            if (!genreList.IsSuccessful)
            {
                ModelState.AddModelError("", genreList.Message);
            }
            //if (!directorList.IsSuccessful)
            //{
            //    ModelState.AddModelError("", directorList.Message);
            //}
            //if (!actorList.IsSuccessful)
            //{
            //    ModelState.AddModelError("", actorList.Message);
            //}
            //if (!(newsWrapper.IsSuccessful && genreList.IsSuccessful && directorList.IsSuccessful && actorList.IsSuccessful))
            //{
            //    return Create();
            //}
            #endregion


            MultiSelectList genreSelectList = new MultiSelectList(genreList.Data, "GenreID", "GenreName");
            //MultiSelectList directorSelectList = new MultiSelectList(directorList.Data, "DirectorID", "DirectorName");
            //MultiSelectList actorSelectList = new MultiSelectList(actorList.Data, "ActorID", "ActorName");

            ViewData["GenreList"] = genreSelectList;
            //ViewData["DirectorList"] = directorSelectList;
            //ViewData["ActorList"] = actorSelectList;

            return View(newsWrapper.Data);
        }

        [HttpPost]
        public ActionResult Edit(News news, HttpPostedFileBase file, FormCollection formColl)
        {
            if (!ModelState.IsValid)
            {
                return Create();
            }


            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                news.Poster = memoryStream.ToArray();
            }
            NewsTrackingDB db = new NewsTrackingDB();


            var tempSelectedGenres = (formColl["GenreList"] as string).Split(',').ToList();
            //var tempSelectedDirectors = (formColl["DirectorList"] as string).Split(',').ToList();
            //var tempSelectedActors = (formColl["ActorList"] as string).Split(',').ToList();


            var delNews = db.News.Where(m => m.NewsID == news.NewsID).FirstOrDefault();
            foreach (var item in db.Genre.ToList())
            {
                item.News.Remove(delNews);
            }
            delNews.Genre.Clear();

            foreach (var item in tempSelectedGenres)
            {
                var tempGenreID = Convert.ToInt32(item);
                var tempNews = db.News.Where(m => m.NewsID == news.NewsID).FirstOrDefault();
                var tempGenre = db.Genre.Where(g => g.GenreID == tempGenreID).FirstOrDefault();
                tempGenre.News.Add(tempNews);
                tempNews.Genre.Add(tempGenre);
            }



            //var delDirec = db.News.Where(m => m.NewsID == news.NewsID).FirstOrDefault();
            //foreach (var item in db.Director.ToList())
            //{
            //    item.News.Remove(delDirec);
            //}
            //delDirec.Genre.Clear();

            //foreach (var item in tempSelectedDirectors)
            //{
            //    var tempDirectorID = Convert.ToInt32(item);
            //    var tempNews = db.News.Where(m => m.NewsID == news.NewsID).FirstOrDefault();
            //    var tempDirector = db.Director.Where(d => d.DirectorID == tempDirectorID).FirstOrDefault();
            //    tempDirector.News.Add(tempNews);
            //    tempNews.Director.Add(tempDirector);
            //}



            //var delActor = db.News.Where(m => m.NewsID == news.NewsID).FirstOrDefault();
            //foreach (var item in db.Actor.ToList())
            //{
            //    item.News.Remove(delActor);
            //}
            //delActor.Genre.Clear();

            //foreach (var item in tempSelectedActors)
            //{
            //    var tempActorID = Convert.ToInt32(item);
            //    var tempNews = db.News.Where(m => m.NewsID == news.NewsID).FirstOrDefault();
            //    var tempActor = db.Actor.Where(a => a.ActorID == tempActorID).FirstOrDefault();
            //    tempActor.News.Add(tempNews);
            //    tempNews.Actor.Add(tempActor);
            //}

            db.SaveChanges();


            return RedirectToAction("Index", "News");
        }


        public ActionResult Delete(int? newsID)    //Eklenilen haberin silinmesi işlemi yapılır.
        {
            News tempNews = db.News.Find(newsID);

            try
            {
                foreach (var item in db.Genre.ToList())
                {
                    item.News.Remove(tempNews);
                }
                //foreach (var item in db.Director.ToList())
                //{
                //    item.News.Remove(tempNews);
                //}
                //foreach (var item in db.Actor.ToList())
                //{
                //    item.News.Remove(tempNews);
                //}
                tempNews.Genre.Clear();
                //tempNews.Director.Clear();
                //tempNews.Actor.Clear();
                db.News.Remove(tempNews);
                db.SaveChanges();
            }
            catch (Exception) { return new HttpStatusCodeResult(HttpStatusCode.NotModified); }

            return RedirectToAction("Index", "News");
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