//using NewsTrackingSite.Models;
//using NewsTrackingSite.Repositories;
//using System.Web.Mvc;

//namespace NewsTrackingSite.Areas.Admin.Controllers
//{
//    public class CategoriesController : Controller
//    {
//        KategoriTipRepository _repository = new KategoriTipRepository();

//        #region Kategori Listesi

//        public ActionResult Index()
//        {
//            var kategoriler = _repository.GetirTumu();
//            if (kategoriler.IsSuccessful)
//                return View(kategoriler.Data);
//            return View();
//        }

//        #endregion

//        #region Kategori Detay

//        public ActionResult Details(int id)
//        {
//            var kategori = _repository.Getir(id);
//            if (kategori.IsSuccessful)
//                return View(kategori.Data);
//            return RedirectToAction("KategoriListesi");
//        }

//        #endregion

//        #region Kategori Ekle

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Create(Category kayit)
//        {
//            if (ModelState.IsValid)
//            {
//                var islemSonuc = _repository.Kaydet(kayit);
//                if (islemSonuc.IsSuccessful)
//                {
//                    return RedirectToAction("KategoriListesi");
//                }
//                else
//                {
//                    ModelState.AddModelError("", islemSonuc.Message);
//                    return View();
//                }
//            }
//            else
//            {
//                return View();
//            }
//        }

//        #endregion

//        #region Kategori Düzenle

//        public ActionResult Edit(int id)
//        {
//            var kategori = _repository.Getir(id);
//            if (kategori.IsSuccessful)
//                return View(kategori.Data);
//            return RedirectToAction("KategoriListesi");
//        }

//        [HttpPost]
//        public ActionResult Edit(Category kayit)
//        {
//            if (ModelState.IsValid)
//            {
//                var islemSonuc = _repository.Guncelle(kayit);
//                if (islemSonuc.IsSuccessful)
//                {
//                    return RedirectToAction("KategoriListesi");
//                }
//                else
//                {
//                    ModelState.AddModelError("", islemSonuc.Message);
//                    return View(kayit);
//                }
//            }
//            else
//            {
//                return View();
//            }
//        }

//        #endregion

//        #region Kategori Sil

//        public ActionResult Delete(int id)
//        {
//            var kategori = _repository.Getir(id);
//            if (kategori.IsSuccessful)
//                return View(kategori.Data);
//            return RedirectToAction("KategoriListesi");
//        }

//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection bilgiler)
//        {
//            var islemSonuc = _repository.Sil(id);
//            if (islemSonuc.IsSuccessful)
//            {
//                return RedirectToAction("KategoriListesi");
//            }
//            else
//            {
//                ModelState.AddModelError("", islemSonuc.Message);
//                return View();
//            }
//        }

//        #endregion
//    }
//}