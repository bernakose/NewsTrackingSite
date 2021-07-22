using NewsTrackingSite.Models;
using NewsTrackingSite.Models.HelperModels;
using NewsTrackingSite.Models.ViewModels;
using NewsTrackingSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Repositories
{
    public class NewsRepository : BaseRepository
    {
        public NewsRepository() : base() { }
        public NResult<List<News>> GetirTumu()
        {
            try
            {
                return new NResult<List<News>>
                {
                    IsSuccessful = true,
                    Data = newsTrackingDB.News.OrderBy(o => o.Title).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<News>> { Message = hata.Message }; }
        }

        //kategoritipıd ye göre haberleri getir !!!
        public NResult<List<News>> GetirHaberler(int kategoritipID)
        {
            try
            {
                return new NResult<List<News>>
                {
                    IsSuccessful = true,
                    Data = newsTrackingDB.News.Where(d => d.Genre.Any(c => c.GenreID == kategoritipID)).OrderBy(o => o.Title).ToList()
                };
            }
            catch (Exception hata) { return new NResult<List<News>> { Message = hata.Message }; }
        }

        public NResult<News> Getir(int id)
        {
            try
            {
                var haberler = (from d in newsTrackingDB.News
                               where d.NewsID == id
                               select d);
                if (haberler.Count() > 0)
                {
                    return new NResult<News>
                    {
                        IsSuccessful = true,
                        Data = haberler.FirstOrDefault()
                    };
                }
                else
                {
                    return new NResult<News>();
                }
            }
            catch (Exception hata) { return new NResult<News> { Message = hata.Message }; }
        }

        public NResult<int> Kaydet(News kayit)
        {
            try
            {
                newsTrackingDB.News.Add(kayit);
                newsTrackingDB.SaveChanges();

                return new NResult<int>
                {
                    IsSuccessful = true,
                    Data = kayit.NewsID
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

        public NResult Guncelle(News kayit)
        {
            try
            {
                var duzenlenecekKayitlar = newsTrackingDB.News.Where(d => d.NewsID == kayit.NewsID);
                if (duzenlenecekKayitlar.Count() > 0)
                {
                    var duzenlenecekKayit = duzenlenecekKayitlar.FirstOrDefault();
                    duzenlenecekKayit.Title = kayit.Title;
                    duzenlenecekKayit.Description = kayit.Description;
                    duzenlenecekKayit.TrailerLink = kayit.TrailerLink;
                    duzenlenecekKayit.ReleaseDate = kayit.ReleaseDate;
                    duzenlenecekKayit.Poster = kayit.Poster;

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
                var silinecekKayitlar = newsTrackingDB.News.Where(d => d.NewsID == id);
                if (silinecekKayitlar.Count() > 0)
                {
                    var silinecekKayit = silinecekKayitlar.FirstOrDefault();
                    newsTrackingDB.News.Remove(silinecekKayit);
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

        public NResult<SelectList> GetirTumu_SelectList()
        {
            var haberler = (from d in newsTrackingDB.News
                           orderby d.Title

                           select new SelectListItem
                           {
                               Text = d.Title,
                               Value = d.NewsID.ToString()
                           }).ToList();

            return new NResult<SelectList>
            {
                IsSuccessful = true,
                Data = new SelectList(haberler, "Value", "Text")
            };
        }

        public NResult<List<NSelectListItem>> GetirTumu_SelectList(int seciliHaberID = 0)
        {
            var haberler = (from f in newsTrackingDB.News
                           orderby f.Title
                           select new NSelectListItem
                           {
                               Text = f.Title,
                               Value = f.NewsID.ToString()
                           }).ToList();
            if (seciliHaberID > 0)
            {
                for (int i = 0; i < haberler.Count; i++)
                {
                    if (haberler[i].Value == seciliHaberID.ToString())
                    {
                        haberler[i].Selected = true;
                        break;
                    }
                }
            }

            return new NResult<List<NSelectListItem>>
            {
                IsSuccessful = true,
                Data = haberler
            };
        }
    }
}