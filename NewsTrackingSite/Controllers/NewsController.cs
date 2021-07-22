using NewsTrackingSite.Models.ViewModels;
using NewsTrackingSite.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Controllers
{
    public class NewsController : Controller
    {
        NewsTrackingDB db = new NewsTrackingDB();
        
        public ActionResult NewsThisWeek(int? page, string sorting)
        {
            var contentNews = db.News.OrderByDescending(m => m.ReleaseDate).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.RandomNews = (from result in contentNews.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";

            // Gelen sıralama ifadesine göre sıralama yapılır.
            DateTime temp = DateTime.Now.AddDays(-7);
            switch (sorting)
            {
                case "ReleaseDateAsc":
                    contentNews = db.News.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderBy(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateDesc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-desc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda çoktan aza sıralar.";
                    break;
                case "ReleaseDateDesc":
                    contentNews = db.News.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
                default:
                    contentNews = db.News.Where(m => (m.ReleaseDate >= temp && m.ReleaseDate <= DateTime.Now)).OrderByDescending(m => m.ReleaseDate).ToList();
                    ViewBag.CurrentSortParam = "ReleaseDateAsc";
                    ViewBag.CurrentDDLText = "Release Date";
                    ViewBag.CurrentArrow = "fa fa-sort-amount-asc";
                    ViewBag.ResortBtnAlt = "Tıkladığınızda azdan çoka sıralar.";
                    break;
            }
            // ------------------- SIRALAMA --------------------
            #endregion


            int pageSize = 5;
            int pageNumber =(page ?? 1);

            return View("NewsThisWeek", contentNews.ToPagedList(pageNumber, pageSize)); 
        }

        public ActionResult NewsDetails(int newsID)       //Haber detaylarını göstermek için kullanılır.
        {
            var contentNews = db.News.Where(m => m.ReleaseDate >= DateTime.Now).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.RandomNews = (from result in contentNews.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            var news = db.News.Where(m => m.NewsID == newsID).FirstOrDefault();

            return View(news);
        }

        public ActionResult NewsList(int genreID, int? page, string sorting)       //Haberlerin sıralanıp listelenmesi için
        {
            var news = db.News.Where(m => m.ReleaseDate >= DateTime.Now).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c => c.GenreName).ToList();
            obj.RandomNews = (from result in news.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            var tempGenre = db.Genre.Where(g => g.GenreID == genreID).First();
            ViewBag.Title = tempGenre.GenreName;

            var contentNews = tempGenre.News.ToList();
            #region [SIRALAMA]
            // ------------------- SIRALAMA --------------------

            // Burada controller çağrılırken "?sorting=xxx_ifade" şeklinde parametre gelir.
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "ReleaseDateDesc";
            }
            ViewBag.ReleaseDateSortParam = sorting == "ReleaseDateAsc" ? "ReleaseDateDesc" : "ReleaseDateAsc";
            ViewBag.RaitingSortParam = sorting == "RaitingDesc" ? "RaitingAsc" : "RaitingDesc";

            // Gelen sıralama ifadesine göre sıralama yapılır.
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

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View("NewsList", contentNews.ToPagedList(pageNumber, pageSize));
        }
    }
}