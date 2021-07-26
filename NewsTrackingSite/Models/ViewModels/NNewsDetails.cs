using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models.ViewModels
{
    //public class NNewsDetails
    //{
    //    public string Title { get; set; }

    //    public string Description { get; set; }

    //    public byte[] Poster { get; set; }

    //    public DateTime ReleaseDate { get; set; }

    //    public string ReleaseCountry { get; set; }

    //    public string TrailerLink { get; set; }

    

    //    public List<Genre> Genres { get; set; }

    //    public List<Category> Categories { get; set; }
    //}

    public class NNewsDetails
    {
        public News News { get; set; }


        [Display(Name = "Tür")]
        public List<Genre> Genres { get; set; }
    }
}