using NewsTrackingSite.Models.ViewModels;
using NewsTrackingSite.Repositories;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Controllers
{
    public class HomeController : Controller
    {
        NewsTrackingDB db = new NewsTrackingDB();
        // GET: Home
        public ActionResult Index(int? page)
        {
            var contentNews = db.News.OrderByDescending(m => m.ReleaseDate).ToList();
            NMainPageDTO obj = new NMainPageDTO();
            obj.Categories = db.Genre.OrderBy(c=>c.GenreName).ToList();
            obj.Posters = (from t in db.News where t.ReleaseDate > DateTime.Now orderby t.ReleaseDate descending select t.Poster).ToList();
            obj.RandomNews = (from result in contentNews.AsEnumerable() orderby Guid.NewGuid() select result).Take(3).ToList();

            ViewBag.MainPageDTO = obj;

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View("Index", contentNews.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Contact()
        {
            NContactModel md = new NContactModel();
            return View(md);
        }
        [HttpPost]
        public ActionResult Contact(NContactModel model)
        {
            if (ModelState.IsValid)
            {
                MailMessage email = new MailMessage();

                string Host = "smtp-mail.outlook.com";
                string smtpUserName = "...@hotmail.com";
                string smtpPassword = "...sifre...";
                email.From = new MailAddress(model.EMail);
                int smtpPort = 587;
                email.IsBodyHtml = true;

                email.Subject = "News Tracking Contact - Person: " + model.Name;
                email.Body = "Mesaj : <br /><br />" + model.Message + "<br /><br />" +
                             "İsim: " + model.Name + "<br />" +
                            "Telefon: " + model.Telephone;
                email.To.Add(new MailAddress("bernakose@gmail.com"));
                email.BodyEncoding = System.Text.Encoding.UTF8;
                SmtpClient smtp = new SmtpClient(Host, smtpPort);
                smtp.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
                smtp.EnableSsl = true;
                smtp.Send(email);
                ViewBag.Success = true;
            }
            return View();
        }
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
    }
}