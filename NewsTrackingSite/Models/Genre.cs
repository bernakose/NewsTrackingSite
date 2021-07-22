using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NewsTrackingSite.Models
{
    [Table("Genre")]
    public partial class Genre
    {
        public Genre()
        {
            News = new HashSet<News>();
        }

        [Key]
        public int GenreID { get; set; }

        [Required(ErrorMessage ="Lütfen Haber Türünü Giriniz !")]
        [Display(Name = "Haber Türü")]
        public string GenreName { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}