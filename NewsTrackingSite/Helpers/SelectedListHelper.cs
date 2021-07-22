using NewsTrackingSite.Models.ViewModels;
using NewsTrackingSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsTrackingSite.Helpers
{
    public class SelectedListHelper
    {
        #region Değişkenler
        private static KategoriTipRepository kategoritiprepository = new KategoriTipRepository();
        private static NewsRepository haberrepository = new NewsRepository();

        #endregion

        public static IEnumerable<SelectListItem> Kategoritipler
        {
            get
            {
                var kategoritipler = kategoritiprepository.GetirTumu_SelectList();
                if (kategoritipler.IsSuccessful)
                    return kategoritipler.Data;
                return new SelectList(null);
            }
        }

        public static IEnumerable<SelectListItem> Haberler
        {
            get
            {
                var donemler = haberrepository.GetirTumu_SelectList();
                if (donemler.IsSuccessful)
                    return donemler.Data;
                return new SelectList(null);


            }
        }

        public static List<NSelectListItem> GetirKategoriHaberleri(int kategoritipıd)
        {
            var dersler = haberrepository.GetirTumu_SelectList(kategoritipıd);
            if (dersler.IsSuccessful)
                return dersler.Data;

            return null;
        }
    }
}