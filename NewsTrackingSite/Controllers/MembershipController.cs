using NewsTrackingSite.Models;
using NewsTrackingSite.Models.ViewModels;
using NewsTrackingSite.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Controllers
{
    public class MembershipController : Controller
    {
        private MembershipRepository membership;
        public MembershipController()
        {
            membership = new MembershipRepository();
        }

        // GET: Users
        [AllowAnonymous]
        public ActionResult Login()
        {
            NLoginModel md = new NLoginModel();
            return View(md);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(NLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Password = MD5Generate.GetMd5Hash(model.Password);
                var loginResult = membership.Login(model);
                if (loginResult.IsSuccessful)
                {
                    Session["User"] =loginResult.Data.ID.ToString();

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    ModelState.AddModelError("", loginResult.Message);
                }
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            User md = new User();
            return View(md);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                model.Password = MD5Generate.GetMd5Hash(model.Password);
                model.RegisterDate = DateTime.Now;
                model.MemberType = MemberType.User;
                if (file != null && file.ContentLength > 0)
                {
                    MemoryStream memoryStream = file.InputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        file.InputStream.CopyTo(memoryStream);
                    }
                    model.ProfilPicture = memoryStream.ToArray();
                }

                var registerResult = membership.Register(model);
                if (registerResult.IsSuccessful)
                {
                    ViewBag.State = registerResult.Message;
                }
                else
                {
                    ModelState.AddModelError("", registerResult.Message);
                }
            }

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session.Remove("User");

            return RedirectToAction("Index", "Home");
        }
    }
}