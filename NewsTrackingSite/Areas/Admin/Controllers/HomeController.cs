using NewsTrackingSite.Models.ViewModels;
using NewsTrackingSite.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private NewsTrackingDB db = new NewsTrackingDB();

        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.AdminCount = db.User.Where(u => u.MemberType == MemberType.Admin).Count();
            ViewBag.NewsCount = db.News.Count();
            ViewBag.GenreCount = db.Genre.Count();
            ViewBag.UserCount = db.User.Count();

            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("User");

            return RedirectToAction("Index", "../Home");
        }


        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);

            return Redirect(returnUrl);
        }
    }
}