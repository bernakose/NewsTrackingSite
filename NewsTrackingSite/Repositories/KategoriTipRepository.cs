using NewsTrackingSite.Models;
using NewsTrackingSite.Models.HelperModels;
using NewsTrackingSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Repositories
{
    public class KategoriTipRepository : BaseRepository
    {
        public NResult<List<Genre>> GetirTumu()
        {
            try
            {
                return new NResult<List<Genre>>
                {
                    IsSuccessful = true,
                    Data = newsTrackingDB.Genre.ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Genre>> { Message = hata.Message }; }

        }

        public NResult<Genre> Getir(int id)
        {
            try
            {
                var kategoritipler = (from b in newsTrackingDB.Genre
                                      where b.GenreID == id
                                      select b);
                if (kategoritipler.Count() > 0)
                {
                    return new NResult<Genre>
                    {
                        IsSuccessful = true,
                        Data = kategoritipler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<Genre>();
                }
            }
            catch (Exception hata) { return new NResult<Genre> { Message = hata.Message }; }
        }

        public NResult<int> Kaydet(Genre kayit)
        {
            try
            {
                newsTrackingDB.Genre.Add(kayit);
                newsTrackingDB.SaveChanges();
                return new NResult<int>
                {
                    IsSuccessful = true,
                    Data = kayit.GenreID
                };
            }
            catch (Exception hata)
            {
                return new NResult<int>()
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }

        public NResult Guncelle(Genre kayit)
        {
            try
            {
                var duzenlenecekKayitlar = newsTrackingDB.Genre.Where(b => b.GenreID == kayit.GenreID);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();

                    duzenlenecekKayit.GenreName = kayit.GenreName;

                    newsTrackingDB.SaveChanges();
                    return new NResult { IsSuccessful = true };
                }
                else
                {
                    return new NResult
                    {
                        IsSuccessful = false,
                        Message = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NResult
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }

        public NResult Sil(int id)
        {
            try
            {
                var silinecekKayitlar = newsTrackingDB.Genre.Where(b => b.GenreID == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    newsTrackingDB.Genre.Remove(silinecekKayit);
                    newsTrackingDB.SaveChanges();
                    return new NResult { IsSuccessful = true };
                }
                else
                {
                    return new NResult
                    {
                        IsSuccessful = false,
                        Message = "Kayıt bulunamadı"
                    };
                }
            }
            catch (Exception hata)
            {
                return new NResult<int>()
                {
                    IsSuccessful = false,
                    Message = hata.Message
                };
            }
        }

        //tüm kategoritip bilgilerini slectlist den alıp dropdownliste göndermke için !!!

        public NResult<SelectList> GetirTumu_SelectList()
        {
            var bolumler = (from f in newsTrackingDB.Genre
                            orderby f.GenreName
                            select new SelectListItem
                            {
                                Text = f.GenreName,
                                Value = f.GenreID.ToString()
                            }).ToList();

            return new NResult<SelectList>
            {
                IsSuccessful = true,
                Data = new SelectList(bolumler, "Value", "Text")
            };
        }

        public NResult<List<Genre>> GetGenre(int newsID)
        {
            try
            {
                return new NResult<List<Genre>>
                {
                    IsSuccessful = true,
                    Data = newsTrackingDB.Genre.Where(d => d.News.Any(m => m.NewsID == newsID)).OrderBy(o => o.GenreName).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<Genre>> { Message = hata.Message }; }
        }
    }
}